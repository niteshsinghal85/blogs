


// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography.X509Certificates;

var cert = new X509Certificate2(@"dev_cert.pfx", "12345");
var handler = new HttpClientHandler();
handler.ClientCertificates.Add(cert);
var client = new HttpClient(handler);

var request = new HttpRequestMessage()
{
    RequestUri = new Uri("https://localhost:7251/weatherforecast"),
    Method = HttpMethod.Get,
};
var response = await client.SendAsync(request);
if (response.IsSuccessStatusCode)
{
    var responseContent = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseContent);
}