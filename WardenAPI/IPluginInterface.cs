using System;

public interface IPluginInterface : IDisposable
{
    void Log(string message);
}