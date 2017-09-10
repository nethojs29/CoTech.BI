using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CoTech.Bi.Util
{
    public static class JsonConverterOptions
    {
        public static JsonSerializerSettings JsonSettings { get; private set; }
        static JsonConverterOptions() {
            var contractResolver = new DefaultContractResolver{
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            JsonSettings = new JsonSerializerSettings {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
        }
    }
}