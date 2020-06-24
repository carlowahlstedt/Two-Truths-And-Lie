using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using jobfit.classes;

namespace jobfit
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var gameFile = GetGameFile(args, 0, "./gamefiles/truths");

            var fileManager = new FileManager();
            var engine = new GameEngine(gameFile);

            await fileManager.ReadAndOutputFile("jobfit");
            Console.ReadLine();

            await fileManager.ReadAndOutputFile("from");

            var tries =  await engine.Play(1);

            Console.WriteLine("");
            switch (tries)
            {
                case 1:
                    InvokeDocker("Ok, that was too easy 🏀, here's another set of questions:", 1);
                    var bonusGameFile = GetGameFile(args, 1, "./gamefiles/truths-rnd2");
                    var bonusEngine = new GameEngine(bonusGameFile);
                    tries =  await bonusEngine.Play(2);
                    break;
                case 2:
                    InvokeDocker("😝 Fooled you once, shame on me! ⛹🏼‍♂️", 2);
                    break;
                default:
                    InvokeDocker("Fooled you twice, shame on you!", 3);
                    break;
            }

            Console.WriteLine("Thanks for playing‼️");
            Console.ReadLine();
        }

        static string GetGameFile(string[] args, int index, string defaultFile)
        {
            var file = args.Length > index ? args[index] : null;
            return file ?? defaultFile;
        }


        void InvokeDocker(string message, int character)
        {
            var type = "gummy";
            switch (character)
            {
                case 1:
                    type = "gummy";
                    break;
                case 2:
                    type = "spike";
                    break;
                case 3:
                    type = "gummy";
                    break;
                // default:
            }
            var processInfo = new ProcessStartInfo("docker", $"run -ti --rm mpepping/ponysay --colour \"0;32\" --pony {type} {message}");

            using (var process = new Process())
            {
                process.StartInfo = processInfo;

                process.Start();
                process.WaitForExit(1000);

                process.Close();
            }
        }
    }
}
