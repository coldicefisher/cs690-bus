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
        } else if (mode == "manager") {
            
            string command;
            do {
                command = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select an action:")
                        .AddChoices(new[] {
                            "show busiest stop", "add stop", "delete stop", "list stops", "end"
                        }));

                if (command == "add stop") {
                    string newStopName = AskForInput("Enter new stop name: ");
                    dataManager.AddStop(new Stop(newStopName));
                    dataManager.SynchronizeStops();

                    dataManager.SynchronizeStops();
                    Console.WriteLine("New stop added: " + newStopName);


                } else if (command == "delete stop") {
                    Stop stopToDelete = AnsiConsole.Prompt(
                        new SelectionPrompt<Stop>()
                            .Title("Select a stop to delete:")
                            .AddChoices(dataManager.Stops));
                    
                    dataManager.RemoveStop(stopToDelete);
                    dataManager.SynchronizeStops();
                    Console.WriteLine("Stop deleted: " + stopToDelete.Name);

                
                } else if (command == "list stops") {
                    var table = new Table();
                    table.AddColumn("Stop Name");
                    
                    Console.WriteLine("Current stops:");
                    foreach (var stop in dataManager.Stops) {
                        table.AddRow(stop.Name);
                    }
                    AnsiConsole.Write(table);
                } else if (command == "show busiest stop") {
                    var result = Reporter.FindBusiestStop(dataManager.PassengerData);
                    
                    Console.WriteLine("Busiest stop is: " + result.Name);
                    
                }

            } while (command != "end");
        }
    }

    public static string AskForInput(string message) {
        Console.Write(message);
        return Console.ReadLine();
    }
}
