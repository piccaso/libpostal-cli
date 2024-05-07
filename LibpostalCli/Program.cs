using System.Diagnostics;
using System.Net.Http.Json;
var client = new HttpClient();
client.BaseAddress = new Uri("http://libpostal.cloud:8080/");

Console.CancelKeyPress += (sender, eventArgs) =>
{
    Console.WriteLine("\nBye");
};

while (true)
{
    Console.Write("? ");
    var input = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(input)) await QueryLibpostal(input);

}

async Task QueryLibpostal(string query)
{
    var sw = new Stopwatch();
    sw.Restart();
    using var result = await client.PostAsJsonAsync("/parser", new { query });
    var json = await result.Content.ReadFromJsonAsync<LibpostalRow[]>();
    sw.Stop();
    foreach (var row in json)
    {
        Console.WriteLine($"{row.Label}: {row.Value}");
    }

    //Console.WriteLine($"{sw.ElapsedMilliseconds}msec");
}

public class LibpostalRow
{
     public string? Label { get; set; }
     public string? Value { get; set; }
}