using StoreApp;
using UI;
namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {


            StoreConsoleApp oStoreConsoleApp = new StoreConsoleApp();

            await oStoreConsoleApp.RunAsync();

        }
    }
}
