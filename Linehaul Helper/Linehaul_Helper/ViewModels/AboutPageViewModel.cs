using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.ViewModels
{
    class AboutPageViewModel : INotifyPropertyChanged
    {
        private string _applicationName;
        private string _applicationVersion;
        private string _authorName;
        private ImageSource _authorImageSource;
        private string _authorPresentation;

        public AboutPageViewModel()
        {
            ApplicationName = "Linehaul Helper";
            ApplicationVersion = "1.0";
            AuthorName = "CellNinja (Roger Leblanc)";
            AuthorImageSource = ImageSource.FromResource("roger_leblanc.jpg");
            AuthorPresentation = "I'm a truck driver since 2004, working as an LCV driver for Dicom since July 2015. You can find me every week nights between Drummondville and Quebec.";
        }

        public string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; OnPropertyChanged(); }
        }

        public string ApplicationVersion
        {
            get { return _applicationVersion; }
            set { _applicationVersion = value; OnPropertyChanged(); }
        }

        public string AuthorName
        {
            get { return _authorName; }
            set { _authorName = value; OnPropertyChanged(); }
        }

        public ImageSource AuthorImageSource
        {
            get { return _authorImageSource; }
            set { _authorImageSource = value; OnPropertyChanged(); }
        }

        public string AuthorPresentation
        {
            get { return _authorPresentation; }
            set { _authorPresentation = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
