grammar EquationGrammar;

/**
PARSER RULES
**/
statement
    : addition EOF
;

addition
    : modulus
    | addition op=(PLUS | MINUS) addition
;

modulus
    : multiplication
    | modulus op=(MODULUS | PERCENT) modulus
    | modulus op=(MODULUS | PERCENT) LPAREN addition RPAREN
;

multiplication
    : power
    | signed_atom
    | multiplication op=(ASTER | SLASH) multiplication
    | LPAREN addition RPAREN multiplication
    | multiplication power_without_percentage
;

power_without_percentage
    : atom_without_percentage
    | <assoc=right> power_without_percentage op=POWER (power | signed_atom)
;

power
    : atom
    | <assoc=right> power op=POWER (power | signed_atom)
;

signed_atom
    : <assoc=right> sign=(PLUS | MINUS) power
    | <assoc=right> sign=(PLUS | MINUS) signed_atom
    | <assoc=right> sign=(PLUS | MINUS) LPAREN addition RPAREN
;

atom_without_percentage
    : constant
    | functional
    | variable_with_scalar_product
    | variable_with_power
    | LPAREN addition RPAREN
    | <assoc=right> atom factorial
;

atom
    : number
    | functional
    | variable
    | variable_with_scalar_product
    | LPAREN addition RPAREN
    | <assoc=right> atom factorial
    | <assoc=right> atom percentage
;

variable_with_scalar_product
    :   number variable
    ;

variable_with_power
    :   variable sign=POWER (power | signed_atom)
    ;

factorial
    : FACTORIAL
;

percentage
    : PERCENT
;

functional
    : function_name atom
    | function_name signed_atom
;

function_name
    : SQRT
    | SIN
    | COS
    | TAN
    | COSEC
    | SEC
    | COT
    | ARCSIN
    | ARCCOS
    | ARCTAN
    | ARCCSC
    | ARCSEC
    | ARCCOT
    | SINH
    | COSH
    | TANH
    | CSCH
    | SECH
    | COTH
    | ASINH
    | ACOSH
    | ATANH
    | ACSCH
    | ASECH
    | ACOTH
    | LOG
    | LOG10
    | EXP
;

number: NUMBER;
variable: VARIABLE;

constant: PI | EULER;

/**
LEXER RULES
**/

LPAREN: '(';
RPAREN: ')';

VARIABLE: 'x';

PLUS: '+';
MINUS: '-';
ASTER: '*' | '×';
SLASH: '/' | '÷';
POWER: '^';
PERCENT: '%';
FACTORIAL: '!';
EULER: 'e';
PI : 'π' | P I;

MODULUS: M O D;

//////// trigonometric functions
SIN : S I N;
COSEC : (C O S E C | C S C);
COS : C O S;
SEC : S E C;
TAN : T A N;
COT : C O T;
//////// inverse trigonometric functions
ARCSIN : ARC S I N;
ARCCSC : ARC (C O S E C | C S C);
ARCCOS : ARC C O S;
ARCSEC : ARC S E C;
ARCTAN : ARC T A N;
ARCCOT : ARC C O T;

//////// hyperbolic functions
SINH : S I N H;
COSH : C O S H;
TANH : T A N H;
CSCH : (C O S E C | C S C) H;
SECH : S E C H;
COTH : C O T H;

//////// inverse hyperbolic functions
ASINH : ARC S I N H;
ACOSH : ARC C O S H;
ATANH : ARC T A N H;
ACSCH : ARC (C O S E C | C S C) H;
ASECH : ARC S E C H;
ACOTH : ARC C O T H;

//////// logarithmic functions
LOG10 : L O G
;
LOG : L N;
EXP: E X P;
SQRT: '√'   | S Q R T;
NUMBER
    : [0-9]+ ('.' [0-9]+)? ('E' ('+' | '-')? [0-9]+)?;


fragment ARC: A (R C)?;
fragment A : [aA];
fragment B : [bB];
fragment C : [cC];
fragment D : [dD];
fragment E : [eE];
fragment F : [fF];
fragment G : [gG];
fragment H : [hH];
fragment I : [iI];
fragment J : [jJ];
fragment K : [kK];
fragment L : [lL];
fragment M : [mM];
fragment N : [nN];
fragment O : [oO];
fragment P : [pP];
fragment Q : [qQ];
fragment R : [rR];
fragment S : [sS];
fragment T : [tT];
fragment U : [uU];
fragment V : [vV];
fragment W : [wW];
fragment X : [xX];
fragment Y : [yY];
fragment Z : [zZ];

WHITESPACE: [ \t\n\r] -> skip;