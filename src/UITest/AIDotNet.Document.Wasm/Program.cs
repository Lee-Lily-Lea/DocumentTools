using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<AIDotNet.Document.App>("#app");

builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddDocumentRcl();

builder.Services.AddDocumentServiceForWasm();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
