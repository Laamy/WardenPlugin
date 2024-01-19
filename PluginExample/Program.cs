using System;

class Program
{
    static void Main(string[] args)
    {
        PluginManager.InitPlugin("Plugins\\Example.cs");

        while (true) {}
    }
}