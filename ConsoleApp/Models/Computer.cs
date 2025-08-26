namespace ConsoleApp.Models
{
    public class Computer {

        // properties
        public int ComputerId { get; set; }
        public string Motherboard { get; set; } = "";
        public int? CPUCores { get; set; }
        public bool HasWifi { get; set; }
        public bool HasLTE { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; } = "";

        // public Computer(

        public void printInfo(){
            Console.WriteLine($"Motherboard: {Motherboard} ||| CPUCores: {CPUCores} ||| HasWifi: {HasWifi} ||| HasLTE: {HasLTE} ||| ReleaseDate: {ReleaseDate} ||| Price: {Price} ||| VideoCard: {VideoCard} |||");
        }
    }

}