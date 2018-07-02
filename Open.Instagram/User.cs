using System.Runtime.Serialization;

namespace Open.Instagram
{
    [DataContract]
    public class UserResponse
    {
        [DataMember(Name = "meta")]
        public Meta Meta { get; set; }
        [DataMember(Name = "data")]
        public User Data { get; set; }
    }
    
    [DataContract]
    public class User
    {
        [DataMember(Name = "username")]
        public string UserName { get; set; }
        [DataMember(Name = "website")]
        public string Website { get; set; }
        [DataMember(Name = "profile_picture")]
        public string ProfilePicture { get; set; }
        [DataMember(Name = "full_name")]
        public string FullName { get; set; }
        [DataMember(Name = "bio")]
        public string Bio { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
    }
}
