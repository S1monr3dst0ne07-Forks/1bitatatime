using src;
using NUnit.Framework;

namespace tests
{
    [TestFixture]
    public class lexerTests
    {
        private string source;
        private lexer lexer;
        [Test]
        public void lexIgnoresEverythingBeforeInAndAfterOut()
        {
            source = "x !& b\nin: x y\nx = x nand y\nout: y";
            lexer = new(source);
            var actual = lexer.lex();
            var expected = new List<token>
            {
                new token { type = tokenType.inKeyword, value = "in:" },
                new token { type = tokenType.identifier, value = "x" },
                new token { type = tokenType.identifier, value = "y" },
                new token { type = tokenType.identifier, value = "x" },
                new token { type = tokenType.assign, value = "assign" },
                new token { type = tokenType.identifier, value = "x" },
                new token { type = tokenType.nand, value = "nand" },
                new token { type = tokenType.identifier, value = "y" },
                new token { type = tokenType.outKeyword, value = "out:" },
                new token { type = tokenType.identifier, value = "y" }
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void lexReturnsEmptyListIfInNotPresent()
        {
            source = "x !& y\nout x y";
            lexer = new(source);
            var actual = lexer.lex();
            var expected = new List<token>();
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void lexHandlesAlternateNandSyntaxToken()
        {
            source = "in: x\na = x !& x\nb = x nand x\nout: a";
            lexer = new(source);
            var actual = lexer.lex();
            var expected = new List<token>
            {
                new token { type = tokenType.inKeyword, value = "in:" },
                new token { type = tokenType.identifier, value = "x" },
                new token { type = tokenType.identifier, value = "a" },
                new token { type = tokenType.assign, value = "assign" },
                new token { type = tokenType.identifier, value = "x" },
                new token { type = tokenType.nand, value = "nand" },
                new token { type = tokenType.identifier, value = "x" },
                new token { type = tokenType.identifier, value = "b" },
                new token { type = tokenType.assign, value = "assign" },
                new token { type = tokenType.identifier, value = "x" },
                new token { type = tokenType.nand, value = "nand" },
                new token { type = tokenType.identifier, value = "x" },
                new token { type = tokenType.outKeyword, value = "out:" },
                new token { type = tokenType.identifier, value = "a" },
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
