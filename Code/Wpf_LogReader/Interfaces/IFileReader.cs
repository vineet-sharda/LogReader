namespace Wpf_LogReader
{
    public interface IFileReader
    {
        FileHandler FileHandler { get; set; }
        LogGroupCollection Logs { get; set; }
        void LoadLogs();
        string GetFullLog(string filePath, int lineNumber);
    }
}