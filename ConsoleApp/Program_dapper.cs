using System.Data;
using System.Globalization;
using ConsoleApp.Data;
using ConsoleApp.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    internal class Program_dapper
    {
        private static void Main_dapper(string[] args)
        {

            IConfiguration conf = getConfig();
            DataContextDapper dataContext = new DataContextDapper(conf);


            Computer myComputer = new() // same as new Computer()
            {
                Motherboard = "Z691",
                CPUCores = 12,
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 980.00m,
                VideoCard = "RTX 4070"
            };

            // insertComputer(myComputer, dbConnection);

            IEnumerable<Computer> computers = dataContext.LoadData<Computer>("SELECT * FROM TutorialAppSchema.Computer");

            foreach (var machine in computers)
            {
                Console.WriteLine($"Motherboard: {machine.Motherboard} ||| CPUCores: {machine.CPUCores} ||| HasWifi: {machine.HasWifi} ||| HasLTE: {machine.HasLTE} ||| ReleaseDate: {machine.ReleaseDate} ||| Price: {machine.Price} ||| VideoCard: {machine.VideoCard} |||");
            }

        }
        
        private static IConfiguration getConfig()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration;
        }

        private static int insertComputer(Computer computer, DataContextDapper dataContext)
        {
            string sql = $@"INSERT INTO TutorialAppSchema.Computer(
                Motherboard,
                CPUCores,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES (
              '{computer.Motherboard}',
              '{computer.CPUCores}',
              '{computer.HasWifi}',
              '{computer.HasLTE}',
              '{computer.ReleaseDate}',
              '{computer.Price.ToString("0.00", CultureInfo.InvariantCulture)}',
              '{computer.VideoCard}'
            )";

            return dataContext.ExecuteSQLWithRowCount(sql); 
        }

        // private static IEnumerable<Computer> getAllComputers(IDbConnection dbConnection){
        //     string sql = "";

        //     IEnumerable<Computer> computers = dbConnection.Query<Computer>(sql);

        //     return computers;
        // }
    }
}