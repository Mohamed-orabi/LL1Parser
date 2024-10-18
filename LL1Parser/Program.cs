namespace LL1Parser
{
    internal class Program
    {
        private static string inputStr;
        private static int index;
        private static char lookahead;
        static void Main(string[] args)
        {
            inputStr = "2+3$";  // The input string to parse, with $ marking the end of input
            index = 0;
            lookahead = inputStr[index];  // Initialize the lookahead token

            // Start parsing with the start symbol E
            if (ParseE() && lookahead == '$')
            {
                Console.WriteLine("Input successfully parsed!");
            }
            else
            {
                Console.WriteLine("Input parsing failed!");
            }

        }

        // Parse the non-terminal E → T E'
        private static bool ParseE()
        {
            if (ParseT())  // Parse T
            {
                return ParseEPrime();  // Parse E'
            }
            return false;
        }

        // Parse the non-terminal E' → + T E' | ε
        private static bool ParseEPrime()
        {
            if (lookahead == '+')  // If the lookahead is '+', apply the rule E' → + T E'
            {
                Match('+');  // Consume the '+'
                if (ParseT())  // Parse T
                {
                    return ParseEPrime();  // Recursively parse E'
                }
                return false;
            }
            // If the lookahead is not '+', apply the rule E' → ε (nothing to do here)
            return true;
        }

        // Parse the non-terminal T → num
        private static bool ParseT()
        {
            if (char.IsDigit(lookahead))  // If the lookahead is a digit (num)
            {
                Match(lookahead);  // Consume the number
                return true;
            }
            return false;
        }

        // Function to consume the current lookahead and move to the next input character
        private static void Match(char expected)
        {
            if (lookahead == expected)
            {
                index++;
                lookahead = inputStr[index];  // Move to the next character
            }
            else
            {
                throw new Exception("Syntax error: unexpected token");
            }
        }
    }
}
