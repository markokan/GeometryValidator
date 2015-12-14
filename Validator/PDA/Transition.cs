using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorUtil.PDA
{
    public class Transition
    {
        public const char EpsilonChar = '`';
        public const char PopChar = ' ';

        public char[] Inputs { get; set; }

        public char? Input { get; set; }
        public char StackSymbol { get; set; }


        public int StateIdOut { get; }

        public Transition(char input, char stack, int stateIdOut = 0)
        {
            Input = input;
            StackSymbol = stack;
            StateIdOut = stateIdOut;
        }

        public Transition(char[] inputs, char stack, int stateIdOut = 0)
        {
            Inputs = inputs;
            StackSymbol = stack;
            StateIdOut = stateIdOut;
        }

        public int? IsIn(char inputChar)
        {
            if (Input.HasValue && Input.Value == inputChar)
            {
                return StateIdOut;
            }
            else if (Inputs != null && Inputs.Length > 0)
            {
                if (Inputs.Contains(inputChar))
                {
                    return StateIdOut;
                }
            }

            return null;
        }
    }
}
