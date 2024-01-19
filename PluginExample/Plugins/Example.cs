using System;

public class Example : Plugin
{
    public override void Initialize()
    {
        PluginTunnel.Log("Loaded plugin " + Name + " on API version " + API.Version);
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}