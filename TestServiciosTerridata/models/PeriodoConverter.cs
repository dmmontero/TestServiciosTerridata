using Newtonsoft.Json.Converters;
using System;

namespace TestServiciosTerridata.models
{
    public class PeriodoConverter : CustomCreationConverter<Periodo>
    {
        public override Periodo Create(Type objectType)
        {
            return new Periodo();
        }
    }
}
