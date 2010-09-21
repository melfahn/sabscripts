using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SABSync.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new CassiniDev.CassiniDevServer();
            server.StartServer(@"D:\Opensource\Spongebob\SABSync.Web");


            System.Console.WriteLine(server.NormalizeUrl(@"series"));
            System.Console.ReadLine();

        }
    }
}
