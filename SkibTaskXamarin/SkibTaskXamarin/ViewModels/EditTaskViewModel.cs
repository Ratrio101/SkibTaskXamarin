using SkibTaskXamarin.Models;
using SkibTaskXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkibTaskXamarin.ViewModels
{
    public class EditTaskViewModel : BindableObject
    {
        private readonly TaskService _taskService = new TaskService();

        public TaskItem Task { get; set; }

        public ICommand SaveTaskCommand { get; }

        public EditTaskViewModel(TaskItem task)
        {
            Task = task;
            SaveTaskCommand = new Command(OnSaveTask);
        }

        private void OnSaveTask()
        {
            _taskService.UpdateTask(Task);
            // Уведомляем MainViewModel об изменении
            MessagingCenter.Send(this, "TaskUpdated", Task);

            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
