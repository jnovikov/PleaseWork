using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeamProjectStart.DTO;
using TeamProjectStart.Helpers;

namespace TeamProjectStart
{
    class ApiData
    {
        public async Task<List<Deadline>> GetDeadlines()
        {
            using (var client = TokenClient.GetClient())
            {
                var response = await client.GetAsync("http://pw.heck.today/api/deadlines");
                var responseString = await response.Content.ReadAsStringAsync();
                var deadlines = Deadline.ListFromJson(responseString);
                return deadlines;
            }
        }

        public async Task<ApiError> AddTask(string name, DateTime finish)
        {
            using (var client = TokenClient.GetClient())
            {
                var values = new Dictionary<string, string>
                {
                     { "name", name },
                     { "finish", finish.ToString() },
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("http://pw.heck.today/api/deadlines",content);
                var error = await ApiError.FromResponse(response);
                return error;
            }
        }
    }
}
