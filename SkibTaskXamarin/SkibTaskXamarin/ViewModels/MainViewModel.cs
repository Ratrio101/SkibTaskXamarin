using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SkibTaskXamarin.Models;
using SkibTaskXamarin.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SkibTaskXamarin.ViewModels
{
    public class MainViewModel : BindableObject
    {
        private readonly TaskService _taskService = new TaskService();

        public ObservableCollection<TaskItem> Tasks { get; set; } = new ObservableCollection<TaskItem>();

        private TaskItem _selectedTask;
        public TaskItem SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        public MainViewModel()
        {
            LoadTasks();
            AddTaskCommand = new Command(OnAddTask);
            EditTaskCommand = new Command(OnEditTask);
            DeleteTaskCommand = new Command(OnDeleteTask);
        }

        private void LoadTasks()
        {
            var tasks = _taskService.GetTasks();
            Tasks.Clear();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
        }

        private void OnAddTask()
        {
            var newTask = new TaskItem { Title = "Новая задача", DueDate = DateTime.Now.AddDays(1) };
            _taskService.AddTask(newTask);
            LoadTasks();
        }

        private void OnEditTask()
        {
            if (SelectedTask != null)
            {
                // Simulate editing task
                SelectedTask.Title = "Отредактированная задача";
                _taskService.UpdateTask(SelectedTask);
                LoadTasks();
            }
        }

        private void OnDeleteTask()
        {
            if (SelectedTask != null)
            {
                _taskService.DeleteTask(SelectedTask.Id);
                LoadTasks();
            }
        }
    }
}
