using SkibTaskXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace SkibTaskXamarin.Services
{
    public class AuthService
    {
        private const string UserFileName = "users.json";
        private readonly string _filePath;
        private List<User> _users;

        public AuthService()
        {
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), UserFileName);
            _users = LoadUsers();
        }

        public bool Register(string username, string password)
        {
            if (_users.Any(u => u.Username == username)) return false;
            _users.Add(new User { Username = username, Password = password });
            SaveUsers();
            return true;
        }

        public bool Login(string username, string password)
        {
            return _users.Any(u => u.Username == username && u.Password == password);
        }

        private void SaveUsers()
        {
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(_users));
        }

        private List<User> LoadUsers()
        {
            if (!File.Exists(_filePath)) return new List<User>();
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }
    }
    public class TaskService
    {
        private readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tasks.json");

        private List<TaskItem> _tasks;

        public TaskService()
        {
            LoadTasks();
        }

        private void LoadTasks()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                _tasks = JsonConvert.DeserializeObject<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
            else
            {
                _tasks = new List<TaskItem>();
            }
        }

        private void SaveTasks()
        {
            var json = JsonConvert.SerializeObject(_tasks, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public List<TaskItem> GetTasks()
        {
            LoadTasks();
            return _tasks;
        }

        public void AddTask(TaskItem task)
        {
            _tasks.Add(task);
            SaveTasks();
        }

        public void UpdateTask(TaskItem task)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.DueDate = task.DueDate;
                existingTask.IsCompleted = task.IsCompleted;
                SaveTasks();
            }
        }

        public void DeleteTask(string taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                _tasks.Remove(task);
                SaveTasks();
            }
        }
    }
}
