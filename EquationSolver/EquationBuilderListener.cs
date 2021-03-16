using System;
using System.Collections.Generic;
using EquationSolver.Pattern;
using EquationSolver.Model;

namespace EquationSolver {

    public sealed class EquationBuilderListener : EquationGrammarBaseListener {
        private Stack<UnivariateFunction> stack = new Stack<UnivariateFunction>();
        private bool hasVariable = false;

        public UnivariateFunction Build() {
            if (stack.Count != 1) {
                throw new Exception($"Stack size failed : {stack.Count}");
            }
            return stack.Pop();
        }

        public double Calculate() {
            if (hasVariable) {
                throw new Exception("Input has a variable!");
            }
            return Build()(0);
        }

        public override void ExitAddition(EquationGrammarParser.AdditionContext context) {
            if (context.op != null) {
                var op = context.op.Text;
                var post = stack.Pop();
                var prev = stack.Pop();
                
                switch (op) {
                    case "+":
                        stack.Push(x => prev(x) + post(x));
                        break;
                    case "-":
                        stack.Push(x => prev(x) - post(x));
                        break;
                    default:
                        throw new Exception("Something wrong...");
                }
            }
        }
        public override void ExitModulus(EquationGrammarParser.ModulusContext context) {
            if (context.op != null) {
                var op = context.op.Text;
                var post = stack.Pop();
                var prev = stack.Pop();

                switch (op.ToLowerInvariant()) {
                    case "%":
                    case "mod":
                        stack.Push(x => prev(x) % post(x));
                        break;
                }
            }
        }
        public override void ExitMultiplication(EquationGrammarParser.MultiplicationContext context) {
            if (context.power() == null && context.signed_atom() == null) {
                var op = context.op?.Text;
                var post = stack.Pop();
                var prev = stack.Pop();
                if (context.op != null) {
                    switch (op) {
                        case "*":
                        case "×":
                            stack.Push(x => prev(x) * post(x));
                            break;
                        case "/":
                        case "÷":
                            stack.Push(x => prev(x) / post(x));
                            break;
                    }
                } else {
                    stack.Push(x => prev(x) * post(x));
                }

            }
        }

        public override void ExitPower_without_percentage(EquationGrammarParser.Power_without_percentageContext context) {
            if (context.op != null) {
                var op = context.op.Text;
                var post = stack.Pop();
                var prev = stack.Pop();
                
                if ("^".Equals(op)) {
                    stack.Push(x => Math.Pow(prev(x), post(x)));
                }
            }

        }
        
        public override void ExitPower(EquationGrammarParser.PowerContext context) {
            if (context.op != null) {
                var op = context.op.Text;
                var post = stack.Pop();
                var prev = stack.Pop();
                
                if ("^".Equals(op)) {
                    stack.Push(x => Math.Pow(prev(x), post(x)));
                }
            }
        }
        public override void ExitSigned_atom(EquationGrammarParser.Signed_atomContext context) {
            if (context.sign != null) {
                var op = context.sign.Text;

                if ("-".Equals(op)) {
                    var top = stack.Pop();
                    stack.Push(x => - top(x));
                }
            }
        }
        
        public override void ExitAtom_without_percentage(EquationGrammarParser.Atom_without_percentageContext context) {
            if (context.factorial() != null) {
                var top = stack.Pop();
                stack.Push(x => MathNet.Numerics.SpecialFunctions.Gamma(top(x)));
            }
        }
        
        public override void ExitAtom(EquationGrammarParser.AtomContext context) {
            if (context.factorial() != null) {
                var top = stack.Pop();
                stack.Push(x => MathNet.Numerics.SpecialFunctions.Gamma(top(x)));
            } else if (context.percentage() != null) {
                var top = stack.Pop();
                stack.Push(x => top(x) * 0.01);
            }
        }
        
        public override void ExitVariable_with_scalar_product(EquationGrammarParser.Variable_with_scalar_productContext context) {
            var post = stack.Pop();
            var prev = stack.Pop();
            stack.Push(x => prev(x) * post(x));
        }
        
        public override void ExitVariable_with_power(EquationGrammarParser.Variable_with_powerContext context) {
            var post = stack.Pop();
            var prev = stack.Pop();
            stack.Push(x => Math.Pow(prev(x), post(x)));
        }
        
        
        public override void ExitFunctional(EquationGrammarParser.FunctionalContext context) {
            var function = context.function_name().GetText();
            var top = stack.Pop();
            switch (function.ToLowerInvariant()) {
                case "sqrt":
                case "√":
                    stack.Push(top.OutCompose(Math.Sqrt));
                    break;
                case "sin":
                    stack.Push(top.OutCompose(Math.Sin));
                    break;
                case "cos":
                    stack.Push(top.OutCompose(Math.Cos));
                    break;
                case "tan":
                    stack.Push(top.OutCompose(Math.Tan));
                    break;
                case "csc":
                case "cosec":
                    stack.Push(top.OutCompose(x => 1.0 / Math.Sin(x)));
                    break;
                case "sec":
                    stack.Push(top.OutCompose(x => 1.0 / Math.Cos(x)));
                    break;
                case "cot":
                    stack.Push(top.OutCompose(x => 1.0 / Math.Tan(x)));
                    break;
                case "asin":
                case "arcsin":
                    stack.Push(top.OutCompose(Math.Asin));
                    break;
                case "acos":
                case "arccos":
                    stack.Push(top.OutCompose(Math.Acos));
                    break;
                case "atan":
                case "arctan":
                    stack.Push(top.OutCompose(Math.Atan));
                    break;
                case "acsc":
                case "arccsc":
                case "acosec":
                case "arccosec":
                    stack.Push(top.OutCompose(x => 1.0 / Math.Asin(x)));
                    break;
                case "asec":
                case "arcsec":
                    stack.Push(top.OutCompose(x => 1.0 / Math.Acos(x)));
                    break;
                case "acot":
                case "arccot":
                    stack.Push(top.OutCompose(x => 1.0 / Math.Atan(x)));
                    break;
                case "sinh":
                    stack.Push(top.OutCompose(Math.Sinh));
                    break;
                case "cosh":
                    stack.Push(top.OutCompose(Math.Cosh));
                    break;
                case "tanh":
                    stack.Push(top.OutCompose(Math.Tanh));
                    break;
                case "csch":
                case "cosech":
                    stack.Push(top.OutCompose(x => 1.0 / Math.Sinh(x)));
                    break;
                case "sech":
                    stack.Push(top.OutCompose(x => 1.0 / Math.Cosh(x)));
                    break;
                case "coth":
                    stack.Push(top.OutCompose(x => 1.0 / Math.Tanh(x)));
                    break;
                case "asinh":
                case "arcsinh":
                    stack.Push(top.OutCompose(Math.Asinh));
                    break;
                case "acosh":
                case "arccosh":
                    stack.Push(top.OutCompose(Math.Acosh));
                    break;
                case "atanh":
                case "arctanh":
                    stack.Push(top.OutCompose(Math.Atanh));
                    break;
                case "acsch":
                case "arccsch":
                case "acosech":
                case "arccosech":
                    stack.Push(top.OutCompose(x => 1.0 / Math.Asinh(x)));
                    break;
                case "asech":
                case "arcsech":
                    stack.Push(top.OutCompose(x => 1.0 / Math.Acosh(x)));
                    break;
                case "acoth":
                case "arccoth":
                    stack.Push(top.OutCompose(x => 1.0 / Math.Atanh(x)));
                    break;
                case "ln":
                    stack.Push(top.OutCompose(Math.Log));
                    break;
                case "log":
                    stack.Push(top.OutCompose(Math.Log10));
                    break;
                case "exp":
                    stack.Push(top.OutCompose(Math.Exp));
                    break;
            }
        }
        
        
        public override void ExitNumber(EquationGrammarParser.NumberContext context) {
            if (double.TryParse(context.NUMBER().GetText(), out double parsed)) {
                stack.Push(x => parsed);
            }
        }
        
        public override void ExitVariable(EquationGrammarParser.VariableContext context) {
            hasVariable = true;
            stack.Push(x => x);
        }
        
        public override void ExitConstant(EquationGrammarParser.ConstantContext context) {
            var c = context.GetText();
            var constant = c.ToLowerInvariant() switch {
                "pi" => Math.PI,
                "π" => Math.PI,
                "e" => Math.E,
                _ => throw new Exception($"Unexpected token : {c}")
            };
            
            stack.Push(x => constant);
        }
    }
}