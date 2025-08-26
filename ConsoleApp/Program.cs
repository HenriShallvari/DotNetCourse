using System.Data;
using System.Globalization;
using ConsoleApp.Data;
using ConsoleApp.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            IConfiguration conf = GetConfig();
            DataContextEF dataContext = new(conf); // same as new DataContextEF()... 

            Computer myComputer = new()
            {
                Motherboard = "Z692 EF CORE",
                CPUCores = 12,
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 980.00m,
                VideoCard = "RTX 4070"
            };

            dataContext.Add(myComputer);
            dataContext.SaveChanges();

            IEnumerable<Computer>? computers = dataContext.Computer?.ToList(); 

            if(computers is not null)
            {
                foreach (var machine in computers)
                {
                    Console.WriteLine($"ComputerId: {machine.ComputerId} ||| Motherboard: {machine.Motherboard} ||| CPUCores: {machine.CPUCores} ||| HasWifi: {machine.HasWifi} ||| HasLTE: {machine.HasLTE} ||| ReleaseDate: {machine.ReleaseDate} ||| Price: {machine.Price} ||| VideoCard: {machine.VideoCard} |||");
                }
            }

        }
        
        private static IConfiguration GetConfig()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration;
        }
    }
}