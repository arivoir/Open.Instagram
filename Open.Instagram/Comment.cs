using System.Runtime.Serialization;

namespace Open.Instagram
{
    [DataContract]
    public class CommentsResponse
    {
        [DataMember(Name = "meta")]
        public Meta Meta { get; set; }
        [DataMember(Name = "data")]
        public Comment[] Data { get; set; }
    }

    [DataContract]
    public class Comment
    {
        [DataMember(Name = "created_time", EmitDefaultValue=false)]
        public string CreatedTime { get; set; }
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }
        [DataMember(Name = "from", EmitDefaultValue = false)]
        public User From { get; set; }
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
    }
}
