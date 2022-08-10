using Mentoring.Models;
using Newtonsoft.Json;

namespace DataFetcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting application");
            var client = new HttpClient();
            client.BaseAddress = new System.Uri("http://localhost:5274/api/");
            var categoriesRequest = client.GetAsync("CategoriesApi").Result;
            var categoriesResponse = categoriesRequest.Content.ReadAsStringAsync().Result;
            var categoriesResultData = JsonConvert.DeserializeObject<List<Category>>(categoriesResponse);
            Console.WriteLine("Categories data is...");
            Console.WriteLine(JsonConvert.SerializeObject(categoriesResultData));
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Products data is...");
            var productsRequest = client.GetAsync("ProductsApi").Result;
            var productsResponse = productsRequest.Content.ReadAsStringAsync().Result;
            var productsResultData = JsonConvert.DeserializeObject<List<Product>>(productsResponse);
            Console.WriteLine(JsonConvert.SerializeObject(productsResultData));
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}