namespace musicCollection
{
    public class Singers
    {
        
            [Newtonsoft.Json.JsonProperty("singer_id")]
            public uint singer_id { get; set; }
            
            [Newtonsoft.Json.JsonProperty("singer")]
            public string singer { get; set; }

    }
}