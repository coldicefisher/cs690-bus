namespace BusShuttle;

using System.IO;

public class FileSaver {
    string fileName;

    public FileSaver(string fileName) {
        this.fileName = fileName;
        File.Create(fileName).Close();

    }

    public void AppendLine(string fileName, string line) {
        File.AppendAllText(fileName, line + Environment.NewLine);
    }
}