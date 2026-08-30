using Spectre.Console;
using simulator_cli_client.Constants;
using simulator_cli_client.Enums;

namespace simulator_cli_client.Program;

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText(CLIConstants.PROGRAM_NAME).LeftJustified().Color(Color.Cyan3));

            MenuAction choice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuAction>()
                    .Title(CLIConstants.MENU_TITLE)
                    .PageSize(CLIConstants.PAGE_SIZE)
                    .UseConverter(action => CLIConstants.MenuActionLabels[action])
                    .AddChoices(CLIConstants.MenuActionLabels.Keys));

            if (choice == MenuAction.Exit) break;

            switch (choice)
            {
                case MenuAction.ListAllDevices:
                    AnsiConsole.MarkupLine(MenuAction.ListAllDevices.ToString());
                    break;

                case MenuAction.StartAllDevices:
                    AnsiConsole.MarkupLine(MenuAction.StartAllDevices.ToString());
                    break;

                case MenuAction.StopAllDevices:
                    AnsiConsole.MarkupLine(MenuAction.StopAllDevices.ToString());
                    break;

                case MenuAction.StartSpecificDevice:
                    AnsiConsole.MarkupLine(MenuAction.StartSpecificDevice.ToString());
                    break;

                case MenuAction.StopSpecificDevice:
                    AnsiConsole.MarkupLine(MenuAction.StopSpecificDevice.ToString());
                    break;

                case MenuAction.AddDevice:
                    AnsiConsole.MarkupLine(MenuAction.AddDevice.ToString());
                    break;

                case MenuAction.RemoveDevice:
                    AnsiConsole.MarkupLine(MenuAction.RemoveDevice.ToString());
                    break;
            }

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine(CLIConstants.PROMPT_RETURN_TO_MENU);
            Console.ReadKey(true);
        }
    }
}