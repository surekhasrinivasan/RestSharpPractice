using Newtonsoft.Json;
using RestSharp;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RestSharpPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declaring the client 
            var client = new RestClient("https://jsonplaceholder.typicode.com/");

            // Declare the request to the endpoint
            var request = new RestRequest("/users", Method.GET);

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            //Console.WriteLine(content);
            var deserializedContent = JsonConvert.DeserializeObject(content); // returns as Object

            Console.WriteLine(deserializedContent);

            // Outputs JSON Array from Object
            //var output = JArray.FromObject(deserializedContent);
            JArray output = new JArray(deserializedContent);

            Console.WriteLine(output);
        }   
    }
}
