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
        private IList<WarehouseLocation> _warehouses;
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

        public async Task<List<WarehouseLocation>> GetDefaultWarehouseLocations()
        {
            if (_warehouses != null)
                return (List<WarehouseLocation>)_warehouses;

            IsBusy = true;

            try
            {
                var warehousesFileContent = await Helpers.PCLStorage.PCLStorageLoad(Commons.Strings.WarehousesFolderName, Commons.Strings.WarehousesFileName);
                _warehouses = JsonConvert.DeserializeObject<List<WarehouseLocation>>(warehousesFileContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception converting file to List<WarehouseLocation>: " + ex.Message);
                _warehouses = new List<WarehouseLocation>();
            }
            finally
            {
                IsBusy = false;
            }

            return (List<WarehouseLocation>)_warehouses;
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

                    await this.SaveWarehouses(warehouses);
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

            return warehouses ?? await GetDefaultWarehouseLocations();
        }

        public async Task SaveWarehouses(List<WarehouseLocation> warehouses)
        {
            if (warehouses == null)
                return;

            IsBusy = true;

            _warehouses = warehouses;
            
            try
            {
                var warehousesAsString = JsonConvert.SerializeObject(_warehouses);
                await Helpers.PCLStorage.PCLStorageSave(Commons.Strings.WarehousesFolderName, Commons.Strings.WarehousesFileName, warehousesAsString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception while converting List<Warehouses> to string: " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
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
