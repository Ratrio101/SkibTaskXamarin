using SkibTaskXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkibTaskXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTaskPage : ContentPage
    {
        public EditTaskPage(EditTaskViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}