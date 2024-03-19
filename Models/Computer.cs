using System.Text.Json.Serialization;

namespace HelloWorld.Models{
    // Models
    public class Computer
    {
        // Put the value as private
        
        // This is long hand to get and set a field via property
        // private string _motherboard; // its field
        // // Add setter and getter to access value
        // public string Motherboard {get{return _motherboard;} set{_motherboard = value;}}

        // Shortcut of above
        // Strings are not nullable so it might throw error so use nullable by adding ?
        [JsonPropertyName("computer_id")]
        public int ComputerId {get; set;}
        [JsonPropertyName("motherboard")]
        public string Motherboard {get; set;}
        // int is non nullable by default and EF cant map null to int
        [JsonPropertyName("cpu_cores")]
        public int? CPUCores{get; set;}
        [JsonPropertyName("has_wifi")]
        public bool HasWifi{get; set;}
        [JsonPropertyName("has_lte")]
        public bool HasLTE{get; set;}
        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate{get; set;}
        [JsonPropertyName("price")]
        public decimal Price{get; set;}
        [JsonPropertyName("video_card")]
        public string VideoCard{get; set;}

        // Constructor Function
        public Computer()
        {
            // To avoid nullable issue either use ? with string or use this strategy
            if(VideoCard == null)
            {
                VideoCard = "";
            }
            if(Motherboard == null)
            {
                Motherboard = "";
            }
            if(CPUCores == null)
            {
                CPUCores = 0;
            }
        }
    }
}