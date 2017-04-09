using Linehaul_Helper.Helpers;
using Linehaul_Helper.Interfaces;
using Linehaul_Helper.Models;
using Linehaul_Helper.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Linehaul_Helper.Views
{
    public partial class UnitInfoCreationPage : ContentPage
    {
        private IPlatesDatabaseService _dbService = null;

        public UnitInfoCreationPage(IPlatesDatabaseService dbService)
        {
            _dbService = dbService ?? throw new ArgumentNullException();

            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
        }

        void Handle_Completed(object sender, System.EventArgs e)
        {
            Handle_Add_Clicked(sender, e);
        }

        async void Handle_Add_Clicked(object sender, System.EventArgs e)
        {
            if (!String.IsNullOrEmpty(EntryUnitNumber.Text) && !String.IsNullOrEmpty(EntryPlateNumber.Text))
            {
                try
                {
                    var unitNumber = int.Parse(EntryUnitNumber.Text);
                    var created = await _dbService.UpdateDocument(new Document
                    {
                        Id = EntryUnitNumber.Text,
                        UnitInfo = new UnitInfo
                        {
                            UnitNumber = unitNumber,
                            PlateNumber = EntryPlateNumber.Text.ToUpper(),
                            RevisionNeeded = false,
                            RevisedPlateNumber = ""
                        }
                    });
                    Debug.WriteLine($"created? {created}");

                    if (created)
                    {
                        unitNumber++;
                        EntryUnitNumber.Text = unitNumber.ToString();
                        //EntryPlateNumber.Text = "";
                        EntryPlateNumber.Focus();
                    }
                }
                catch (Exception ex)
                {
                    await Commons.DisplayAlert("Error", ex.Message, "Ok");
                }
            }
        }

        async void Handle_Done_Clicked(object sender, System.EventArgs e)
        {
            await Commons.DetailNavigationPopAsync();
        }
    }
}
