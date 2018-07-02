using System.Runtime.Serialization;

namespace Open.Instagram
{
    [DataContract]
    public class ItemsResponse
    {
        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; set; }
        [DataMember(Name = "meta")]
        public Meta Meta { get; set; }
        [DataMember(Name = "data")]
        public Item[] Data { get; set; }
    }

    [DataContract]
    public class Pagination
    {
        [DataMember(Name = "next_url")]
        public string NextUrl { get; set; }
        [DataMember(Name = "next_max_id")]
        public string NextMaxId { get; set; }
    }

    [DataContract]
    public class LinksResponse
    {
        //[DataMember(Name = "pagination")]
        //public Pagination Pagination { get; set; }
        [DataMember(Name = "meta")]
        public Meta Meta { get; set; }
        [DataMember(Name = "data")]
        public User[] Data { get; set; }
    }

    [DataContract]
    public class Comments
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }
        [DataMember(Name = "data")]
        public Comment[] Data { get; set; }
    }

    [DataContract]
    public class Likes
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }
        [DataMember(Name = "data")]
        public User[] Data { get; set; }
    }

    [DataContract]
    public class Images
    {
        [DataMember(Name = "low_resolution")]
        public Image LowResolution { get; set; }
        [DataMember(Name = "thumbnail")]
        public Image Thumbnail { get; set; }
        [DataMember(Name = "standard_resolution")]
        public Image StandardResolution { get; set; }
    }

    [DataContract]
    public class Videos
    {
        [DataMember(Name = "low_resolution")]
        public Image LowResolution { get; set; }
        [DataMember(Name = "low_bandwidth")]
        public Image LowBandwidth { get; set; }
        [DataMember(Name = "standard_resolution")]
        public Image StandardResolution { get; set; }
    }

    [DataContract]
    public class Caption
    {
        [DataMember(Name = "created_time")]
        public string CreatedTime { get; set; }
        [DataMember(Name = "text")]
        public string Text { get; set; }
        [DataMember(Name = "from")]
        public User From { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
    }

    [DataContract]
    public class Item
    {
        [DataMember(Name = "attribution")]
        public string Attribution { get; set; }
        [DataMember(Name = "tags")]
        public string[] Tags { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        //[DataMember(Name = "location")]
        //public string Location { get; set; }
        [DataMember(Name = "comments")]
        public Comments Comments { get; set; }
        [DataMember(Name = "filter")]
        public string Filter { get; set; }
        [DataMember(Name = "created_time")]
        public string CreatedTime { get; set; }
        [DataMember(Name = "link")]
        public string Link { get; set; }
        [DataMember(Name = "likes")]
        public Likes Likes { get; set; }
        [DataMember(Name = "images")]
        public Images Images { get; set; }
        [DataMember(Name = "users_in_photo")]
        public User[] UsersInPhoto { get; set; }
        [DataMember(Name = "caption")]
        public Caption Caption { get; set; }
        [DataMember(Name = "user_has_liked")]
        public bool UserHasLiked { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "user")]
        public User User { get; set; }
        [DataMember(Name = "videos")]
        public Videos Videos { get; set; }
    }
}
