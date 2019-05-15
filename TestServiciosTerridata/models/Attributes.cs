using RestSharp.Deserializers;

namespace TestServiciosTerridata.models
{
    public class Attributes
    {
        [DeserializeAs(Name = "cod_depart")]
        public string CodDepart { get; set; }

        [DeserializeAs(Name = "departamen")]
        public string Departamen { get; set; }

        [DeserializeAs(Name = "area_ha")]
        public double AreaHa { get; set; }

        [DeserializeAs(Name = "elemento")]
        public string Elemento { get; set; }

    }
}
