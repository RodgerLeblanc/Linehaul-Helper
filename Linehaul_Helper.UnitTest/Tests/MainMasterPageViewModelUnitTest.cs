using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linehaul_Helper.ViewModels;
using Linehaul_Helper.Helpers;
using Xamarin.Forms;
using System.Threading;

namespace Linehaul_Helper.UnitTest.Tests
{
    [TestClass]
    public class MainMasterPageViewModelUnitTest
    {
        [TestMethod]
        public void LoadPageCommandSendPageSelectedMessageToMessageCenter()
        {
            // Arrange
            var vm = new MainMasterPageViewModel();
            bool messageReceived = false;
            var dumbPage = new Page();

            MessagingCenter.Subscribe<MainMasterPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, (source, page) =>
            {
                messageReceived = true;
            });


            // Act
            vm.LoadPageCommand.Execute(dumbPage.GetType());

            // Assert
            Assert.IsTrue(messageReceived);
        }
    }
}
