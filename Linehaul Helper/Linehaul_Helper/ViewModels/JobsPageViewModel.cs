using Linehaul_Helper.CustomEventArgs;
using Linehaul_Helper.Helpers;
using Linehaul_Helper.Interfaces;
using Linehaul_Helper.Models;
using Linehaul_Helper.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Linehaul_Helper.ViewModels
{
    public class JobsPageViewModel : INotifyPropertyChanged
    {
        private IJobsRetrievalService _jobsRetrievalService;
        private List<IndeedJob> _jobs;
        private IndeedJob _selectedItem;
        private bool _isBusy = false;

        public JobsPageViewModel(IJobsRetrievalService jobsRetrievalService)
        {
            _jobsRetrievalService = jobsRetrievalService;
            _jobsRetrievalService.IsBusyChanged += (source, args) =>
            {
                IsBusy = (args as IsBusyEventArgs).IsBusy;
            };

            if (GetJobsCommand.CanExecute(null))
                GetJobsCommand.Execute(null);
        }

        public JobsPageViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GetJobsCommand {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        Jobs = await _jobsRetrievalService.GetJobsAsync();
                    }
                    catch (Exception ex)
                    {
                        await Commons.DisplayAlert("Error", ex.Message, "Ok");
                    }
                });
            }
        }

        public ICommand DisplayJob
        {
            get
            {
                return new Command<IndeedJob>((j) =>
                {
                    MessagingCenter.Send<JobsPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, new IndeedJobWebViewPage(j));
                });
            }
        }

        public List<IndeedJob> Jobs
        {
            get { return _jobs; }
            private set { _jobs = value; OnPropertyChanged(); }
        }

        public IndeedJob SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                Debug.WriteLine("_selectedItem: " + JsonConvert.SerializeObject(_selectedItem));

                if (_selectedItem == null)
                    return;

                if (DisplayJob.CanExecute(_selectedItem))
                    DisplayJob.Execute(_selectedItem);

                SelectedItem = null;
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            private set { _isBusy = value; OnPropertyChanged(); }
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
