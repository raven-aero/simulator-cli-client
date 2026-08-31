using Spectre.Console;
using simulator_cli_client.Constants;
using simulator_cli_client.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace simulator_cli_client.Handlers;

public static class DeviceCliHandlers
{
    public static async Task ExecuteSafeAsync(Func<Task> action)
    {
        try
        {
            await action();
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.MarkupLine(string.Format(CLIConstants.UI.ERR_NETWORK, ex.Message));
            AnsiConsole.MarkupLine(CLIConstants.UI.ERR_NETWORK_HINT);
        }
        catch (FileNotFoundException ex)
        {
            AnsiConsole.MarkupLine(string.Format(CLIConstants.UI.ERR_FILE_NOT_FOUND, ex.FileName));
        }
        catch (TaskCanceledException)
        {
            AnsiConsole.MarkupLine(CLIConstants.UI.ERR_TIMEOUT);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine(string.Format(CLIConstants.UI.ERR_UNEXPECTED, ex.Message));
        }
    }

    public static async Task HandleListAllDevicesAsync(IUAVSimulatorCilent client)
    {
        var devices = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .StartAsync(CLIConstants.UI.STATUS_FETCHING_DEVICES, async _ => await client.GetAllDevicesAsync());

        if (devices.Count == 0)
        {
            AnsiConsole.MarkupLine(CLIConstants.UI.WARN_NO_DEVICES_FOUND);
            return;
        }

        var table = new Table().Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn(CLIConstants.UI.TABLE_COL_INDEX).Centered());
        table.AddColumn(new TableColumn(CLIConstants.UI.TABLE_COL_DEVICE_NAME));

        for (int i = 0; i < devices.Count; i++)
        {
            table.AddRow((i + 1).ToString(), $"[cyan]{devices[i]}[/]");
        }

        AnsiConsole.Write(table);
    }

    public static async Task HandleStartAllDevicesAsync(IUAVSimulatorCilent client)
    {
        var result = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .StartAsync(CLIConstants.UI.STATUS_STARTING_ALL, async _ => await client.StartAllDevicesChannelsAsync());

        if (result.Success)
            AnsiConsole.MarkupLine($"[green] {result.Message ?? CLIConstants.UI.MSG_START_ALL_SUCCESS}[/]");
        else
            AnsiConsole.MarkupLine(string.Format(CLIConstants.UI.ERR_ACTION_FAILED, result.Message));
    }

    public static async Task HandleStopAllDevicesAsync(IUAVSimulatorCilent client)
    {
        var result = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .StartAsync(CLIConstants.UI.STATUS_STOPPING_ALL, async _ => await client.StopAllDevicesChannelsAsync());

        if (result.Success)
            AnsiConsole.MarkupLine($"[green] {result.Message ?? CLIConstants.UI.MSG_STOP_ALL_SUCCESS}[/]");
        else
            AnsiConsole.MarkupLine(string.Format(CLIConstants.UI.ERR_ACTION_FAILED, result.Message));
    }

    public static async Task HandleStartSpecificDeviceAsync(IUAVSimulatorCilent client)
    {
        var devices = await client.GetAllDevicesAsync();
        if (devices.Count == 0)
        {
            AnsiConsole.MarkupLine(CLIConstants.UI.WARN_NO_DEVICES_TO_START);
            return;
        }

        var selectedDevice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(CLIConstants.UI.PROMPT_SELECT_DEVICE_START)
                .AddChoices(devices));

        var success = await AnsiConsole.Status()
            .StartAsync(string.Format(CLIConstants.UI.STATUS_STARTING_DEVICE, selectedDevice), async _ => await client.StartDeviceChannelsAsync(selectedDevice));

        if (success)
            AnsiConsole.MarkupLine(string.Format(CLIConstants.UI.MSG_START_DEVICE_SUCCESS, selectedDevice));
        else
            AnsiConsole.MarkupLine(string.Format(CLIConstants.UI.ERR_START_DEVICE_FAILED, selectedDevice));
    }

    public static async Task HandleStopSpecificDeviceAsync(IUAVSimulatorCilent client)
    {
        var devices = await client.GetAllDevicesAsync();
        if (devices.Count == 0)
        {
            AnsiConsole.MarkupLine(CLIConstants.UI.WARN_NO_DEVICES_TO_STOP);
            return;
        }

        var selectedDevice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(CLIConstants.UI.PROMPT_SELECT_DEVICE_STOP)
                .AddChoices(devices));

        var success = await AnsiConsole.Status()
            .StartAsync(string.Format(CLIConstants.UI.STATUS_STOPPING_DEVICE, selectedDevice), async _ => await client.StopDeviceChannelsAsync(selectedDevice));

        if (success)
            AnsiConsole.MarkupLine(string.Format(CLIConstants.UI.MSG_STOP_DEVICE_SUCCESS, selectedDevice));
        else
            AnsiConsole.MarkupLine(string.Format(CLIConstants.UI.ERR_STOP_DEVICE_FAILED, selectedDevice));
    }

    public static async Task HandleRemoveDeviceAsync(IUAVSimulatorCilent client)
    {
        var devices = await client.GetAllDevicesAsync();
        if (devices.Count == 0)
        {
            AnsiConsole.MarkupLine(CLIConstants.UI.WARN_NO_DEVICES_TO_REMOVE);
            return;
        }

        var selectedDevice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(CLIConstants.UI.PROMPT_SELECT_DEVICE_DELETE)
                .AddChoices(devices));

        var confirm = AnsiConsole.Confirm(string.Format(CLIConstants.UI.CONFIRM_DELETE_DEVICE, selectedDevice), defaultValue: false);
        if (!confirm)
        {
            AnsiConsole.MarkupLine(CLIConstants.UI.INFO_ACTION_CANCELED);
            return;
        }

        var success = await AnsiConsole.Status()
            .StartAsync(string.Format(CLIConstants.UI.STATUS_REMOVING_DEVICE, selectedDevice), async _ => await client.RemoveDeviceAsync(selectedDevice));

        if (success)
            AnsiConsole.MarkupLine(string.Format(CLIConstants.UI.MSG_REMOVE_DEVICE_SUCCESS, selectedDevice));
        else
            AnsiConsole.MarkupLine(string.Format(CLIConstants.UI.ERR_REMOVE_DEVICE_FAILED, selectedDevice));
    }

    public static async Task HandleAddDeviceAsync(IUAVSimulatorCilent client)
    {
        var deviceName = AnsiConsole.Ask<string>(CLIConstants.UI.ASK_DEVICE_NAME).Trim();
        var telemetryPath = AnsiConsole.Ask<string>(CLIConstants.UI.ASK_TELEMETRY_PATH).Trim('"', ' ');
        var multimediaPath = AnsiConsole.Ask<string>(CLIConstants.UI.ASK_MULTIMEDIA_PATH).Trim('"', ' ');

        var success = await AnsiConsole.Status()
            .StartAsync(CLIConstants.UI.STATUS_ADDING_DEVICE, async _ => await client.AddDeviceAsync(deviceName, telemetryPath, multimediaPath));

        if (success)
            AnsiConsole.MarkupLine(CLIConstants.UI.MSG_ADD_DEVICE_SUCCESS);
        else
            AnsiConsole.MarkupLine(CLIConstants.UI.ERR_ADD_DEVICE_FAILED);
    }
}