using System.Net.Http.Json;

var client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5195/");

Console.WriteLine("== GET ==");
var all = await client.GetFromJsonAsync<object>("api/products");
Console.WriteLine(all);

Console.WriteLine("== POST ==");
var newProduct = new { name = "Apple", price = 5.25 };
var createResp = await client.PostAsJsonAsync("api/products", newProduct);
Console.WriteLine(await createResp.Content.ReadAsStringAsync());

Console.WriteLine("== PUT ==");
var update = new { id = 1, name = "Apple Updated", price = 6.0 };
await client.PutAsJsonAsync("api/products/1", update);

Console.WriteLine("== DELETE ==");
var delete = await client.DeleteAsync("api/products/1");
Console.WriteLine(delete.StatusCode);