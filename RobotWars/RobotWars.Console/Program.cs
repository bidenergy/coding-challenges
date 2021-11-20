using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RobotWars.Logic;

namespace RobotWars.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            var inputParser = host.Services.GetService<IInputParser>();

            var game = host.Services.GetService<IRobotWarsGame>();

            while (true)
            {
                string input = System.Console.ReadLine();

                // Check if quitting
                if (inputParser.IsQuit(input))
                {
                    return;
                }

                var result = game.ProcessInstruction(input);
                if (result.Successful)
                {
                    if (result.SuccessMessage != null)
                    {
                        System.Console.WriteLine(result.SuccessMessage);
                    }
                }
                else
                {
                    System.Console.WriteLine(result.FailureMessage);
                }
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices);
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection svc)
        {
            svc.AddSingleton<IInputParser, InputParser>();
            svc.AddSingleton<IRobotWarsGame, RobotWarsGame>();
        }
    }
}
