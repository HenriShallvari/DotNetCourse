using System.Text.Json;
using AutoMapper;
using Azure.Core.Serialization;
using ConsoleApp.Data;
using ConsoleApp.Models;
using ConsoleApp.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ConsoleApp.Handlers
{
    public class ActionHandler
    {
        // private static readonly JsonSerializerOptions _jsonOptions;

        // Constructor
        // static ActionHandler()
        // {
        //     _jsonOptions = new JsonSerializerOptions()
        //     {
        //         PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //     };
        // }

        public static void LoadComputersJson(IConfiguration config, DataContextEF dataContext)
        {
            Console.WriteLine("Loading computers.json...");
            // string computersJsonPath = config.GetValue<string>("ComputersJsonPath") ?? throw new Exception("Invalid value for ComputersJsonPath configuration."); 
            string computersSnakeJsonPath = config.GetValue<string>("ComputersSnakeJsonPath") ?? throw new Exception("Invalid value for computersSnakeJsonPath configuration."); 
            string computersJson = File.ReadAllText(computersSnakeJsonPath);

            // we explicitely map every snake case field into our 
            // pascal case field

            // we skip mapping ComputerId as it's an Identity Field in our DB. 
            Mapper mapper = new(new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<ComputerSnake, Computer>()
                    // .ForMember(
                    //     destination => destination.ComputerId,
                    //     options => options.MapFrom(source => source.computer_id)
                    // )
                    .ForMember(
                        destination => destination.Motherboard,
                        options => options.MapFrom(source => source.motherboard)
                    )
                    .ForMember(
                        destination => destination.CPUCores,
                        options => options.MapFrom(source => source.cpu_cores)
                    )
                    .ForMember(
                        destination => destination.HasWifi,
                        options => options.MapFrom(source => source.has_wifi)
                    )
                    .ForMember(
                        destination => destination.HasLTE,
                        options => options.MapFrom(source => source.has_lte)
                    )
                    .ForMember(
                        destination => destination.ReleaseDate,
                        options => options.MapFrom(source => source.release_date)
                    )
                    .ForMember(
                        destination => destination.Price,
                        options => options.MapFrom(source => source.price)
                    )
                    .ForMember(
                        destination => destination.VideoCard,
                        options => options.MapFrom(source => source.video_card)
                    );
            }));

            // IF you have decorators in your class like these:
            // [JsonPropertyName("motherboard")]
            // you can avoid mapping like this: IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computers); 
            // this works only for simple solutions where our JSON Structure is 1:1 with our model
            
            // In more complex cases (maybe when we have first_name and last_name in our JSON and in our model we have FullName),
            // a bridge DTO + AutoMapper is more flexible. 

            IEnumerable<ComputerSnake>? computers = JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);

            if(computers != null)
            {
                // here we are mapping our snake case computers to our Computer class.
                IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computers);

                foreach(var computer in computerResult)
                {
                    dataContext.Add(computer);
                }

                dataContext.SaveChanges();

                Console.WriteLine($"Added {computerResult.ToList().Count} computers");
            }


            // Here we are using newtonsoft.json's deserialization implementation. 
            // we do not need to pass in custom JSON options, it adds, however a layer of complexity (mantaining yet another library...)
            // List<JObject>? computerJObjects = JsonConvert.DeserializeObject<List<JObject>>(computersJson);

            // if (computerJObjects != null)
            // {
            //     foreach (var jObject in computerJObjects)
            //     {
            //         // Necessary as Computers.json has a ComputerId field that clashes with the DB
            //         // we could strip it from the JSON file, but it's after midnight and it seems
            //         // too much work.
            //         jObject.Remove("ComputerId");
            //         Computer computer = jObject.ToObject<Computer>()!;

            //         dataContext.Add(computer);
            //     }

            //     dataContext.SaveChanges();

            //     Console.WriteLine($"Added {computerJObjects.Count} computers");
            // }

                
        }

        public static void ListAllComputers(IConfiguration config, DataContextEF dataContext)
        {
            // CODE IMPLEMENTATION HERE...
            Console.WriteLine("Coming soon!");
        }

        public static void ResetDB(IConfiguration config, DataContextEF dataContext)
        {
            // CODE IMPLEMENTATION HERE...
            Console.WriteLine("Coming soon!");
        }
    }
}