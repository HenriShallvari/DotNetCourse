using ConsoleApp.Data;
using ConsoleApp.Handlers;
using ConsoleApp.Models;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            try
            {
                IConfiguration conf = GetConfig("appsettings.json");
                DataContextEF dataContext = new(conf); // same as new DataContextEF()... 

                IConfiguration functionalities = GetConfig("functionalities.json");
                FunctionalitiesOptions funcOptions = functionalities.GetSection("Functionalities").Get<FunctionalitiesOptions>() ?? throw new Exception("No functionalities found... check if functionalities.json exists and is configured correctly.");
                HashSet<string> availableActions = GetAvailableActionNames();

                BootAndGreet(funcOptions);
                string userOption = "";

                ActionHandler handler = new();

                while(userOption != "0")
                {
                    ShowMenuOptions(funcOptions, availableActions);
                    userOption = Console.ReadLine();

                    if (userOption != null && userOption.Trim() != "")
                    {
                        var selectedOption = funcOptions.MenuOptions
                                            .FirstOrDefault(opt => opt.Trigger.Equals(userOption, StringComparison.InvariantCultureIgnoreCase));

                        if (userOption != "0")
                        {
                            if (selectedOption != null && availableActions.Contains(selectedOption.Name))
                            {
                                MethodInfo actionToInvoke = typeof(ActionHandler).GetMethod(selectedOption.Name)!;
                                actionToInvoke.Invoke(null, null); 
                            }
                            else
                            {
                                Console.WriteLine("Please select a valid option.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please select an option.");
                    }
                }
            } 
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"There was an error during the program execution: {exc.Message}");
            }

            Console.WriteLine("Bye!");
        }
        
        private static IConfiguration GetConfig(string configName)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configName, optional: false, reloadOnChange: true)
                .Build();

            return configuration;
        }
        

        private static void BootAndGreet(FunctionalitiesOptions funcOptions)
        {

            Console.WriteLine(funcOptions.BootString);

            Random rnd = new();
            int randomGreet = rnd.Next(0, funcOptions.Greetings.Count);

            Console.WriteLine("...");
            Thread.Sleep(1000);
            Console.WriteLine(funcOptions.Greetings[randomGreet]);

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        private static void ShowMenuOptions(FunctionalitiesOptions funcOptions, HashSet<string> availableActions)
        {
            List<MenuOption> options = funcOptions.MenuOptions ?? throw new Exception("No Menu Options found... Nothing to do. BYE!");

            Console.WriteLine("What do you want to do?");
            Console.WriteLine("-------------------------------------------------");

            foreach(var opt in options)
            {

                if (availableActions.Contains(opt.Name))
                {
                    Console.WriteLine($"{opt.Trigger} => {opt.Description}");
                }
            }

            Console.WriteLine("0 => Quit");
        }

        private static HashSet<string> GetAvailableActionNames()
        {
            HashSet<string> availableActions = typeof(ActionHandler)
                                            .GetMethods(BindingFlags.Public | BindingFlags.Static)
                                            .Select(method => method.Name)
                                            .ToHashSet();
            return availableActions;
        }

    }
}