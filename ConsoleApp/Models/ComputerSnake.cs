namespace ConsoleApp.Models
{
    public class ComputerSnake {

        // properties
        public int computer_id { get; set; }
        public string motherboard { get; set; } = "";
        public int? cpu_cores { get; set; } = 0;
        public bool has_wifi { get; set; }
        public bool has_lte { get; set; }
        public DateTime? release_date { get; set; }
        public decimal price { get; set; }
        public string video_card { get; set; } = "";

        public string retrieveInfo(){
            return $"Motherboard: {motherboard} ||| CPUCores: {cpu_cores} ||| HasWifi: {has_wifi} ||| HasLTE: {has_lte} ||| ReleaseDate: {release_date} ||| Price: {price} ||| VideoCard: {video_card} |||";
        }
    }

}