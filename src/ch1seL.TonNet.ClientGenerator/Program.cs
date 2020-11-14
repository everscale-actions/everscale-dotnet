using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Model;
using Model.Domain;

namespace Runner
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Generator.Generator.GenerateModule();
        }
    }
}