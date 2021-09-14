using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using CurrieTechnologies.Razor.Clipboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace pingu.onl
{
    public class Program
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddClipboard();
        }
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services

                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();


            builder.Services.AddClipboard();

            builder.Services.AddScoped(sp => new HttpClient(new DefaultBrowserOptionsMessageHandler(new HttpClientHandler())
            {
                DefaultBrowserRequestCache = BrowserRequestCache.NoStore,
                DefaultBrowserRequestCredentials = BrowserRequestCredentials.Include,
                DefaultBrowserRequestMode = BrowserRequestMode.Cors,
            })
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            await builder.Build().RunAsync();
        }

    }
}
