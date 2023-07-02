using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Standard.AI.Data;

namespace POC.EntityIntelligence
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            for (; ; )
            {
                Console.WriteLine("Please enter query:");
                string query = Console.ReadLine();

                var standardAiDataClient = new AIDataClient(
                    openAIKey: "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",
                    connectionString: "Server=(localdb)\\MSSQLLocalDB;Database=DemoAIDb;Trusted_Connection=True;MultipleActiveResultSets=true");


                IEnumerable<dynamic> results =
                    await standardAiDataClient.RunAIQueryAsync(query);

                PrintDynamicList(results);
            }
        }

        private static void PrintDynamicList(IEnumerable<dynamic> dynamicList)
        {
            List<string> list = new List<string>();

            if (dynamicList.Count() < 1)
            {
                Console.WriteLine("We couldn't find any matching records.");
            }
            else
            {
                foreach (var dynamicObject in dynamicList)
                {
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(dynamicObject))
                    {
                        string name = descriptor.Name;
                        object value = descriptor.GetValue(dynamicObject);

                        if (list.Contains(name + value) is false)
                        {
                            list.Add(name + value);
                            Console.WriteLine("{0}: {1}", name, value);
                        }
                    }
                }
            }
        }
    }
}