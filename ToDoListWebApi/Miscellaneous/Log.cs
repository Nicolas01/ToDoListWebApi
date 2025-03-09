namespace ToDoListWebApi.Miscellaneous;

public static partial class Log
{
    [LoggerMessage(Level = LogLevel.Debug, Message = "{Method} {Path}\nHeaders: {Headers}\nBody: {Body}")]
    public static partial void LogRequest(ILogger logger, string Method, string Path, string Headers, string Body);
}
