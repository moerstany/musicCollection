namespace musicCollection
{
    public class Music
    {
        
        
            [Newtonsoft.Json.JsonProperty("id")]
            public uint id { get; set; }
            
            [Newtonsoft.Json.JsonProperty("song_name")]
            public string song_name { get; set; }
            
            [Newtonsoft.Json.JsonProperty("song_time")]
            public uint song_time { get; set; } 
            
            [Newtonsoft.Json.JsonProperty("music_style")]
            public string music_style { get; set; }
            
            [Newtonsoft.Json.JsonProperty("music_disk_name")]
            public string music_disk_name { get; set; }
            
            [Newtonsoft.Json.JsonProperty("singer")]
            public string singer { get; set; }

           
    }
    
}