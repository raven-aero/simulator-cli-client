using Spectre.Console;
using simulator_cli_client.Constants;
using simulator_cli_client.Enums;
using simulator_cli_client.Handlers;
using simulator_cli_client.Interfaces;
using simulator_cli_client.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace simulator_cli_client.Program;

public class Program
{
    public static async Task Main(string[] args)
    {
        var httpClient = new HttpClient { BaseAddress = new Uri(CLIConstants.BASE_PROXY_ADDRESS) };
        IUAVSimulatorCilent proxyClient = new UAVSimulatorCilent(httpClient);

        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new FigletText(CLIConstants.PROGRAM_NAME).LeftJustified().Color(Color.Cyan3));

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuAction>()
                    .Title(CLIConstants.MENU_TITLE)
                    .PageSize(CLIConstants.PAGE_SIZE)
                    .UseConverter(action => CLIConstants.MenuActionLabels[action])
                    .AddChoices(CLIConstants.MenuActionLabels.Keys));

            if (choice == MenuAction.Exit) break;

            await DeviceCliHandlers.ExecuteSafeAsync(async () =>
            {
                switch (choice)
                {
                    case MenuAction.ListAllDevices:
                        await DeviceCliHandlers.HandleListAllDevicesAsync(proxyClient);
                        break;
                    case MenuAction.StartAllDevices:
                        await DeviceCliHandlers.HandleStartAllDevicesAsync(proxyClient);
                        break;
                    case MenuAction.StopAllDevices:
                        await DeviceCliHandlers.HandleStopAllDevicesAsync(proxyClient);
                        break;
                    case MenuAction.StartSpecificDevice:
                        await DeviceCliHandlers.HandleStartSpecificDeviceAsync(proxyClient);
                        break;
                    case MenuAction.StopSpecificDevice:
                        await DeviceCliHandlers.HandleStopSpecificDeviceAsync(proxyClient);
                        break;
                    case MenuAction.GetActiveStreams:
                        await DeviceCliHandlers.HandleListActiveStreamsAsync(proxyClient);
                        break;
                    case MenuAction.AddDevice:
                        await DeviceCliHandlers.HandleAddDeviceAsync(proxyClient);
                        break;
                    case MenuAction.RemoveDevice:
                        await DeviceCliHandlers.HandleRemoveDeviceAsync(proxyClient);
                        break;
                }
            });

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine(CLIConstants.PROMPT_RETURN_TO_MENU);
            Console.ReadKey(true);
        }
    }
}