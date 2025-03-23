  public interface IAppLogger<T>
  {
      void LogInformacion(string message, params object[] args);
      void LogWarning(string message, params object[] args);
      void LogError(string message, params object[] args);
      void LogMessageWithEventAndId(string message, int eventId, string identifier, string dataLog);
  }