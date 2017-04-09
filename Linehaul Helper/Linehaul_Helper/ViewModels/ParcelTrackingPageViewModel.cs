using Linehaul_Helper.CustomEventArgs;
using Linehaul_Helper.Interfaces;
using Linehaul_Helper.Models;
using Linehaul_Helper.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Linehaul_Helper.ViewModels
{
    public class ParcelTrackingPageViewModel : INotifyPropertyChanged
    {
        private IParcelTrackingService _parcelTrackingService;
        private string _trackingNumber;
        private ICommand _trackCommand;
        private bool _isBusy = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public ParcelTrackingPageViewModel(IParcelTrackingService parcelTrackingService)
        {
            _parcelTrackingService = parcelTrackingService;
            _parcelTrackingService.IsBusyChanged += (source, args) =>
            {
                IsBusy = (args as IsBusyEventArgs).IsBusy;
            };

            TrackingNumber = "AA0309024";

            TrackCommand = new Command(async () =>
            {
                ParcelTrackingModel parcelTracking = await _parcelTrackingService.Track(_trackingNumber);

                string title = parcelTracking.TrackingNumber;
                string message = $"Tracking: {parcelTracking.TrackingNumber}\n" +
                    (!String.IsNullOrWhiteSpace(parcelTracking.Status)
                        ? $"Status: {parcelTracking.Status}\n" : "") +
                    (!String.IsNullOrWhiteSpace(parcelTracking.Division)
                        ? $"Division: {parcelTracking.Division}\n" : "") +
                    (!String.IsNullOrWhiteSpace(parcelTracking.LastUpdateString)
                        ? $"Last update: {parcelTracking.LastUpdateString}\n" : "") +
                    (!String.IsNullOrWhiteSpace(parcelTracking.Status)
                        ? $"Notes: {parcelTracking.Notes}" : "");
                await Application.Current.MainPage.DisplayAlert(title, message, "Ok");
            });
        }

        public string TrackingNumber
        {
            get { return _trackingNumber; }
            set { _trackingNumber = value; OnPropertyChanged(); }
        }

        public ICommand TrackCommand
        {
            get { return _trackCommand; }
            private set { _trackCommand = value; OnPropertyChanged(); }
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
