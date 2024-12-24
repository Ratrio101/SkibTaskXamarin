using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SkibTaskXamarin.Models;
using SkibTaskXamarin.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SkibTaskXamarin.Views;
using System.Linq;

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
            // _taskService = new TaskService();

            AddTaskCommand = new Command(OnAddTask);
            EditTaskCommand = new Command(OnEditTask);
            DeleteTaskCommand = new Command(OnDeleteTask);

            MessagingCenter.Subscribe<EditTaskViewModel, TaskItem>(this, "TaskUpdated", (sender, updatedTask) =>
            {
                // Удаляем старую задачу
                var existingTask = Tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
                if (existingTask != null)
                {
                    Tasks.Remove(existingTask);
                }

                // Добавляем обновлённую задачу
                Tasks.Add(updatedTask);

                // Уведомляем интерфейс
                OnPropertyChanged(nameof(Tasks));
            });

            LoadTasks();
            // MessagingCenter.Subscribe<AddTaskViewModel>(this, "TaskAdded", (sender) => LoadTasks());
        }

        private void LoadTasks()
        {
            Tasks.Clear();
            var tasks = _taskService.GetTasks();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
        }

        private async void OnAddTask()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.AddTaskPage());
            MessagingCenter.Subscribe<AddTaskViewModel>(this, "TaskAdded", sender =>
            {
                var newTasks = _taskService.GetTasks();
                Tasks.Clear();
                foreach (var task in newTasks)
                {
                    Tasks.Add(task);
                }
            });
        }

        private async void OnEditTask()
        {
            if (SelectedTask != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new EditTaskPage(new EditTaskViewModel(SelectedTask)));
                LoadTasks(); // Перезагрузка задач после редактирования
            }
        }

        private void OnDeleteTask()
        {
            if (SelectedTask != null)
            {
                _taskService.DeleteTask(SelectedTask.Id);
                Tasks.Remove(SelectedTask); // Удалить из коллекции
                LoadTasks(); // Перезагрузка задач после удаления
            }
        }
    }
}
