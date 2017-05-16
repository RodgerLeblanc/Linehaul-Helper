using Linehaul_Helper.Services;
using Linehaul_Helper.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Linehaul_Helper.Views
{

    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobsPage : ContentPage
    {
        private JobsPageViewModel _jobsViewModel;

        public JobsPage()
        {
            InitializeComponent();
            _jobsViewModel = new JobsPageViewModel(new JobsRetrievalService());
            BindingContext = _jobsViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if ((_jobsViewModel.Jobs != null) && (_jobsViewModel.Jobs.Count != 0))
                return;

            if (_jobsViewModel.GetJobsCommand.CanExecute(null))
                _jobsViewModel.GetJobsCommand.Execute(null);
        }
    }
}
