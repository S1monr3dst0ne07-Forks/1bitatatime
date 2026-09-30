using System.Text;

namespace src
{
    public class transpiler
    {
        public string transpileToC(List<token> tokens)
        {
            var sb = new StringBuilder();
            var idMap = new Dictionary<string, byte>();
            sb.AppendLine("#include <stdio.h>");
            sb.AppendLine("#include <stdlib.io");
            sb.AppendLine();

            int pc = 0;
            int program = 0;
            while (pc < tokens.Count)
            {
                if (tokens[pc].type == tokenType.inKeyword)
                {
                    idMap.Clear();
                    idMap["0"] = 0;
                    idMap["1"] = 1;

                    byte nextId = 2;

                    sb.AppendLine($"unsigned long program_{program}(unsigned long ram){{");
                }
            }


            throw new NotImplementedException();
        }
    }
}