using System;
using System.Diagnostics;

namespace AltexDBG
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[AltexDBG]-> Initializing AltexDBG...");

            // Start Main Game with Debugging and logging set to output to the console.

            ProcessStartInfo _procStart = new ProcessStartInfo()
            {
                FileName = "NOTF.exe",
                UseShellExecute = false,
                LoadUserProfile = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                ErrorDialog = false,
            };

            Process _procMain = null;
            
            try
            {
                _procMain = Process.Start(_procStart);
            }
            catch (Exception)
            {
                Console.WriteLine($"[PROC]-> Process '{_procStart.FileName}' could not be found.");
                return;
            }

            if (_procMain.Start())
            {
                Console.WriteLine($"[PROC]-> Process '{_procMain.ProcessName}' has started successfully.");
            }
            else
            {
                Console.WriteLine($"[PROC]-> Process '{_procMain.ProcessName}' encountered an error while attempting to start.");
                return;
            }


            _procMain.Exited += (_, _) =>
            {
                Console.WriteLine($"[PROC]-> Process '{_procMain.ProcessName}' has been killed.");
            };

            _procMain.OutputDataReceived += (_, output) =>
            {
                Console.WriteLine($"[OUT]-> {output.Data}");
            };

            _procMain.ErrorDataReceived += (_, output) =>
            {
                Console.WriteLine($"[ERR]-> {output.Data}");
            };

            _procMain.BeginOutputReadLine();

            _procMain.BeginErrorReadLine();

            _procMain.WaitForExit();

            Console.WriteLine("[AltexDBG]-> Altex Debugger has been disabled.");

            Console.ReadKey();
        }
    }
}
