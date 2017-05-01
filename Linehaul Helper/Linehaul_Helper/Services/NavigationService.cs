using Linehaul_Helper.Helpers;
using Linehaul_Helper.Interfaces;
using Linehaul_Helper.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.Services
{
    public class NavigationService : INavigationService
    {
        public void SubscribeToMessagingCenter()
        {
            MessagingCenter.Subscribe<MainPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, async (source, page) =>
            {
                await NavigationHelper.NavigationPushAsync(page);
            });

            MessagingCenter.Subscribe<JobsPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, async (source, page) =>
            {
                await NavigationHelper.NavigationPushAsync(page);
            });
        }

        public void UnsubscribeToMessagingCenter()
        {
            MessagingCenter.Unsubscribe<JobsPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage);
            MessagingCenter.Unsubscribe<MainPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage);
        }
    }
}
