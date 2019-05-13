using RestSharp;
using RestSharp.Serialization.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServiciosTerridata.models;

namespace TestServiciosTerridata
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://geoservicios.upra.gov.co/arcgis/rest/services/ordenamiento_productivo/frontera_agricola_abril_2018/MapServer/0/query");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest(Method.POST);
            request.AddParameter("f", "json"); // adds to POST or URL querystring based on Method
            request.AddParameter("outFields", "cod_depart,departamen,area_ha,elemento"); // adds to POST or URL querystring based on Method
            request.AddParameter("outSr", 4326); // adds to POST or URL querystring based on Method
            request.AddParameter("returnGeometry", false); // adds to POST or URL querystring based on Method
            request.AddParameter("where", "elemento = 'Frontera agrícola nacional'"); // adds to POST or URL querystring based on Method


            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            client.UseDotNetXmlSerializer();

            // execute the request
            IRestResponse response = client.Execute<Features>(request);
            var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();

            //var respuesta = client.Execute(request);
            //RestResponse<Person> response2 = client.Execute<Person>(request);
            //var name = response2.Data.Name;

            // easy async support
            //client.ExecuteAsync(request, response => { Console.WriteLine(response.Content); });

            // async with deserialization
            //var asyncHandle = client.ExecuteAsync<Person>(request, response => {Console.WriteLine(response.Data);});

            // abort the request on demand
            //asyncHandle.Abort();
        }
    }
}
