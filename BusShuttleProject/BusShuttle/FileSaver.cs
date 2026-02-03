namespace BusShuttle;

using System.IO;

public class FileSaver {
    string fileName;

    public FileSaver(string fileName) {
        this.fileName = fileName;
        File.Create(fileName).Close();

    }

    public void AppendData(PassengerData data) {
        string line = $"{data.Driver.Name}:{data.Loop.Name}:{data.Stop.Name}:{data.Boarded}" + Environment.NewLine;
        File.AppendAllText(fileName, line);
    }

    public void AppendLine(string line) {
        File.AppendAllText(fileName, line + Environment.NewLine);
    }
}