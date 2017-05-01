using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Linehaul_Helper.Helpers;
using Linehaul_Helper.Models;
using Linehaul_Helper.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Linehaul_Helper.CustomEventArgs;

namespace Linehaul_Helper.Services
{
    public class PlatesDatabaseService : IPlatesDatabaseService
    {
        private String _baseUrl = ApiKeys.CloudantDbUrl + ApiKeys.FretPlatesDbName + "/";
        private HttpClient _client = new HttpClient();
        private IList<UnitInfo> _unitInfos;
        private DateTime _unitInfoListDateTime;
        private bool _isBusy;

        public event EventHandler UnitInfoListDateTimeChanged;
        public event EventHandler IsBusyChanged;

        public DateTime UnitInfoListDateTime
        {
            get { return _unitInfoListDateTime; }
            private set { _unitInfoListDateTime = value; OnUnitInfoListDateTimeChanged(value); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            private set { _isBusy = value; OnIsBusyChanged(value); }
        }

        public PlatesDatabaseService()
        {
            var authData = string.Format($"{ApiKeys.FretPlatesK}:{ApiKeys.FretPlatesP}");
            var authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorization);
        }

        public async Task SaveUnitInfos(List<UnitInfo> unitInfos, DateTime unitInfoListDateTime)
        {
            if (unitInfos == null)
                return;

            IsBusy = true;

            _unitInfos = unitInfos;
            UnitInfoListDateTime = unitInfoListDateTime;

            try
            {
                var unitInfoAsString = JsonConvert.SerializeObject(_unitInfos);
                await Helpers.PCLStorage.PCLStorageSave(Commons.Strings.UnitInfosFolderName, Commons.Strings.UnitInfosFileName, unitInfoAsString);

                GeneralSettingsObject settings = Settings.GetGeneralSettingsObject();
                settings.UnitInfoListDateTime = unitInfoListDateTime;
                Settings.SaveGeneralSettingsObject(settings);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception while converting List<UnitInfo> to string: " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task<List<UnitInfo>> GetUnitInfosOnline()
        {
            IsBusy = true;

            GeneralSettingsObject settings = Settings.GetGeneralSettingsObject();
            UnitInfoListDateTime = settings?.UnitInfoListDateTime ?? DateTime.Now.AddYears(-1);

            List<UnitInfo> documents = new List<UnitInfo>();
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
                        documents.Add(row.RowDoc.UnitInfo);
                    }
                    Debug.WriteLine($"Number of docs: {documents.Count}");
                }

                await this.SaveUnitInfos(documents, DateTime.Now);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception thrown: " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            return (List<UnitInfo>)_unitInfos ?? new List<UnitInfo>();
        }

        public async Task<List<UnitInfo>> GetUnitInfos()
        {
            IsBusy = true;

            try
            {
                var unitInfosFileContent = await Helpers.PCLStorage.PCLStorageLoad(Commons.Strings.UnitInfosFolderName, Commons.Strings.UnitInfosFileName);
                _unitInfos = JsonConvert.DeserializeObject<List<UnitInfo>>(unitInfosFileContent);

                GeneralSettingsObject settings = Settings.GetGeneralSettingsObject();
                UnitInfoListDateTime = settings?.UnitInfoListDateTime ?? DateTime.Now.AddYears(-1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception converting file to List<UnitInfo>: " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

            var isSavedDataOld = (DateTime.Now - UnitInfoListDateTime).TotalDays > 7;
            if ((_unitInfos != null) && (!isSavedDataOld))
            {
                return (List<UnitInfo>)_unitInfos;
            }

            return await this.GetUnitInfosOnline();
        }

        public async Task<bool> UpdateDocument(Document document)
        {
            IsBusy = true;

            var url = _baseUrl + document.Id;

            JObject documentToSend = new JObject();
            documentToSend["_id"] = document.Id;
            if (!String.IsNullOrEmpty(document.Rev))
                documentToSend["_rev"] = document.Rev;
            JObject unitInfo = new JObject();
            unitInfo["UnitNumber"] = document.UnitInfo.UnitNumber;
            unitInfo["PlateNumber"] = document.UnitInfo.PlateNumber;
            unitInfo["RevisionNeeded"] = document.UnitInfo.RevisionNeeded;
            unitInfo["RevisedPlateNumber"] = document.UnitInfo.RevisedPlateNumber;
            documentToSend["UnitInfo"] = unitInfo;

            var content = new StringContent(JsonConvert.SerializeObject(documentToSend), Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PutAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                return ((response != null) && (response.StatusCode == HttpStatusCode.Created));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception thrown: " + ex.Message);
                return false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected virtual void OnUnitInfoListDateTimeChanged(DateTime value)
        {
            UnitInfoListDateTimeChanged?.Invoke(this, 
                new UnitInfoListDateTimeEventArgs()
                    { UnitInfoListDateTime = value }
            );
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
