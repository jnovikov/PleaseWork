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
    class AuthController
    {
        public async Task<ApiError> Register(string email, string password, string name)
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                     { "email", email },
                     { "password", password },
                     { "name", name }
                };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("http://pw.heck.today/register", content);
                return await ApiError.FromResponse(response);
            }
        }

        public string Login(string email, string password, out ApiError error)
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                     { "email", email },
                     { "password", password },
                };
                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync("http://pw.heck.today/login", content).Result;
                var contentMessage = response.Content.ReadAsStringAsync().Result;

                error = ApiError.FromResponse(response).Result;
                if (error == null)
                {
                    return UserToken.FromJson(contentMessage)?.AccessToken;
                }
                return "";
            }
                
        }
    }
}
