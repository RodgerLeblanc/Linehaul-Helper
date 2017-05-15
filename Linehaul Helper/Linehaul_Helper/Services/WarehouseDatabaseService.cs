using Linehaul_Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linehaul_Helper.Models;
using Linehaul_Helper.Helpers;
using System.Net.Http;
using Linehaul_Helper.CustomEventArgs;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

namespace Linehaul_Helper.Services
{
    class WarehouseDatabaseService : IWarehouseDatabaseService
    {
        private String _baseUrl = ApiKeys.CloudantDbUrl + ApiKeys.WarehouseLocationsDbName + "/";
        private HttpClient _client = new HttpClient();
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            private set { _isBusy = value; OnIsBusyChanged(value); }
        }

        public WarehouseDatabaseService()
        {
            var authData = string.Format($"{ApiKeys.WarehouseLocationsK}:{ApiKeys.WarehouseLocationsP}");
            var authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorization);
        }

        public event EventHandler IsBusyChanged;

        public List<WarehouseLocation> GetDefaultWarehouseLocations()
        {
            return new List<WarehouseLocation>
            {
                new WarehouseLocation
                {
                    Name = "Dicom Drummondville",
                    Description = "DRU",
                    Position = new Position { Latitude = 45.877198, Longitude = -72.543418 },
                    Address = "330 RUE ROCHELEAU, DRUMMONDVILLE, QC, J2C7S7"
                },
                new WarehouseLocation
                {
                    Name = "Dicom Trois-Rivieres",
                    Description = "TRS",
                    Position = new Position { Latitude = 46.345096, Longitude = -72.638812 },
                    Address = "3700 L-P NORMAND, TROIS-RIVIERES, QC"
                },
                new WarehouseLocation
                {
                    Name = "Dicom Quebec",
                    Description = "QUE",
                    Position = new Position { Latitude = 46.792657, Longitude = -71.323205 },
                    Address = "5150 JOHN-MOLSON, QUEBEC, QC, G1X3X4"
                },
                new WarehouseLocation
                {
                    Name = "Dicom Cote-De-Liesse",
                    Description = "CDL",
                    Position = new Position { Latitude = 45.463108, Longitude = -73.722380 },
                    Address = "10755 COTE DE LIESSE OUEST, MONTREAL, QC, H9P1A7"
                }
            };
        }

        public async Task<List<WarehouseLocation>> GetWarehouseLocations()
        {
            IsBusy = true;

            List<WarehouseLocation> warehouses = new List<WarehouseLocation>();
            var allDocsAsString = String.Empty;

            try
            {
                Debug.WriteLine("Get docs online");
                var url = _baseUrl + "_all_docs?include_docs=true";
                var response = await _client.GetAsync(url);

                if ((response != null) && (response.StatusCode == HttpStatusCode.OK))
                {
                    allDocsAsString = await response.Content.ReadAsStringAsync();
                }

                var cloudantResponse = JsonConvert.DeserializeObject<WarehouseDatabaseResponse.RootObject>(allDocsAsString) as WarehouseDatabaseResponse.RootObject;

                if (cloudantResponse != null)
                {
                    foreach (var row in cloudantResponse.rows)
                    {
                        warehouses.Add(row.doc);
                    }
                    Debug.WriteLine($"Number of docs: {warehouses.Count}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception thrown: " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

//            return (warehouses.Count == 0) ? GetDefaultWarehouseLocations() : warehouses;
            return warehouses ?? new List<WarehouseLocation>();
        }

        protected virtual void OnIsBusyChanged(bool value)
        {
            IsBusyChanged?.Invoke(this,
                new IsBusyEventArgs()
                { IsBusy = value }
            );
        }
    }
}
