using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linehaul_Helper.ViewModels;
using Linehaul_Helper.Models;
using Linehaul_Helper.Exceptions;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Linehaul_Helper.UnitTest.Tests
{
    [TestClass]
    public class PlateNumberPageViewModelUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(LayoutException))]
        public void DoneCommandShouldExitCurrentScreen()
        {
            var pnpvm = new PlateNumberPageViewModel(new UnitInfo());
            if (pnpvm.DoneCommand.CanExecute(null))
                pnpvm.DoneCommand.Execute(null);
        }
    }
}
