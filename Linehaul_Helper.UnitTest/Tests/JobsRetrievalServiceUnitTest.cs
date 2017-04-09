using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linehaul_Helper.Services;
using Linehaul_Helper.CustomEventArgs;
using Linehaul_Helper.Interfaces;
using Linehaul_Helper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Linehaul_Helper.Exceptions;

namespace Linehaul_Helper.UnitTest.Tests
{
    [TestClass]
    public class JobsRetrievalServiceUnitTest
    {
        [TestMethod]
        public void GetJobsAsyncShouldSetIsBusyToTrueThenFalse()
        {
            // Assign
            var isBusyChangedToTrue = false;
            var isBusyChangedToFalse = false;

            var jrs = new JobsRetrievalService();
            jrs.IsBusyChanged += (sender, args) =>
            {
                var ibea = args as IsBusyEventArgs;
                if (ibea.IsBusy)
                    isBusyChangedToTrue = true;
                else
                    isBusyChangedToFalse = true;
            };

            // Act
            var result = jrs.GetJobsAsync().Result;

            // Assert
            Assert.IsTrue(isBusyChangedToTrue);
            Assert.IsTrue(isBusyChangedToFalse);
        }

        [TestMethod]
        public void GetJobsAsyncShouldReturnAListOfJobs()
        {
            var jrs = new JobsRetrievalService();
            var result = jrs.GetJobsAsync().Result;
            Assert.IsInstanceOfType(result, typeof(List<IndeedJob>));
        }

        [TestMethod]
        [ExpectedException(typeof(IndeedJobsNotFoundException))]
        public void GetJobsAsyncShouldThrowAnExceptionWhenNotConnected()
        {
            var jrs = new NoInternetJobsRetrievalService();
            var result = jrs.GetJobsAsync().Result;
        }

        private class DummyJobsRetrievalService : IJobsRetrievalService
        {
            public event EventHandler IsBusyChanged;

            public Task<List<IndeedJob>> GetJobsAsync()
            {
                return Task.FromResult<List<IndeedJob>>(new List<IndeedJob>());
            }
        }

        private class NoInternetJobsRetrievalService : IJobsRetrievalService
        {
            public event EventHandler IsBusyChanged;

            public Task<List<IndeedJob>> GetJobsAsync()
            {
                throw new IndeedJobsNotFoundException("No internet");
            }
        }
    }
}
