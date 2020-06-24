using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace jobfit.classes
{
    public class GameEngine
    {
        public string GameFile { get; }
        public string ResponseFile { get; }

        public GameEngine(string file)
        {
            GameFile = file;
            ResponseFile = $"{GameFile}.response";

            CheckFile(GameFile);
            CheckFile(ResponseFile);
        }

        private void CheckFile(string file)
        {
            if (!File.Exists(file))
            {
                throw new NullReferenceException($"Cannot find the file ({file})");
            }
        }

        public async Task<int> Play(int correctAnswer)
        {
            Console.WriteLine("Ready to play a game 🎲❓(press any key to continue)");
            Console.ReadLine();

            var answer = "";
            var tries = 0;
            do
            {
                var fileManager = new FileManager();
                await fileManager.ReadAndOutputFile(GameFile);
                tries++;
                Debug.WriteLine($"tries: {tries}");
                answer = await CheckResponse();
            } while (answer != correctAnswer.ToString());

            return tries;
        }

        private async Task<string> CheckResponse()
        {
            var answer = Console.ReadLine();
            using (var reader = new System.IO.StreamReader(ResponseFile, true))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (line.StartsWith(answer))
                    {
                        Console.WriteLine(line.Substring(2));
                        Console.WriteLine("");
                        return answer;
                    }
                }
            }

            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There is no answer response to this question");
            Console.ForegroundColor = color;
            return null;
        }
    }
}