using simulator_cli_client.Enums;

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
}