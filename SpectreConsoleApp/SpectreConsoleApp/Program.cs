using Spectre.Console;
using System;
using System.Threading.Tasks;

namespace SpectreConsoleApp
{
    public enum Mode
    {
        Http,
        Https
    }

    class Program
    {
        public static async Task Main(string[] args)
        {
            // Prompting the user for input
            string IPAddress = AnsiConsole.Prompt(new TextPrompt<string>("Enter IPAddress to connect :"));
            string Port = AnsiConsole.Prompt(new TextPrompt<string>("Enter Port to connect :"));
            
            // prompting to choose from given option
            var mode = AnsiConsole.Prompt(
                new SelectionPrompt<Mode>()
                    .Title("Enter connection mode :")
                    .AddChoices(
                        Mode.Http,
                        Mode.Https));
           
           // creating table
           AnsiConsole.Write(
           new Table()
               .AddColumn(new TableColumn("Setting").Centered())
               .AddColumn(new TableColumn("Value").Centered())
               .AddRow("IPAddress", IPAddress)
               .AddRow("Port", Port)
               .AddRow(new Text("Mode"), new Markup($"[{GetModeColor(mode)}]{mode}[/]")));

            // prompting to choose from given option
            var proceedWithSettings = AnsiConsole.Prompt(
            new SelectionPrompt<bool> { Converter = value => value ? "Yes" : "No" }
                .Title("Do you want to proceed with given settings?")
                .AddChoices(true, false));

            if (!proceedWithSettings)
            {
                return;
            }

            var connector = new Connector();

            // waiting for operation to complete
            await AnsiConsole.Status()
                    .StartAsync("Connecting...", migrationResults => connector.ConnectAsync(IPAddress, Port, mode));
             
            // writing bar chart
            AnsiConsole.Write(
                    new BarChart()
                        .Label("Connection results")
                        .AddItem("Succeeded", 70, Spectre.Console.Color.Green)
                        .AddItem("Failed", 30, Spectre.Console.Color.Red));

            return;
        }

        static string GetModeColor(Mode mode)
        {
            return mode switch
            {
                Mode.Https => "green",
                Mode.Http => "yellow",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public class Connector
    {
        public Task ConnectAsync(string IPAddress, string Port, Mode mode)
            => Task.Delay(TimeSpan.FromSeconds(5));
    }
}
