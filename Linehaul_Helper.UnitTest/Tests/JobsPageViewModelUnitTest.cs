using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linehaul_Helper.ViewModels;
using Xamarin.Forms;
using Linehaul_Helper.Helpers;
using Linehaul_Helper.Interfaces;
using Linehaul_Helper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linehaul_Helper.UnitTest.Tests
{
    [TestClass]
    public class JobsPageViewModelUnitTest
    {
        [TestMethod]
        public void GetJobsCommandShouldReceiveAListOfJobsFromService()
        {
            // Arrange
            var jobsRetrievalService = new DummyJobsRetrievalService();

            // Act
            var listOfJobs = jobsRetrievalService.GetJobsAsync().Result;

            // Assert
            Assert.IsInstanceOfType(listOfJobs, typeof(List<IndeedJob>));
        }

        [TestMethod]
        public void DisplayJobCommandSendPageSelectedMessageToMessageCenter()
        {
            // Arrange
            var vm = new JobsPageViewModel(new DummyJobsRetrievalService());
            bool messageReceived = false;
            var dumbPage = new Page();

            MessagingCenter.Subscribe<JobsPageViewModel, Page>(this, Commons.Strings.PageSelectedMessage, (source, page) =>
            {
                messageReceived = true;
            });


            // Act
            vm.DisplayJob.Execute(new IndeedJob());

            // Assert
            Assert.IsTrue(messageReceived);
        }



        private class DummyJobsRetrievalService : IJobsRetrievalService
        {
            public event EventHandler IsBusyChanged;

            public Task<List<IndeedJob>> GetJobsAsync()
            {
                return Task.FromResult<List<IndeedJob>>(new List<IndeedJob>());
            }
        }
    }
}
