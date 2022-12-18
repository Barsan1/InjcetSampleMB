using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

namespace Core.Services
{
    internal class ComponentsService : IDisposable
    {
        private HttpClient client;
        
        public ComponentsService()
        {
            // Create new Http client 
            client= new HttpClient();

            // Add user agent
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(Constants.ProductInfoHeader, "1.0.0"));
        }


        public async Task<TreeResponse> GetTreeResponseAsync()
            => DeserializeData(await LoadSourceTreeAsync());


        public async Task<List<ComponentFather>> LoadComponentsFromJsonFileAsync(string path)
        {
            // Read data from file
            using (var sourceStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
            {
                var sb = new StringBuilder();

                // Assaign buffer
                byte[] buffer = new byte[0x1000];
                int numRead;

                // Read while buffer is not empty
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.Unicode.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                // Convert to json and return as new dictionary
                return JsonConvert.DeserializeObject<List<ComponentFather>>(sb.ToString());
            }
        }


        public async Task WriteComponentsToJsonFileAsync(string path, List<ComponentFather> componenets)
        {
            // Convert to json string
            var jsonString = JsonConvert.SerializeObject(componenets, Formatting.Indented);

            byte[] encodedText = Encoding.Unicode.GetBytes(jsonString);

            using (var sourceStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            }

        }


        private async Task<string> LoadSourceTreeAsync()
        {
            // Load data from http github url
            using (HttpResponseMessage message = await client.GetAsync(Constants.MudBlazor_Github_Url))
            {
                // Throw exception if failed
                if (!message.IsSuccessStatusCode)
                    throw new Exception($"Status code : {message.StatusCode}, Message : {message.Content}");

                // Return as string
                return await message.Content.ReadAsStringAsync();
            }
        }


        private TreeResponse DeserializeData(string jsonData)
            => JsonConvert.DeserializeObject<TreeResponse>(jsonData);
        

        /// <summary>
        /// Dispose http client after service has been end his life
        /// </summary>
        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
