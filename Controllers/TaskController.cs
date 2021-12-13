using AutoMapper;
using System.Linq;
using System.Web.Http;
using Tasks.Application.Models;
using Tasks.Application.Repositories;
using Tasks.Domain.Entities;

namespace Tasks.Controllers
{
    [RoutePrefix("tasks")]
    public class TaskController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TaskController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetTasks()
        {
            var tasks = unitOfWork.Tasks.Get().ToList();

            return Ok(tasks);
        }

        [HttpGet]
        [Route("{taskId}", Name = "GetTaskById")]
        public IHttpActionResult GetTaskById(int taskId)
        {
            var task = unitOfWork.Tasks.GetByID(taskId);

            return Ok(mapper.Map<TaskReadDto>(task));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddTask(TaskCreateDto taskCreateDto)
        {
            Task task = mapper.Map<Task>(taskCreateDto);

            unitOfWork.Tasks.Insert(task);

            unitOfWork.Commit();

            return CreatedAtRoute(nameof(GetTaskById), new { taskId = task.Id }, mapper.Map<TaskReadDto>(task));
        }


        [HttpPost]
        [Route("{taskId}/{userId}")]
        public IHttpActionResult AssignTask(int taskId, int userId)
        {
            var task = unitOfWork.Tasks.GetByID(taskId);
            var user = unitOfWork.Users.GetByID(userId);

            if (task == null)
                return BadRequest("Task was not found!");

            if (user == null)
                return BadRequest("User was not found!");

            task.UserId = userId;
            unitOfWork.Tasks.Update(task);
            unitOfWork.Commit();

            return CreatedAtRoute(nameof(GetTaskById), new { taskId = task.Id }, mapper.Map<TaskReadDto>(task));
        }
    }
}
