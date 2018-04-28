using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
     public sealed class JsonNetSerializer:IJsonSerializer
    {
        private readonly JsonSerializer _serializer;


        public JsonNetSerializer() : this(JsonSerializer.CreateDefault())
        {

        }

        /// <summary>
        /// 创建JsonNetSerializer 并且实例化一个JsonSerializer对象
        /// </summary>
        /// <param name="serializer"></param>
        public JsonNetSerializer(JsonSerializer serializer)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public T Deserialize<T>(string json)
        {
            return JObject.Parse(json).ToObject<T>(_serializer);
        }

        public string Serialize(object obj)
        {            
            return JObject.FromObject(obj, _serializer).ToString(_serializer.Formatting, _serializer.Converters.ToArray());
        }
    }
}
