using System.Runtime.Serialization;

namespace Open.Instagram
{
    [DataContract]
    public class Image
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }
        [DataMember(Name = "width")]
        public int Width { get; set; }
        [DataMember(Name = "height")]
        public int Height { get; set; }
    }
}
