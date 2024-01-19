﻿using System;

public class PluginTunnel : IPluginInterface
{
    string plugName;

    public PluginTunnel(string pluginName)
    {
        plugName = pluginName;
    }

    public void Log(string message)
    {
        Console.WriteLine($"[{plugName}] {message}");
    }

    public void Dispose()
    {
        plugName = null;
    }
}