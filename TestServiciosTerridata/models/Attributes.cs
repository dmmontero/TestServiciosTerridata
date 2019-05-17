using System;
using Newtonsoft.Json.Linq;
using RestSharp.Deserializers;

namespace TestServiciosTerridata.models
{
    public class Attributes
    {

        [DeserializeAs(Name = "cod_depart")]
        public string Cod_Depart { get; set; }

        [DeserializeAs(Name = "departamen")]
        public string Departamen { get; set; }

        [DeserializeAs(Name = "area_ha")]
        public double Area_Ha { get; set; }

        [DeserializeAs(Name = "elemento")]
        public string Elemento { get; set; }

    }
}
