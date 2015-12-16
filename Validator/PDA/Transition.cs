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
        public char PopCharacter { get; set; }

        public char PushCharacter { get; set; }

        public int StateIdOut { get; }

        public bool IsEpsilonMove { get { return Input == EpsilonChar;  } }

        public Transition(char input, char popChar, char pushChar = Transition.EpsilonChar, int stateIdOut = 0)
        {
            Input = input;
            PopCharacter = popChar;
            StateIdOut = stateIdOut;
            PushCharacter = pushChar;
        }

        public Transition(char[] inputs, char popChar, char pushChar = Transition.EpsilonChar,  int stateIdOut = 0)
        {
            Inputs = inputs;
            PopCharacter = popChar;
            StateIdOut = stateIdOut;
            PushCharacter = pushChar;
        }

        public int? IsIn(char inputChar, char currentTopOfStackChar)
        {
            if (PopCharacter == EpsilonChar || currentTopOfStackChar == PopCharacter)
            {
                if ((Input.HasValue && (Input.Value == inputChar ||
                     Input.Value == Transition.EpsilonChar)) || inputChar == Transition.EpsilonChar)
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
            }
            return null;
        }
    }
}
