using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UpdateICA
{
    public class Envelope<TEntity>
    {
        [JsonProperty(PropertyName = "data")]
        public Data<TEntity> Data { get; set; }
    }

    public class Data<TEntity>
    {
        [JsonProperty(PropertyName = "attributes")]
        public TEntity Attributes { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }

    public class Provider
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "x")]
        public double X { get; set; }

        [JsonProperty(PropertyName = "y")]
        public double Y { get; set; }

        [JsonProperty(PropertyName = "visitingAddress")]
        public string VisitingAddress { get; set; }

        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "postTown")]
        public string PostTown { get; set; }

        [JsonIgnore]
        public string TypeName => "provider";

        // Note that this is a temporary attribute
        public string ServiceType { get; set; }
    }
    public class Service
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "providerId")]
        public int providerId { get; set; }

        [JsonProperty(PropertyName = "servicePrincipalId")]
        public int servicePrincipalId { get; set; }

        [JsonProperty(PropertyName = "serviceTypeId")]
        public int serviceTypeId { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public Dictionary<string, object> properties { get; set; }

        [JsonProperty(PropertyName = "forms")]
        public string[] forms { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool active { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int year { get; set; }

        [JsonProperty(PropertyName = "timelinessDate")]
        public DateTime timelinessDate { get; set; }

        [JsonIgnore]
        public string TypeName => "service";
    }

    public class ServiceCollection
    {
        public List<Service> data { get; set; }
    }
}
