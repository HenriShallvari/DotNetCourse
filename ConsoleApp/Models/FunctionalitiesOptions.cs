namespace ConsoleApp.Models
{
    public class FunctionalitiesOptions
    {
        public string BootString { get; set; } = "SUPERCOOL COMPUTER MANAGEMENT SYSTEM";

        public List<string> Greetings { get; set; } = ["Hello Human..."];

        public required List<MenuOption> MenuOptions { get; set; }
    }

    public class MenuOption 
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Trigger { get; set; }
    }
}