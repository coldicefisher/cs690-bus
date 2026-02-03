namespace BusShuttle;
using Spectre.Console;


public class ConsoleUI {
    

    DataManager dataManager;

    public ConsoleUI() {

        this.dataManager = new DataManager();

    }


    public void Show() {

        // string mode = AskForInput("Please select mode (driver of manager): ");
        string mode = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select mode:")
                .AddChoices(new[] {
                    "driver", "manager"
                }));

        if (mode == "driver") {
            Driver selectedDriver = AnsiConsole.Prompt(
                new SelectionPrompt<Driver>()
                    .Title("Select a driver")
                    .AddChoices(dataManager.Drivers));

            Console.WriteLine("Now you are driving as " + selectedDriver.Name + "!");


            Loop selectedLoop = AnsiConsole.Prompt(
                new SelectionPrompt<Loop>()
                    .Title("Select a loop")
                    .AddChoices(dataManager.Loops));
            
            
            Console.WriteLine("You have selected  " + selectedLoop.Name + " loop!");

            string command;

            do {
                // string stopName = AskForInput("Enter stop name: ");

                Stop selectedStop = AnsiConsole.Prompt(
                new SelectionPrompt<Stop>()
                    .Title("Select a stop")
                    .AddChoices(selectedLoop.Stops));

                Console.WriteLine("You have selected " + selectedStop.Name + " stop!");

                int boarded = int.Parse(AskForInput("Enter number of boarded passengers: "));

                PassengerData data = new PassengerData(boarded, selectedStop, selectedLoop, selectedDriver);

                dataManager.AddNewPassengerData(data);
                // fileSaver.AppendLine("passenger-data.txt", selectedStop.Name + ":" + boarded);
                // command = AskForInput("Enter command (end OR continue): ");
                command = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Enter command:")
                        .AddChoices(new[] {
                            "continue", "end"
                        }));
            } while (command != "end");
        }
    }

    public static string AskForInput(string message) {
        Console.Write(message);
        return Console.ReadLine();
    }
}
