namespace src
{
    public class lexer
    {
        private string source;
        public lexer(string source) => this.source = source;
        public List<token> lex()
        {
            bool active = false;
            var tokens = new List<token>();
            var lines = source.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                if (line.StartsWith("in:"))
                {
                    active = true;
                    tokens.Add(new token { type = tokenType.inKeyword, value = "in:" });
                    var inputs = line.Substring(3).Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    foreach (var input in inputs)
                        tokens.Add(new token { type = tokenType.identifier, value = input });

                    continue;
                }

                if (!active) continue;

                if (line.StartsWith("out:"))
                {
                    tokens.Add(new token { type = tokenType.outKeyword, value = "out:" });
                    var outputs = line.Substring(4).Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    foreach (var output in outputs)
                        tokens.Add(new token { type = tokenType.identifier, value = output });

                    active = false;
                    continue;
                }

                var args = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var arg in args)
                    switch (arg)
                    {
                        case "=":
                            tokens.Add(new token { type = tokenType.assign, value = "assign" });
                            break;

                        case "!&":
                        case "nand":
                            tokens.Add(new token { type = tokenType.nand, value = "nand" });
                            break;

                        case "1":
                        case "true":
                            tokens.Add(new token { type = tokenType.oneLiteral, value = "1" });
                            break;

                        case "0":
                        case "false":
                            tokens.Add(new token { type = tokenType.zeroLiteral, value = "0" });
                            break;

                        default:
                            tokens.Add(new token { type = tokenType.identifier, value = arg });
                            break;
                    }
            }

            return tokens;
        }
    }
}
