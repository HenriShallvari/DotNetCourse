namespace AsyncAndTasks
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // anonymous functions. yay!
            Task firstTask = new(() => {
                Thread.Sleep(100);
                Console.WriteLine("Task 1");
            });

            firstTask.Start();

            Task secondTask = ConsoleAfterDelayAsync("Task 2", 150);

            ConsoleAfterDelay("Delay", 101);

            Task thirdTask = ConsoleAfterDelayAsync("Task 3", 50);

            await secondTask;
            await firstTask;
            Console.WriteLine("After the Task was created.");

            await thirdTask;
        }

        static void ConsoleAfterDelay(string txt, int delayTime)
        {
            Thread.Sleep(delayTime);
            Console.WriteLine(txt);
        }
        static async Task ConsoleAfterDelayAsync(string txt, int delayTime)
        {
            await Task.Delay(delayTime);
            Console.WriteLine(txt);
        }
    }   
}