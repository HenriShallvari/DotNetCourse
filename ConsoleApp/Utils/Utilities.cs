using Newtonsoft.Json;

namespace ConsoleApp.Utils
{
    public static class Utilities
    {
        public static void DumpObj(Object obj)
        {
            string jsonDump = JsonConvert.SerializeObject(obj, Formatting.Indented);

            Console.WriteLine(jsonDump);
        }
    }
}