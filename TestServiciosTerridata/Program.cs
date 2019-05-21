using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using TestServiciosTerridata.models;

namespace TestServiciosTerridata
{
    class Program
    {
        static void Main(string[] args)
        {
            ObtenerdataFronteraAgricola();
            //ObtenerDataSinergia();
        }

        private static void ObtenerDataSinergia()
        {
            var client = new RestClient("https://serviciossinergia.dnp.gov.co/ServicioSeguimientoRest.svc/ObtenerPeriodosPresidenciales");
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody("{ \"Params\": [{\"Llave\": \"idMapa\",\"Valor\": 4}]}");

            IRestResponse response = client.Execute(request);

            var content = @response.Content;

            var obj = JToken.Parse(content);
            JObject rss = JObject.Parse(obj.ToString());
            JArray p = (JArray)rss["Periodos"];

            var settings = new JsonSerializerSettings();
            settings.Formatting = Newtonsoft.Json.Formatting.Indented;
            settings.StringEscapeHandling = StringEscapeHandling.Default;

            //DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(obj.ToString(), settings);

            var periodosDto = JsonConvert.DeserializeObject<List<Periodo>>(p.ToString(), new PeriodoConverter());

            var response2 = client.Execute<RespuestaPeriodos>(request);
            if (response2.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var terridateException = new ApplicationException(message, response2.ErrorException);
                throw terridateException;
            }
            var _data = response2.Data;

        }
        private static void ObtenerdataFronteraAgricola()
        {
            var client = new RestClient("http://geoservicios.upra.gov.co/arcgis/rest/services/ordenamiento_productivo/frontera_agricola_abril_2018/MapServer/0/query");

            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

            request.AddParameter("f", "json"); // adds to POST or URL querystring based on Method
            request.AddParameter("outFields", "cod_depart,departamen,area_ha,elemento"); // adds to POST or URL querystring based on Method
            request.AddParameter("outSr", 4326); // adds to POST or URL querystring based on Method
            request.AddParameter("returnGeometry", false); // adds to POST or URL querystring based on Method
            request.AddParameter("where", "elemento = 'Frontera agrícola nacional'"); // adds to POST or URL querystring based on Method


            // execute the request
            IRestResponse response = client.Execute(request);
            var content = @response.Content; // raw content as string

            var obj = JToken.Parse(content);
            JObject features = JObject.Parse(obj.ToString());

            var attributes = from f in features["features"] select f;
            //select (f["attributes"]) into atributes
            //select f["attributes"];



            JArray p = (JArray)features["features"];

            IEnumerable<JToken> la = p.Select(c => (c["attributes"]));

            // serialize JSON results into .NET objects
            IList<Attributes> lstAttributes = new List<Attributes>();
            foreach (JToken attr in la)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                Attributes _atribute = attr.ToObject<Attributes>();
                lstAttributes.Add(_atribute);
            }

            var settings = new JsonSerializerSettings();
            settings.Formatting = Newtonsoft.Json.Formatting.Indented;
            settings.StringEscapeHandling = StringEscapeHandling.Default;

            var attributesDto = JsonConvert.DeserializeObject<List<Attributes>>(la.ToString(), new AttributesConverter());

            int size = attributesDto.Count;
            //client.UseJson();
            //var response2 = client.Execute<List<Attributes>>(request);
            //if (response2.ErrorException != null)
            //{
            //    const string message = "Error retrieving response.  Check inner details for more info.";
            //    var terridateException = new ApplicationException(message, response2.ErrorException);
            //    throw terridateException;
            //}
            //var _data = response2.Data;
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
