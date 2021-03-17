using Newtonsoft.Json;
using Autofocus.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Autofocus.Repository
{
    public class RepositoryAPI<T> : IRepositoryAPI<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;

        public RepositoryAPI(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<bool> CreateAsync(string url, T objToCreate)//, string token=""
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if(objToCreate!=null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _clientFactory.CreateClient();
            //if (token != null && token.Length != 0)
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //}


            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Created|| response.StatusCode== System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string url, int Id)//, string token=""
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url+Id);

            var client = _clientFactory.CreateClient();
            //if (token != null && token.Length != 0)
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //}
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url)//,string token=""
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = _clientFactory.CreateClient();
            //if (token!=null &&  token.Length != 0)
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //}
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }

            return null;
        }

        public async Task<T> GetAsync(string url, int Id)//, string token=""
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url+Id);

            var client = _clientFactory.CreateClient();
            //if (token != null && token.Length != 0)
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //}
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }

            return null;
        }

        public async Task<bool> UpdateAsync(string url, T objToUpdate)//,string token=""
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url);
            if (objToUpdate != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(objToUpdate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _clientFactory.CreateClient();
            //if (token != null && token.Length != 0)
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //}
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
