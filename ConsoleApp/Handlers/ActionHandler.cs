using System.Text.Json;
using Azure.Core.Serialization;
using ConsoleApp.Data;
using ConsoleApp.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp.Handlers
{
    public class ActionHandler
    {
        // private static readonly JsonSerializerOptions _jsonOptions;

        // static ActionHandler()
        // {
        //     _jsonOptions = new JsonSerializerOptions()
        //     {
        //         PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //     };
        // }

        public static void LoadComputersJson(IConfiguration config, DataContextEF dataContext)
        {
            // CODE IMPLEMENTATION HERE...
            Console.WriteLine("Loading computers.json...");
            string computersJsonPath = config.GetValue<string>("ComputersJsonPath") ?? throw new Exception("Invalid value for ComputersJsonPath configuration."); 
            string computersJson = File.ReadAllText(computersJsonPath);

            // options are necessary to map correctly the JSON to our model
            // IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, _jsonOptions);

            // Here we are using newtonsoft.json's deserialization implementation. 
            // we do not need to pass in custom JSON options, it adds, however a layer of complexity (mantaining yet another library...)
            List<JObject>? computerJObjects = JsonConvert.DeserializeObject<List<JObject>>(computersJson);

            if (computerJObjects != null)
            {
                foreach (var jObject in computerJObjects)
                {
                    // Necessary as Computers.json has a ComputerId field that clashes with the DB
                    // we could strip it from the JSON file, but it's after midnight and it seems
                    // too much work.
                    jObject.Remove("ComputerId");
                    Computer computer = jObject.ToObject<Computer>()!;

                    dataContext.Add(computer);
                }

                dataContext.SaveChanges();

                Console.WriteLine($"Added {computerJObjects.Count} computers");
            }

        }

        public static void ListAllComputers(IConfiguration config, DataContextEF dataContext)
        {
            // CODE IMPLEMENTATION HERE...
            Console.WriteLine("Coming soon!");
        }

        public static void ResetDB(IConfiguration config, DataContextEF dataContext)
        {
            // CODE IMPLEMENTATION HERE...
        }
    }
}