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

            // Console.WriteLine(content);
            // var deserializedContent = JsonConvert.DeserializeObject(content); // returns as Object

            // Console.WriteLine(deserializedContent);

            // Outputs JSON Array from Object
            // var output = JArray.FromObject(deserializedContent);
            // JArray output = new JArray(deserializedContent);

            // Console.WriteLine(output);

            // Outputs JSON Array from String   
            var output = JArray.Parse(content);

            var connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestSharpDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                foreach (var item in output)
                {
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO [User] (Id, Name, UserName, Email) VALUES (@Id, @Name, @UserName, @Email)", conn);
                    insertCommand.Parameters.AddWithValue("@Id", item["id"].ToObject<int>());
                    insertCommand.Parameters.AddWithValue("@Name", item["name"].ToString());
                    insertCommand.Parameters.AddWithValue("@UserName", item["username"].ToString());
                    insertCommand.Parameters.AddWithValue("@Email", item["email"].ToString());
                    insertCommand.ExecuteNonQuery();
                }
                Console.WriteLine("Database Updated");
                conn.Close();
            }
        }
    }
}
