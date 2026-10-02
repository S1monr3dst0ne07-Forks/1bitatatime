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

            File.WriteAllText(sourceFile.Split('.')[0] + ".c", c);

        }
    }
}
