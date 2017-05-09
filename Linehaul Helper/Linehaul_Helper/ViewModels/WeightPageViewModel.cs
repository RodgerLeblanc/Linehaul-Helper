using Linehaul_Helper.Helpers;
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
    class WeightPageViewModel : BaseViewModel
    {
        public ICommand TruckSelectedCommand {
            get
            {
                return new Command<string>((c) =>
                {
                    Page page = null;
                    if (c.Length == 1)
                        page = new WeightPageForSingleCombination(c);
                    else
                        page = new WeightPageForSingleCombination(c);

                    MessagingCenter.Send<WeightPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, page);
                });
            }
        }
    }
}
