using System;
using Newtonsoft.Json.Converters;

namespace TestServiciosTerridata.models
{
    class AttributesConverter : CustomCreationConverter<Attributes>
    {
        public override Attributes Create(Type objectType)
        {
            return new Attributes();
        }
    }
}
