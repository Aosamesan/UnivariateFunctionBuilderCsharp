# UnivariateFunction Builder
## Example : `4x+3`
* Code
```cs
string example = "4x+3";
UnivaraiteFunction function = EquationBuilder.Build(example);
for (int i = 0; i < 5; i++) {
    Console.WriteLine($"f({i})={function(i)}");
}
```
* Ouput
```
f(0)=3
f(1)=7
f(2)=11
f(3)=15
f(4)=19
```

## Example : `4sqrtx`
* Code
```cs
string example = "4sqrtx";
UnivaraiteFunction function = EquationBuilder.Build(example);
for (int i = 0; i < 5; i++) {
    Console.WriteLine($"f({i})={function(i)}");
}
```
* Ouput
```
f(0)=0
f(1)=4
f(2)=5.656854249492381
f(3)=6.928203230275509
f(4)=8
```

## Example : `sinx+cosx`
```cs
string input = "sinx+cosx";
var func = EquationBuilder.Build(input);
for (int i = 0; i < 5; i++) {
    double x = i * Math.PI / 3;
    Console.WriteLine($"f({i}π/3)={func(x)}");
}}
```
* Ouput
```
f(0π/3)=1
f(1π/3)=1.3660254037844388
f(2π/3)=0.36602540378443904
f(3π/3)=-0.9999999999999999 // -1
f(4π/3)=-1.3660254037844388
```

## Example : `(1+sqrt5)/2`
* Code
```cs
string input = "(1+sqrt5)/2";
var result = EquationBuilder.Calculate(input);
System.Console.WriteLine($"{input}={result}");
```

* Output
```
(1+sqrt5)/2=1.618033988749895
```
