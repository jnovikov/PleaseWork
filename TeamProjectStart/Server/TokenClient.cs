using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectStart
{
    class TokenClient
    {
        public static string Token { get; set; }

        public static HttpClient GetClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
            return client;
        }
    }
}
