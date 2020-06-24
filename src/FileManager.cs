using System;
using System.Threading.Tasks;

namespace jobfit
{
    public class FileManager
    {
        public async Task ReadAndOutputFile(string file)
        {
            using (var jobfit = new System.IO.StreamReader(file, true))
            {
                while (!jobfit.EndOfStream)
                {
                    var line = await jobfit.ReadLineAsync();
                    Console.WriteLine(line);
                }
            }
        }
    }
}