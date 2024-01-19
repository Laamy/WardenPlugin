using System;

public abstract class Plugin : IDisposable
{
    private string _name;
    public IPluginInterface PluginTunnel { get; private set; }

    public Plugin() { }

    public string Name
    { get => _name; }

    public void SetTunnel(IPluginInterface Logger, string name)
    {
        PluginTunnel = Logger;
        _name = name;
    }

    public virtual void Initialize() { }

    public virtual void Dispose()
    {
        _name = null;

        PluginTunnel.Dispose();
        PluginTunnel = null;
    }
}