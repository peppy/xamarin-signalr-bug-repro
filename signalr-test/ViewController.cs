using Foundation;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UIKit;

namespace signalr_test
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            Task.Run(async () =>
            {
                var builder = new HubConnectionBuilder().WithUrl("https://dev.ppy.sh/spectator");
                builder.ConfigureLogging(l => l.AddConsole());
                var connection = builder.Build();
                connection.Closed += Connection_Closed;
                Console.WriteLine("Connecting...");
                await connection.StartAsync();
                Console.WriteLine("Connected!");
            });
        }

        private Task Connection_Closed(Exception arg)
        {
            Console.WriteLine($"Connection closed with {arg}");
            return Task.CompletedTask;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}