using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drug_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            //DbManager db = new DbManager();
            //db.Open();
            Parser parser = new Parser();
            parser.Parse();
            Console.WriteLine("Начали анализ страницы");
            Console.ReadKey();
        }
    }
}
