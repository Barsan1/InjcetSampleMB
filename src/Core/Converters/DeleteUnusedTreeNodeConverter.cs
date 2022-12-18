using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Core.Converters
{
    public class DeleteUnusedTreeNodeConverter<T> : JsonConverter
    {
        /// <summary>
        /// Override if this converter can write too.
        /// </summary>
        public override bool CanWrite => false;


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var list = (existingValue as List<T>);

            if (list == null)
                list = new List<T>();

            if (reader.TokenType != JsonToken.StartArray)
                return list;

            while (reader.Read())
            {
                // Ent of loop.
                if (reader.TokenType == JsonToken.EndArray)
                    break;

                var obj = JObject.Load(reader);

                var path = obj["path"];
                var size = obj["size"];

                // Check if valid path, then add it.
                if (path != null && 
                    path.Type == JTokenType.String && 
                    IsSampleComponentPath((string)path))
                {
                    list.Add((T)obj.ToObject<T>(serializer));
                }
            }

            return list;
        }


        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<T>);
        }

        /// <summary>
        /// Check if this is a valid componnent path sample
        /// </summary>
        /// <param name="path">Url path to check</param>
        /// <returns>Valid path or not</returns>
        private bool IsSampleComponentPath(string path)
            => path.StartsWith(@"src/MudBlazor.Docs/Pages/Components/");

        /// <summary>
        /// Throw exception if someone want to write to this json convertor.
        /// </summary>
        /// <exception cref="NotImplementedException">Throw exception if someone want to write to this json convertor.</exception>
        /// <returns>Throw exception if someone want to write to this json convertor.</returns>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            =>  throw new NotImplementedException();
        
    }
}
