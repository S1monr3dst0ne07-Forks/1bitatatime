using System.Diagnostics;
using System.Runtime.InteropServices;

namespace src
{
    internal class program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("error: no arguments given");
                return;
            }
            string sourceFile = args[0];
            string source = File.ReadAllText(sourceFile);
            var lexer = new lexer(source);
            var transpiler = new transpiler();

            List<token> tokens = lexer.lex();
            string c = transpiler.transpileToC(tokens);


            string tempDir = Path.GetTempPath();
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            string binaryPath = Path.Combine(tempDir, $"memory_compiled_{(isWindows ? "app.exe" : "app.out")}");

            string compiler = !isWindows && File.Exists("/usr/bin/clang") ? "clang" : "gcc";

            var startInfo = new ProcessStartInfo
            {
                FileName = compiler,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
            };

            startInfo.ArgumentList.Add("-x");
            startInfo.ArgumentList.Add("c");
            startInfo.ArgumentList.Add("-");
            startInfo.ArgumentList.Add("-o");
            startInfo.ArgumentList.Add(binaryPath);

            using (var process = Process.Start(startInfo))
            {
                if (process == null) return;

                using (StreamWriter sw = process.StandardInput)
                {
                    sw.Write(c);
                }

                string errors = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    Console.WriteLine($"error:\n{errors}");
                    return;
                }
            }

            Process.Start(new ProcessStartInfo { FileName = binaryPath, UseShellExecute = false })?.WaitForExit();

            if (File.Exists(binaryPath)) File.Delete(binaryPath);

            return;
        }
    }
}
