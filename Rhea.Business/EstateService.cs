using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Rhea.Data.Entities;

namespace Rhea.Business
{
    public class EstateService
    {
        private string host = "http://localhost:11500/";

        public IEnumerable<BuildingGroup> GetBuildingGroupList()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // List all products.
            HttpResponseMessage response = client.GetAsync("api/department").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!                
                var buildingGroups = response.Content.ReadAsAsync<IEnumerable<BuildingGroup>>().Result;
                //foreach (var p in departments)
                //{
                //    Console.WriteLine("{0}:{1};{2}", p.Id, p.Name);
                //}
                return buildingGroups;
            }
            else
            {
                //Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
        }
    }
}
