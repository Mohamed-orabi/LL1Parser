namespace LL1Parser
{
    internal class Program
    {
        private static Stack<string> stack = new Stack<string>();
        private static string input;
        private static int index = 0;
        static void Main(string[] args)
        {
            // Input string with $ at the end (end of input marker)
            input = "2+3$";

            // Initialize the stack with the start symbol and end-of-input marker
            stack.Push("$");
            stack.Push("E");

            // Start parsing
            if (Parse())
            {
                Console.WriteLine("Input successfully parsed!");
            }
            else
            {
                Console.WriteLine("Input parsing failed.");
            }

        }
        // LL(1) parsing function
        static bool Parse()
        {
            while (stack.Count > 0)
            {
                string top = stack.Peek();  // Get the top of the stack
                char lookahead = input[index];  // Get the lookahead token

                // If top is a terminal
                if (IsTerminal(top))
                {
                    // If the top matches the lookahead, pop the stack and move input forward
                    if (top == lookahead.ToString())
                    {
                        stack.Pop();
                        index++;  // Move to the next character in input
                    }
                    else
                    {
                        return false;  // Mismatch between top of stack and input
                    }
                }
                else  // If top is a non-terminal, use the parsing table
                {
                    string rule = GetRule(top, lookahead);  // Get the production rule to apply
                    if (rule == null)
                    {
                        return false;  // No rule found, parsing fails
                    }

                    stack.Pop();  // Pop the non-terminal

                    // Push the right-hand side of the rule onto the stack (in reverse order)
                    if (rule != "ε")  // Don't push anything for epsilon (ε)
                    {
                        for (int i = rule.Length - 1; i >= 0; i--)
                        {
                            stack.Push(rule[i].ToString());
                        }
                    }
                }
            }

            // Parsing succeeds if we have reached the end of the input
            return index == input.Length;
        }

        // Function to check if a symbol is terminal (num and + are terminals, $ is end-of-input)
        static bool IsTerminal(string symbol)
        {
            return symbol == "num" || symbol == "+" || symbol == "$" || char.IsDigit(symbol[0]);
        }

        // Function to retrieve the rule from the LL(1) parsing table
        static string GetRule(string nonTerminal, char lookahead)
        {
            if (nonTerminal == "E")
            {
                if (char.IsDigit(lookahead)) return "T E'";
            }
            else if (nonTerminal == "E'")
            {
                if (lookahead == '+') return "+ T E'";
                if (lookahead == '$') return "ε";  // Empty production
            }
            else if (nonTerminal == "T")
            {
                if (char.IsDigit(lookahead)) return "num";
            }

            // If no rule is found, return null
            return null;
        }
    }
}
