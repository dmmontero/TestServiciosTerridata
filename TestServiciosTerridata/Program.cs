using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization;
using System;
using System.Collections.Generic;
using TestServiciosTerridata.models;

namespace TestServiciosTerridata
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://geoservicios.upra.gov.co/arcgis/rest/services/ordenamiento_productivo/frontera_agricola_abril_2018/MapServer/0/query");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

            request.AddParameter("f", "json"); // adds to POST or URL querystring based on Method
            request.AddParameter("outFields", "cod_depart,departamen,area_ha,elemento"); // adds to POST or URL querystring based on Method
            request.AddParameter("outSr", 4326); // adds to POST or URL querystring based on Method
            request.AddParameter("returnGeometry", false); // adds to POST or URL querystring based on Method
            request.AddParameter("where", "elemento = 'Frontera agrícola nacional'"); // adds to POST or URL querystring based on Method

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // execute the request
            IRestResponse response = client.Execute(request);
            //IRestResponse response = client.Execute<Features>(request);
            var content = response.Content; // raw content as string

            client.UseJson();
            var response2 = client.Execute<List<Attributes>>(request);
            if (response2.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var terridateException = new ApplicationException(message, response2.ErrorException);
                throw terridateException;
            }
            var _data = response2.Data;
        }

        public class JsonNetSerializer : IRestSerializer
        {
            public string Serialize(object obj) =>
                JsonConvert.SerializeObject(obj);

            public string Serialize(Parameter parameter) =>
                JsonConvert.SerializeObject(parameter.Value);

            public T Deserialize<T>(IRestResponse response) =>
                JsonConvert.DeserializeObject<T>(response.Content);

            public string[] SupportedContentTypes { get; } =
            {
                "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
            };

            public string ContentType { get; set; } = "application/json";

            public DataFormat DataFormat { get; } = DataFormat.Json;
        }
    }
}
