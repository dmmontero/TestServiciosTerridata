using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServiciosTerridata.serializers
{
    class SnakeJsonSerializerStrategy : PocoJsonSerializerStrategy
    {
        protected override string MapClrMemberNameToJsonFieldName(string clrPropertyName)
        {
            //PascalCase to snake_case
            return string.Concat(clrPropertyName.Select((x, i) => char.IsUpper(x) ? (i > 0 ? "_" : "") + char.ToLower(x).ToString() : x.ToString()));
        }
    }
}
