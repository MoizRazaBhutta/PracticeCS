namespace HelloWorld.Models{
    // Models
    public class ComputerSnake
    {
        // Put the value as private
        
        // This is long hand to get and set a field via property
        // private string _motherboard; // its field
        // // Add setter and getter to access value
        // public string Motherboard {get{return _motherboard;} set{_motherboard = value;}}

        // Shortcut of above
        // Strings are not nullable so it might throw error so use nullable by adding ?
        public int computer_id {get; set;}
        public string motherboard {get; set;}
        // int is non nullable by default and EF cant map null to int
        public int? cpu_cores{get; set;}
        public bool has_wifi{get; set;}
        public bool has_lte{get; set;}
        public DateTime? release_date{get; set;}
        public decimal price{get; set;}
        public string video_card{get; set;}

        // Constructor Function
        public ComputerSnake()
        {
            // To avoid nullable issue either use ? with string or use this strategy
            if(video_card == null)
            {
                video_card = "";
            }
            if(motherboard == null)
            {
                motherboard = "";
            }
            if(cpu_cores == null)
            {
                cpu_cores = 0;
            }
        }
    }
}