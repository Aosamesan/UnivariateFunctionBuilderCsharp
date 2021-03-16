namespace EquationSolver.Model {
    public delegate double UnivariateFunction(double x);

    public static class UnivariateFunctionUtils {
        public static UnivariateFunction Wrap(UnivariateFunction function) {
            return function;
        }

        public static UnivariateFunction Compose(this UnivariateFunction function, UnivariateFunction inner) {
            return x => function(inner(x));
        }

        public static UnivariateFunction OutCompose(this UnivariateFunction function, UnivariateFunction outer) {
            return x => outer(function(x));
        }
    }
}