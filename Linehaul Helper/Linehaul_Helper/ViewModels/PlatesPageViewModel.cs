using Linehaul_Helper.CustomEventArgs;
using Linehaul_Helper.Helpers;
using Linehaul_Helper.Interfaces;
using Linehaul_Helper.Localization;
using Linehaul_Helper.Models;
using Linehaul_Helper.Services;
using Linehaul_Helper.Views;
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
    class PlatesPageViewModel : INotifyPropertyChanged
    {
        private IDatabaseService _dbService;
        private bool _isBusy;
        private List<UnitInfo> _unitInfos;
        private DateTime _unitInfoListDateTime;
        private ICommand _command;
        private ICommand _addUnitCommand;
        private ICommand _findUnitCommand;
        private string _unitNumber;
        private PlatesPageModel _platesPageModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public PlatesPageViewModel(IDatabaseService dbService)
        {
            _dbService = dbService ?? throw new ArgumentNullException();

            _dbService.UnitInfoListDateTimeChanged += (sender, args) =>
            {
                var unitInfoListDateTimeEventArgs = args as UnitInfoListDateTimeEventArgs;
                UnitInfoListDateTime = unitInfoListDateTimeEventArgs.UnitInfoListDateTime;
            };

            _dbService.IsBusyChanged += (sender, args) =>
            {
                var isBusyEventArgs = args as IsBusyEventArgs;
                IsBusy = isBusyEventArgs.IsBusy;
            };

            UpdateCommand = new Command(async () =>
            {
                _unitInfos = await _dbService.GetUnitInfosOnline();
                UnitInfoListDateTime = _dbService.UnitInfoListDateTime;
            });

            AddUnitCommand = new Command(async () =>
            {
                if (String.Equals(UnitNumber, "123"))
                {
                    await Commons.DetailNavigationPushAsync(new UnitInfoCreationPage(_dbService));
                    UnitNumber = "";
                }
            });

            FindUnitCommand = new Command(async () =>
            {
                await FindUnitInfo(UnitNumber, true);
            });

            Task.Run(() =>
            {
                var result = _dbService.GetUnitInfos();
                result.Wait();
                UnitInfos = result.Result;
            });
        }

        public ICommand UpdateCommand
        {
            get { return _command; }
            private set { _command = value; OnPropertyChanged(); }
        }

        public ICommand AddUnitCommand
        {
            get { return _addUnitCommand; }
            private set { _addUnitCommand = value; OnPropertyChanged(); }
        }

        public ICommand FindUnitCommand
        {
            get { return _findUnitCommand; }
            private set { _findUnitCommand = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            private set { _isBusy = value; OnPropertyChanged(); }
        }

        public List<UnitInfo> UnitInfos
        {
            get { return _unitInfos; }
            private set { _unitInfos = value; OnPropertyChanged(); }
        }

        public DateTime UnitInfoListDateTime
        {
            get { return _unitInfoListDateTime; }
            private set { _unitInfoListDateTime = value; OnPropertyChanged(); }
        }

        public string UnitNumber
        {
            get { return _unitNumber; }
            set {
                _unitNumber = value;
                OnPropertyChanged();

                if (_unitNumber.Length == 4)
                {
                    if (FindUnitCommand.CanExecute(null))
                        FindUnitCommand.Execute(null);
                }
            }
        }

        public PlatesPageModel PlatesPageModel
        {
            get { return _platesPageModel; }
            private set { _platesPageModel = value; OnPropertyChanged(); }
        }

        async Task FindUnitInfo(string unit, bool showErrorDialogOnFail)
        {
            try
            {
                if (!Int32.TryParse(unit, out int unitNumber))
                    throw new Exception("Cannot convert unit #" + unit + " to a number.");

                var unitInfo = _unitInfos.SingleOrDefault(u => u.UnitNumber == unitNumber);
                if (unitInfo != null)
                {
                    await Commons.DetailNavigationPushAsync(new PlateNumberPage(unitInfo));
                    UnitNumber = "";
                }
                else
                {
                    if (showErrorDialogOnFail)
                    {
                        await Commons.DisplayAlert(AppResources.PlatesPageButtonErrorTitle, AppResources.PlatesPageButtonErrorMessage, AppResources.PlatesPageButtonErrorCancel);
                        //var result = await Application.Current.MainPage.DisplayAlert(AppResources.FretPlatesPageButtonErrorTitle, AppResources.FretPlatesPageButtonErrorMessage, AppResources.FretPlatesPageButtonErrorConfirm, AppResources.FretPlatesPageButtonErrorCancel);
                        //Debug.WriteLine($"result:{result}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception thrown: " + ex.Message);
            }
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
