using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Application.Models
{
    public class Message
    {
        public MessageType Type { get; set; }
        public object Data { get; set; }
    }

    public enum MessageType
    {
        ADD_USER,
        DELETE_USER
    }
}
