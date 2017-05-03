using Linehaul_Helper.CustomEventArgs;
using Linehaul_Helper.Exceptions;
using Linehaul_Helper.Interfaces;
using Linehaul_Helper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.Services
{
    public class JobsRetrievalService : IJobsRetrievalService
    {
        private string _baseUrl = "http://api.indeed.com/ads/apisearch?publisher=480667076757286&q=company:dicom&format=json&latlong=1&co=ca&userip=1.2.3.4&useragent=Mozilla/%2F4.0%28Firefox%29&v=2&limit=100&sort=date";
        private bool _isBusy = false;

        public JobsRetrievalService()
        {
        }

        public event EventHandler IsBusyChanged;

        public async Task<List<IndeedJob>> GetJobsAsync()
        {
            SetIsBusy(true);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3 like Mac OS X) AppleWebKit/603.1.23 (KHTML, like Gecko) Version/10.0 Mobile/14E5239e Safari/602.1");

                    var response = await client.GetAsync(_baseUrl);
                    Debug.WriteLine(JsonConvert.SerializeObject(response));

                    SetIsBusy(false);
                    return await Deserialize(response);
                }
            }
            catch (Exception ex)
            {
                SetIsBusy(false);
                throw new IndeedJobsNotFoundException("Couldn't retrieve an answer from server", ex);
            }
        }

        async private Task<List<IndeedJob>> Deserialize(HttpResponseMessage response)
        {
            if ((response == null) || (response.StatusCode != HttpStatusCode.OK))
                throw new IndeedJobsNotFoundException("Error in server response");

            string httpAsString = "";
            try
            {
                httpAsString = await response.Content.ReadAsStringAsync();

                IndeedJsonResponse indeedJsonResponse = 
                    JsonConvert.DeserializeObject<IndeedJsonResponse>(httpAsString);
                return indeedJsonResponse.results;
            }
            catch (Exception ex)
            {
                throw new IndeedJobsNotFoundException("Can't deserialize response from server to Indeed jobs", httpAsString, ex);
            }
        }

        private void SetIsBusy(bool isBusy)
        {
            if (_isBusy != isBusy)
            {
                _isBusy = isBusy;
                IsBusyChanged?.Invoke(this, new IsBusyEventArgs { IsBusy = isBusy });
            }
        }
    }
}
