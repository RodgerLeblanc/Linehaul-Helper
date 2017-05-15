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
    class AboutPageViewModel : BaseViewModel
    {
        private string _applicationName;
        private string _applicationVersion;
        private string _authorName;
        private ImageSource _authorImageSource;
        private string _authorPresentation;
        private ICommand _openUriCommand;

        public AboutPageViewModel()
        {
            ApplicationName = "Linehaul Helper";
            ApplicationVersion = "1.0";
            AuthorName = "CellNinja (Roger Leblanc)";
            AuthorImageSource = ImageSource.FromResource("roger_leblanc.jpg");
            AuthorPresentation = "https://github.com/RodgerLeblanc";
            //AuthorPresentation = "I'm a truck driver since 2004, working as an LCV driver for Dicom since July 2015. You can find me every week nights between Drummondville and Quebec.";

            OpenUriCommand = new Command(p => Device.OpenUri(new Uri(p as string)));
        }

        public string ApplicationName
        {
            get { return _applicationName; }
            set { SetProperty(ref _applicationName, value); }
        }

        public string ApplicationVersion
        {
            get { return _applicationVersion; }
            set { SetProperty(ref _applicationVersion, value); }
        }

        public string AuthorName
        {
            get { return _authorName; }
            set { SetProperty(ref _authorName, value); }
        }

        public ImageSource AuthorImageSource
        {
            get { return _authorImageSource; }
            set { SetProperty(ref _authorImageSource, value); }
        }

        public string AuthorPresentation
        {
            get { return _authorPresentation; }
            set { SetProperty(ref _authorPresentation, value); }
        }

        public ICommand OpenUriCommand
        {
            get { return _openUriCommand; }
            private set { SetProperty(ref _openUriCommand, value); }
        }

        public string ContactUsUri {
            get
            {
                return $"mailto:info@cellninja.ca?subject={ApplicationName}%20{ApplicationVersion}%20Support&body=Hi%20{AuthorName},";
            }
        }
    }
}
