using SkibTaskXamarin.Models;
using SkibTaskXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkibTaskXamarin.ViewModels
{
    public class AddTaskViewModel : BindableObject
    {
        private readonly TaskService _taskService = new TaskService();

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(1);

        public ICommand SaveTaskCommand { get; }

        public AddTaskViewModel()
        {
            SaveTaskCommand = new Command(OnSaveTask);
        }

        private void OnSaveTask()
        {
            var newTask = new TaskItem
            {
                Title = Title,
                Description = Description,
                DueDate = DueDate
            };

            _taskService.AddTask(newTask);

            // Отправка сообщения об обновлении
            MessagingCenter.Send(this, "TaskAdded");

            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
