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
        private String _baseUrl = ApiKeys.CloudantDbUrl + ApiKeys.FretPlatesDbName + "/";
        private HttpClient _client = new HttpClient();
        private IList<WarehouseLocation> _warehouseLocations;
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            private set { _isBusy = value; OnIsBusyChanged(value); }
        }

        public WarehouseDatabaseService()
        {
            var authData = string.Format($"{ApiKeys.FretPlatesK}:{ApiKeys.FretPlatesP}");
            var authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorization);
        }

        public event EventHandler IsBusyChanged;

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
                //Debug.WriteLine($"response: {JsonConvert.SerializeObject(response)}");

                if ((response != null) && (response.StatusCode == HttpStatusCode.OK))
                {
                    allDocsAsString = await response.Content.ReadAsStringAsync();
                    //Debug.WriteLine($"allDocsAsString: {allDocsAsString}");
                }

                var cloudantResponse = JsonConvert.DeserializeObject<CloudantResponse>(allDocsAsString) as CloudantResponse;
                //Debug.WriteLine($"cloudantResponse: {JsonConvert.SerializeObject(cloudantResponse)}");

                if (cloudantResponse != null)
                {
                    foreach (var row in cloudantResponse.Rows)
                    {
                        //warehouses.Add(row.RowDoc.UnitInfo);
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

            return (List<WarehouseLocation>)_warehouseLocations ?? new List<WarehouseLocation>();
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
