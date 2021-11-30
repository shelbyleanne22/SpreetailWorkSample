using SpreetailWorkSample.Constants;
using SpreetailWorkSample.Interfaces;
using System;
using System.Collections.Generic;

namespace SpreetailWorkSample.Services
{
    public class PrintService : IPrintService
    {
        public void Print(HashSet<string> results)
        {
            if (results != null)
            {
                int index = 1;
                foreach (string value in results)
                {
                    Console.WriteLine($"{index}) {value}");
                    index++;
                };
            } else
            {
                Console.WriteLine(MessageConstants.EmptySetMessage);
            }
        }

        public void Print(IReadOnlyList<string> results)
        {
            if (results != null)
            {
                int index = 1;
                foreach (string value in results)
                {
                    Console.WriteLine($"{index}) {value}");
                    index++;
                };
            }
            else
            {
                Console.WriteLine(MessageConstants.EmptySetMessage);
            }
        }


        public void Print(HashSet<KeyValuePair<string,string>> results)
        {
            if (results != null)
            {
                int index = 1;
                foreach (KeyValuePair<string, string> key in results)
                {
                    Console.WriteLine($"{index}) {key.Key} : {key.Value}");
                    index++;
                }
            } else
            {
                Console.WriteLine(MessageConstants.EmptySetMessage);
            }
        }

        public void Print(string result)
        {
            if (result != null)
            {
                Console.WriteLine(result);
            }
        }
    }
}
