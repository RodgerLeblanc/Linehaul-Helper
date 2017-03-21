using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Linehaul_Helper.Models
{
    class MasterPageItem
    {
        public string Title { get; set; }
        public ImageSource ImageSource { get; set; }
        public Type TargetType { get; set; }
        public ICommand Command { get; set; }
    }
}
