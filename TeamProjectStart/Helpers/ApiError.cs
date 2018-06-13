using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace TeamProjectStart.Helpers
{
    class ApiError
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public ApiError(int code, string message) 
        {
            StatusCode = code;
            ErrorMessage = message;
        }

        public async static Task<ApiError> FromResponse(HttpResponseMessage message)
        {
            var code = (int)message.StatusCode;
            if (code != 200) {
                var content = await message.Content.ReadAsStringAsync();
                try
                {
                    var error = JsonConvert.DeserializeObject<ErrorMessage>(content);
                    return new ApiError(code, error.Message);
                } catch (Newtonsoft.Json.JsonReaderException)
                {
                    return new ApiError(code, content);
                }
                
            }
            return null;
        }
    }


    class ErrorMessage
    {
        public string Message
        {
            get; set;
        }
    }
}
