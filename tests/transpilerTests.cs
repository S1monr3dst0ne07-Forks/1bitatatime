using src;

namespace tests
{
    [TestFixture]
    public class transpilerTests
    {
        private transpiler transpiler;
        [SetUp]
        public void init()
        {
            transpiler = new transpiler();
        }
        public void transpileGeneratesMainFunctionAndValidatesArgcCount()
        {
            var tokens = new List<token>
            {
                new token { type = tokenType.inKeyword, value = "in:" },
                new token { type = tokenType.identifier, value = "x" },
                new token { type = tokenType.identifier, value = "y" }
            };

            var result = transpiler.transpileToC(tokens);

            Assert.That(result, Does.Contain("int main(int argc, char* argv[])"));
            Assert.That(result, Does.Contain("if (argc < 3)")); // 2 inputs means argc < 3
        }
        [Test]
        public void transpileProcessesNandOperationWithoutSkippingOutKeyword()
        {
            var tokens = new List<token>
            {
                new token { type = tokenType.inKeyword, value = "in:" },
                new token { type = tokenType.identifier, value = "x" },
                new token { type = tokenType.identifier, value = "y" },

                new token { type = tokenType.identifier, value = "gate" },
                new token { type = tokenType.assign, value = "assign" },
                new token { type = tokenType.identifier, value = "x" },
                new token { type = tokenType.nand, value = "nand" },
                new token { type = tokenType.identifier, value = "y" },

                new token { type = tokenType.outKeyword, value = "out:" },
                new token { type = tokenType.identifier, value = "gate" }
            };

            var result = transpiler.transpileToC(tokens);

            Assert.That(result, Does.Contain("res = !(a & b) & 1UL;"));
            Assert.That(result, Does.Contain("printf(\"output:"));
        }
        [Test]
        public void transpileHandlesLiteralZeroAndOneAssignments()
        {
            var tokens = new List<token>
            {
                new token { type = tokenType.inKeyword, value = "in:" },
                new token { type = tokenType.identifier, value = "x" },

                new token { type = tokenType.identifier, value = "setTrue" },
                new token { type = tokenType.assign, value = "assign" },
                new token { type = tokenType.oneLiteral, value = "1" },

                new token { type = tokenType.identifier, value = "setFalse" },
                new token { type = tokenType.assign, value = "assign" },
                new token { type = tokenType.zeroLiteral, value = "0" }
            };

            var result = transpiler.transpileToC(tokens);

            Assert.That(result, Does.Contain("|="));
            Assert.That(result, Does.Contain("&="));
        }
        [Test]
        public void transpileHandlesEmptyTokenStreamsSafely()
        {
            var emptyTokens = new List<token>();

            var result = transpiler.transpileToC(emptyTokens);

            Assert.That(result, Is.EqualTo(string.Empty));
        }
    }
}
