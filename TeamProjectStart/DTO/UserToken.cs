using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectStart.DTO
{
    class UserToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        public string Username { get; set; }

        public static UserToken FromJson(string json)
        {
            try
            {
                var token = JsonConvert.DeserializeObject<UserToken>(json);
                return token;
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                return null;
            }
        }
    }

}
