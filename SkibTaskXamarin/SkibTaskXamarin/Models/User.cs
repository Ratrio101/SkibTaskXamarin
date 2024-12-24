using System;
using System.Collections.Generic;
using System.Text;

namespace SkibTaskXamarin.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class TaskItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
