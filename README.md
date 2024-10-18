# LL1Parser

# What is an LL(1) Parser?
An LL(1) parser is a type of top-down parser that reads input from Left to right (the first "L") and produces a Leftmost derivation (the second "L"). The "1" means that the parser uses 1 lookahead token to make decisions about which production rule to apply.

# Key Characteristics:
- Left to right parsing (reads input from left to right).
- Leftmost derivation (builds the parse tree by expanding the leftmost non-terminal first).
- Uses 1 token of lookahead to decide which production rule to apply.

# Example of LL(1) Parsing:
- E → T E' 
- E' → + T E' | ε  (ε means "nothing", which stops recursion)
- T → num

# This grammar means:

- E represents an expression.
- T represents a term (a number in this case).
- E' represents what comes after a term, which can either be + T E' (an expression followed by another term) or nothing (ε) to end the expression.

# LL(1) Parsing Table
In an LL(1) parser, a parsing table is created based on the grammar. The parser uses this table and 1 lookahead token to decide which production rule to use.

Let’s first create the LL(1) parsing table for the grammar:

# First and Follow Sets
Before we create the table, we need the FIRST and FOLLOW sets for each non-terminal:

- FIRST set: What terminals can appear first in the derivation of a non-terminal.
- FOLLOW set: What terminals can appear immediately after a non-terminal.

# First Sets:
- FIRST(E) = { num } (because E → T E' and T → num)
- FIRST(E') = { +, ε } (because E' → + T E' or E' → ε)
- FIRST(T) = { num } (because T → num)

# Follow Sets:
- FOLLOW(E) = { $ } (we use $ to represent the end of input)
- FOLLOW(E') = { $, ) } (because E' follows T in E → T E')
- FOLLOW(T) = { +, $ } (because T is followed by E' in E → T E')

# LL(1) Parsing Table:
Now, based on the FIRST and FOLLOW sets, we create the parsing table:

| Non-Terminal | num | + | $  | ε |
| :---         |     :---:      |          :---: |          :---: |          :---: |
|E   | E → T E'     |     |    |    |
| E'     |        | E' → + T E'      |E' → ε      |E' → ε      |
| T     | T → num      |       |      |      |

- E → T E' is applied when the lookahead token is num.
- E' → + T E' is applied when the lookahead token is +.
- E' → ε is applied when the lookahead token is $ (end of input) or when no more expressions follow.

# Example: Parsing the Input "2 + 3"
Let’s use the parsing table to parse the input "2 + 3" step by step.

1. Start with E (starting non-terminal):
- Lookahead token: 2
- Use rule E → T E' because FIRST(E) has num.

2.Parse T:
- Lookahead token: 2
- Use rule T → num because FIRST(T) has num.
- Match 2 and move to the next token (+).

3.Parse E':
- Lookahead token: +
- Use rule E' → + T E' because FIRST(E') has +.

4.Parse T again (for the second number):
- Lookahead token: 3
- Use rule T → num because FIRST(T) has num.
- Match 3 and move to the next token ($, end of input).

5.Parse E' again:
- Lookahead token: $ (end of input).
- Use rule E' → ε because FOLLOW(E') has $.

6.Parsing is complete:
-We have successfully parsed the entire input string 2 + 3.









