using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProjectStart.DTO;

namespace TeamProjectStart
{
    public class AuthFile
    {
        public const string DataDirectory = "Data";
        public static string AuthPath => Path.Combine(DataDirectory, "auth.json");

        public static void Save(string token, string email)
        {
            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }
            var tokenObject = new UserToken { AccessToken = token, Username = email };
            using (var sw = new StreamWriter(AuthPath)) { 
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, tokenObject);
                }
            }
        }

        public static string GetToken()
        {
            try
            {
                using (var sr = new StreamReader(AuthPath))
                {
                    using (var jsonReader = new JsonTextReader(sr))
                    {
                        var serializer = new JsonSerializer();
                        var userToken = serializer.Deserialize<UserToken>(jsonReader);
                        return userToken?.AccessToken;
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        public static void Flush()
        {
            File.Delete(AuthPath);
        }
    }
}
