using Core.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Core.Models
{
    [DataContract]
    public class TreeResponse
    {
        [JsonIgnore]
        public string sha { get; set; }

        public string url { get; set; }

        [DataMember(Name = "tree")]
        [JsonConverter(typeof(DeleteUnusedTreeNodeConverter<Tree>))]
        public List<Tree> tree { get; set; }

        [JsonIgnore]
        public bool truncated { get; set; }
    }

    public class Tree
    {
        public string path { get; set; }
        
        public int size { get; set; }

        public string url { get; set; }
    }

}