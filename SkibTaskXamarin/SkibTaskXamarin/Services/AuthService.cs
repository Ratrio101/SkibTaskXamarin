using SkibTaskXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkibTaskXamarin.Services
{
    public class AuthService
    {
        private readonly List<User> _users = new List<User>();

        public bool Register(string username, string password)
        {
            if (_users.Any(u => u.Username == username)) return false;
            _users.Add(new User { Username = username, Password = password });
            return true;
        }

        public bool Login(string username, string password)
        {
            return _users.Any(u => u.Username == username && u.Password == password);
        }
    }
    public class TaskService
    {
        private readonly List<TaskItem> _tasks = new List<TaskItem>();

        public List<TaskItem> GetTasks() => _tasks;

        public void AddTask(TaskItem task) => _tasks.Add(task);

        public void UpdateTask(TaskItem task)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.DueDate = task.DueDate;
                existingTask.IsCompleted = task.IsCompleted;
            }
        }

        public void DeleteTask(string taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null) _tasks.Remove(task);
        }
    }
}
