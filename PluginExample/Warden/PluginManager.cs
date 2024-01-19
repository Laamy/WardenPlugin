using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

using Microsoft.CSharp;

class PluginManager
{
    public static List<Plugin> plugins = new List<Plugin>();

    public static void Foreach(Action<Plugin> act)
    {
        foreach (Plugin plugin in plugins)
        {
            act(plugin);
        }
    }

    public static void InitPlugin(string file)
    {
        try
        {
            using (var codeProvider = new CSharpCodeProvider())
            {
                CompilerParameters compilerParameters = new CompilerParameters
                {
                    GenerateInMemory = true,
                    GenerateExecutable = false,

                    ReferencedAssemblies = { "WardenAPI.dll", "System.dll" } // , "System.Linq.dll", "System.Drawing.dll", "System.Windows.Forms.dll"
                };

                CompilerResults compilerResults = codeProvider.CompileAssemblyFromFile(compilerParameters, file);

                if (compilerResults.Errors.Count == 0)
                {
                    foreach (var type in compilerResults.CompiledAssembly.GetExportedTypes())
                    {
                        Console.WriteLine($"[Plugins] Initializing {type.Name}");

                        if (typeof(Plugin).IsAssignableFrom(type) && !type.IsAbstract)
                        {
                            var constructor = type.GetConstructor(Type.EmptyTypes);

                            if (constructor != null)
                            {
                                var plugin = (Plugin)constructor.Invoke(new object[] { });

                                plugin.SetTunnel(new PluginTunnel(type.Name), type.Name);
                                plugin.Initialize();

                                plugins.Add(plugin);
                            }
                            else
                            {
                                Console.WriteLine($"Error loading dynamic plugin {type.Name}: No constructor with MinesweeperGame parameter found.");
                            }
                        }
                    }
                }
                else
                {
                    foreach (CompilerError error in compilerResults.Errors)
                    {
                        Console.WriteLine($"Compilation Error in {file}: {error.ErrorText}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading dynamic plugin from {file}: {ex.Message}");
        }
    }
}