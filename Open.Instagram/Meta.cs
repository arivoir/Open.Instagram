using System.Runtime.Serialization;

namespace Open.Instagram
{
    [DataContract]
    public class Meta
    {
        [DataMember(Name = "code")]
        public int Code { get; set; }
        [DataMember(Name = "error_type")]
        public string ErrorType { get; set; }
        [DataMember(Name = "error_message")]
        public string ErrorMessage { get; set; }
    }

    
    [DataContract]
    public class InstagramError
    {
        [DataMember(Name = "meta")]
        public Meta Meta { get; set; }
    }
}
