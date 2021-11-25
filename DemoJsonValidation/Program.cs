using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NJsonSchema;

namespace DemoJsonValidation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await UsingNewtonsoftAsync();

            await UsingNJsonSchemaAsync();
        }

        private static async Task UsingNJsonSchemaAsync()
        {
            var jsonSchema = await NJsonSchema.JsonSchema.FromFileAsync("schema.json");

            var json = await File.ReadAllTextAsync("TestFile.Json", System.Text.Encoding.UTF8);

            var errors = jsonSchema.Validate(json);

            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
        }

        private static async Task UsingNewtonsoftAsync()
        {
            string jsonSchema = await File.ReadAllTextAsync("schema.Json", System.Text.Encoding.UTF8);
            JSchema schema = JSchema.Parse(jsonSchema);

            string jsonContent = await File.ReadAllTextAsync("TestFile.Json", System.Text.Encoding.UTF8);

            JObject person = JObject.Parse(jsonContent);

            person.IsValid(schema, out IList<string> messages);

            foreach (var item in messages)
            {
                Console.WriteLine(item);
            }
        }
    }
}
