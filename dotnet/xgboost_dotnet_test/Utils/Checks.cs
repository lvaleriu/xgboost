using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xgboost_dotnet_test
{
    public class Checks
    {
        public string Title = "";
        public List<string> Messages = new List<string>();
        public int Errors = 0;

        public Checks(string title)
        {
            Title = title;
        }

        public void IsEqual(object expected, object real, string description) 
        {
            var line = description + ": ";
            if (expected.Equals(real))
            {
                line += "ok";
            }
            else
            {
                line += "expected " + expected + " <> real " + real;
                Errors++;
            }
            Messages.Add(line);
        }

        public void IsTrue(bool condition, string description)
        {
            var line = description + ": ";
            if (condition)
            {
                line += "ok";
            }
            else
            {
                line += " not true";
                Errors++;
            }
            Messages.Add(line);
        }

        public void PrintReport()
        {
            Console.WriteLine("");
            Console.WriteLine("****" + Title + "****");
            foreach (var message in Messages)
            {
                Console.WriteLine(message);
            }
        }
    }
}
