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
            string outBinary = sourceFile.Split('.')[0];

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                outBinary += ".exe";

            outBinary = Path.Combine(AppContext.BaseDirectory, outBinary);

            string source = File.ReadAllText(sourceFile);
            var lexer = new lexer(source);
            var transpiler = new transpiler();

            var tokens = lexer.lex();
            string c = transpiler.transpileToC(tokens);

            var startInfo = new ProcessStartInfo
            {
                FileName = "gcc",
                Arguments = $"-O3 -x c - -o \"{outBinary}\"",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process compiler = new Process { StartInfo = startInfo })
            {
                try
                {
                    compiler.Start();

                    using (StreamWriter writer = compiler.StandardInput)
                        if (writer.BaseStream.CanWrite)
                            writer.Write(c);

                    string errors = compiler.StandardError.ReadToEnd();
                    compiler.WaitForExit();

                    switch (compiler.ExitCode)
                    {
                        case 0:
                            Console.WriteLine($"success! compiled binary: {outBinary}");
                            break;
                        default:
                            Console.WriteLine($"error: {errors}");
                            break;
                    }
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    Console.WriteLine("error: gcc is not installed on your machine");
                }
            }


            File.WriteAllText(outBinary + ".c", c);

        }
    }
}
