using simulator_cli_client.Enums;
using System.Collections.Generic;

namespace simulator_cli_client.Constants;

public static class CLIConstants
{
    public const string PROGRAM_NAME = "UAV Simulator";
    public const string MENU_TITLE = "[bold yellow]Choose an action:[/]";
    public const string PROMPT_RETURN_TO_MENU = "[grey]Press any key to return to menu...[/]";
    public const int PAGE_SIZE = 10;

    public static readonly IReadOnlyDictionary<MenuAction, string> MenuActionLabels = new Dictionary<MenuAction, string>
    {
        [MenuAction.ListAllDevices] = "1. List All Devices",
        [MenuAction.StartAllDevices] = "2. Start All Devices",
        [MenuAction.StopAllDevices] = "3. Stop All Devices",
        [MenuAction.StartSpecificDevice] = "4. Start Specific Device",
        [MenuAction.StopSpecificDevice] = "5. Stop Specific Device",
        [MenuAction.AddDevice] = "6. Add Device",
        [MenuAction.RemoveDevice] = "7. Remove Device",
        [MenuAction.Exit] = "0. Exit"
    };

    public const string BASE_PROXY_ADDRESS = "http://localhost:5264/";
    public const string MultimediaFileName = "MultimediaFile";
    public const string TelemetryFileName = "TelemetryFile";

    public const string DeviceNameFile = "deviceName";
    public const string UNKNOWN_ERROR = "Unknown error";

    public static class ProxyAPIs
    {
        public const string BASE_ROUTE = "api/v1.0/sp/Devices";
        public const string START_DEVICE_ROUTE = BASE_ROUTE + "/Start";
        public const string STOP_DEVICE_ROUTE = BASE_ROUTE + "/Stop";
        public const string STARTALL_DEVICE_ROUTE = BASE_ROUTE + "/StartAll";
        public const string STOPALL_DEVICE_ROUTE = BASE_ROUTE + "/StopAll";
    }

    public static class UI
    {
        // Status spinners
        public const string STATUS_FETCHING_DEVICES = "Fetching devices...";
        public const string STATUS_STARTING_ALL = "Starting all devices...";
        public const string STATUS_STOPPING_ALL = "Stopping all devices...";
        public const string STATUS_STARTING_DEVICE = "Starting device {0}...";
        public const string STATUS_STOPPING_DEVICE = "Stopping device {0}...";
        public const string STATUS_REMOVING_DEVICE = "Removing device {0}...";
        public const string STATUS_ADDING_DEVICE = "Uploading and creating device...";

        // Prompts & Questions
        public const string PROMPT_SELECT_DEVICE_START = "Select a device to [green]start[/]:";
        public const string PROMPT_SELECT_DEVICE_STOP = "Select a device to [red]stop[/]:";
        public const string PROMPT_SELECT_DEVICE_DELETE = "Select a device to [red]delete[/]:";
        public const string CONFIRM_DELETE_DEVICE = "Are you sure you want to delete [red]{0}[/]?";
        public const string ASK_TELEMETRY_PATH = "Enter path to [cyan]Telemetry File[/]:";
        public const string ASK_DEVICE_NAME = "Enter [cyan]Device Name[/]:";
        public const string ASK_MULTIMEDIA_PATH = "Enter path to [cyan]Multimedia File[/]:";

        // Warnings & Info
        public const string WARN_NO_DEVICES_FOUND = "[yellow]No devices found.[/]";
        public const string WARN_NO_DEVICES_TO_START = "[yellow]No devices available to start.[/]";
        public const string WARN_NO_DEVICES_TO_STOP = "[yellow]No devices available to stop.[/]";
        public const string WARN_NO_DEVICES_TO_REMOVE = "[yellow]No devices available to remove.[/]";
        public const string INFO_ACTION_CANCELED = "[yellow]Action canceled.[/]";

        // Table headers
        public const string TABLE_COL_INDEX = "[bold yellow]#[/]";
        public const string TABLE_COL_DEVICE_NAME = "[bold yellow]Device Name[/]";

        // Success / Failure messages
        public const string MSG_START_ALL_SUCCESS = "All devices started successfully.";
        public const string MSG_STOP_ALL_SUCCESS = "All devices stopped successfully.";
        public const string MSG_START_DEVICE_SUCCESS = "[green] Device '{0}' started successfully.[/]";
        public const string MSG_STOP_DEVICE_SUCCESS = "[green] Device '{0}' stopped successfully.[/]";
        public const string MSG_REMOVE_DEVICE_SUCCESS = "[green] Device '{0}' removed successfully.[/]";
        public const string MSG_ADD_DEVICE_SUCCESS = "[green] Device added successfully![/]";

        public const string ERR_ACTION_FAILED = "[red] Failed: {0}[/]";
        public const string ERR_START_DEVICE_FAILED = "[red] Failed to start device '{0}'.[/]";
        public const string ERR_STOP_DEVICE_FAILED = "[red] Failed to stop device '{0}'.[/]";
        public const string ERR_REMOVE_DEVICE_FAILED = "[red] Failed to remove device '{0}'.[/]";
        public const string ERR_ADD_DEVICE_FAILED = "[red] Failed to add device. Please verify file paths and server logs.[/]";

        // Exception Handler Messages
        public const string ERR_NETWORK = "[bold red]✖ Network/API Error:[/] [red]{0}[/]";
        public const string ERR_NETWORK_HINT = "[grey]Please verify that the UAV Proxy Server is running.[/]";
        public const string ERR_FILE_NOT_FOUND = "[bold red] File Not Found:[/] [red]{0}[/]";
        public const string ERR_TIMEOUT = "[bold yellow] Request timed out or was canceled.[/]";
        public const string ERR_UNEXPECTED = "[bold red] Unexpected Error:[/] [red]{0}[/]";
    }
}