using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using EquationSolver.Model;
using EquationSolver.Pattern;

namespace EquationSolver {
    public static class EquationBuilder {
        public static UnivariateFunction Build(string input) {
            var stream = CharStreams.fromstring(input);
            var lexer = new EquationGrammarLexer(stream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new EquationGrammarParser(tokenStream);
            var listener = new EquationBuilderListener();
            var tree = parser.statement();
            ParseTreeWalker.Default.Walk(listener, tree);
            return listener.Build();
        }

        public static double Calculate(string input) {
            var stream = CharStreams.fromstring(input);
            var lexer = new EquationGrammarLexer(stream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new EquationGrammarParser(tokenStream);
            var listener = new EquationBuilderListener();
            var tree = parser.statement();
            ParseTreeWalker.Default.Walk(listener, tree);
            return listener.Calculate();
        }
    }
}