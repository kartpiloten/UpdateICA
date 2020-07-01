using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace UpdateICA
{
    public class CApiConnector
    {
        static readonly string BASE_URL = "https://serviceapi.pipos.se/testapi/";
        //static readonly string BASE_URL = "http://192.168.80.214/serviceapi";

        public static string AcquireToken(string username, string password)
        {
            string token = string.Empty;
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                string HtmlResult = client.UploadString(BASE_URL + "/Token",
                    $"grant_type=password&username={username}&password={password}");
                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(HtmlResult);

                if (json.ContainsKey("access_token"))
                {
                    token = json["access_token"] as string;
                }
            }
            return token;
        }
        public static Provider CreateProvider(Provider provider, string token)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, string.Format("Bearer {0}", token));
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers.Add(HttpRequestHeader.Accept, "application/json");

                var envelope = new Envelope<Provider>
                {
                    Data = new Data<Provider>
                    {
                        Attributes = provider,
                        Type = "provider"
                    }
                };

                var json = JsonConvert.SerializeObject(envelope);
                try
                {
                    var result = client.UploadString(BASE_URL + "/api/providers/", json);
                    var newProvider = JsonConvert
                        .DeserializeObject<Envelope<Provider>>(result)
                        .Data
                        .Attributes;
                    return newProvider;
                }
                catch
                {
                    using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"Z:\Projekt 2020\Pipon\Servicedata\NoProvider.txt", true))
                    {
                        file.WriteLine(json);
                        file.Close();
                    }
                    return null;
                }
            }
        }
        public static Service CreateService(Service service, string token)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, string.Format("Bearer {0}", token));
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers.Add(HttpRequestHeader.Accept, "application/json");

                var envelope = new Envelope<Service>
                {
                    Data = new Data<Service>
                    {
                        Attributes = service,
                        Type = "service"
                    }
                };

                var json = JsonConvert.SerializeObject(envelope);
                //try
                {
                    var result = client.UploadString(BASE_URL + "/api/services/", json);
                    var newService = JsonConvert
                        .DeserializeObject<Envelope<Service>>(result)
                        .Data
                        .Attributes;
                    return newService;
                }
                /*catch
                {
                    using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"Z:\Projekt 2020\Pipon\Servicedata\NoService.txt", true))
                    {
                        file.WriteLine(json);
                        file.Close();
                    }
                    return null;
                }*/
            }
        }
        public static Provider FindProvider(int providerId, string token)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, string.Format("Bearer {0}", token));
                using (var data = client.OpenRead(BASE_URL + $"/api/providers/{providerId}"))
                using (var reader = new StreamReader(data))
                {
                    
                    var provider = JsonConvert
                        .DeserializeObject<Envelope<Provider>>(reader.ReadToEnd())
                        .Data
                        .Attributes;
                    return provider;
                }
            }
        }
        public static ServiceCollection FindService(int principalId, string token)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, string.Format("Bearer {0}", token));
                using (var data = client.OpenRead(BASE_URL + $"/api/services?principalId={principalId}"))
                using (var reader = new StreamReader(data))
                {
                    string aJSONstring = reader.ReadToEnd();
                    ServiceCollection serviceList = JsonConvert
                        .DeserializeObject<Envelope<ServiceCollection>>(aJSONstring)
                        .Data
                        .Attributes;
                    return serviceList;
                }
            }
        }
    }
}