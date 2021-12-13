using AutoMapper;
using EasyNetQ;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Linq;
using Tasks.Application.Models;
using Tasks.Application.Repositories;
using Tasks.Domain.Entities;
using Unity;

namespace Tasks.Application.AsyncServices
{
    public static class MessageBusSubscriber
    {
        private readonly static IBus _connection = RabbitHutch.CreateBus(ConfigurationManager.ConnectionStrings["RabbitMQ"].ConnectionString);

        public static void Subscribe(IUnityContainer container)
        {
            _connection.PubSub.Subscribe<string>("UsersSubscriber", message => ProcessMessage(message, container));
        }

        private static void ProcessMessage(string jsonMessage, IUnityContainer container)
        {
            var message = JsonConvert.DeserializeObject<Message>(jsonMessage);
            var mapper = container.Resolve<IMapper>();
            var unitOfWork = container.Resolve<IUnitOfWork>();

            switch (message.Type)
            {
                case MessageType.ADD_USER:
                    var userToAdd = ((JObject)message.Data).ToObject<User>();
                    if (!unitOfWork.IsUserDefined(userToAdd))
                    {
                        unitOfWork.Users.Insert(userToAdd);
                        unitOfWork.Commit();
                    }
                    break;
                case MessageType.DELETE_USER:
                    var userToDelete = ((JObject)message.Data).ToObject<User>();
                    if (unitOfWork.IsUserDefined(userToDelete))
                    {
                        unitOfWork.Users.DeleteByExpression(u => u.Email.Equals(userToDelete.Email));
                        unitOfWork.Commit();
                    }
                    break;
                default:
                    break;
            }
        }

        private static bool IsUserDefined(this IUnitOfWork unitOfWork, User user)
        {
            return unitOfWork.Users.Get(u => u.Email.Equals(user.Email)).Any();
        }
    }
}
