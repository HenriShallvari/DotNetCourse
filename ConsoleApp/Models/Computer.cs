using System.Text.Json.Serialization;

namespace ConsoleApp.Models
{
    public class Computer {

        // properties
        [JsonPropertyName("computer_id")] // an even simpler way of mapping our computer fields to our snake case JSON fields.
        public int ComputerId { get; set; }

        [JsonPropertyName("motherboard")]
        public string Motherboard { get; set; } = "";

        [JsonPropertyName("cpu_cores")]
        public int? CPUCores { get; set; } = 0;

        [JsonPropertyName("has_wifi")]
        public bool HasWifi { get; set; }

        [JsonPropertyName("has_lte")]
        public bool HasLTE { get; set; }

        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("video_card")]
        public string VideoCard { get; set; } = "";


        public string retrieveInfo(){
            return $"Motherboard: {Motherboard} ||| CPUCores: {CPUCores} ||| HasWifi: {HasWifi} ||| HasLTE: {HasLTE} ||| ReleaseDate: {ReleaseDate} ||| Price: {Price} ||| VideoCard: {VideoCard} |||";
        }
    }

}