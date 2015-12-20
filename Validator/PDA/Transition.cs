using System;
using System.Linq;

namespace ValidatorUtil.PDA
{
    /// <summary>
    /// Push down automata Transition
    /// </summary>
    public class Transition
    {
        public const char EpsilonChar = '`';
        public const char PopChar = ' ';
        
        /// <summary>
        /// Current Transition id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Transition with many possible accepted inputs.
        /// </summary>
        public char[] Inputs { get; set; }

        /// <summary>
        /// Transition which has only one transition
        /// </summary>
        public char? Input { get; set; }

        /// <summary>
        /// Transition pop character
        /// </summary>
        public char PopCharacter { get; set; }

        /// <summary>
        /// Transition push character
        /// </summary>
        public char? PushCharacter { get; set; }


        /// <summary>
        /// Possible state change id
        /// </summary>
        public int StateIdOut { get; }

        /// <summary>
        /// Is epsilonmove.
        /// </summary>
        public bool IsEpsilonMove { get { return Input == EpsilonChar;  } }

        /// <summary>
        ///  Initializes new instances of Transition with one input
        /// </summary>
        /// <param name="input"></param>
        /// <param name="popChar"></param>
        /// <param name="pushChar"></param>
        /// <param name="stateIdOut"></param>
        public Transition(char input, char popChar, char pushChar = Transition.EpsilonChar, int stateIdOut = 0)
        {
            Id = Guid.NewGuid();
            Input = input;
            PopCharacter = popChar;
            StateIdOut = stateIdOut;
            PushCharacter = pushChar;
        }

        /// <summary>
        /// Initializes new instance of Transition with multiple inputs
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="popChar"></param>
        /// <param name="pushChar"></param>
        /// <param name="stateIdOut"></param>
        public Transition(char[] inputs, char popChar, char pushChar = Transition.EpsilonChar,  int stateIdOut = 0)
        {
            Id = Guid.NewGuid();
            Inputs = inputs;
            PopCharacter = popChar;
            StateIdOut = stateIdOut;
            PushCharacter = pushChar;
        }

        /// <summary>
        /// Check is input char this transition 
        /// </summary>
        /// <param name="inputChar"></param>
        /// <param name="currentTopOfStackChar"></param>
        /// <returns></returns>
        public int? IsIn(char inputChar, char currentTopOfStackChar)
        {
            if (PopCharacter == EpsilonChar || currentTopOfStackChar == PopCharacter)
            {
                if (Input.HasValue && Input.Value == inputChar || inputChar == Transition.EpsilonChar)
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

        /// <summary>
        /// Transition info
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (!Input.HasValue)
            {
                return string.Format("Id={0}, {1}, {2} -> {3}", Id, "<many>", PopCharacter, PushCharacter);
            }

            return string.Format("Id={0}, {1}, {2} -> {3}", Id, Input.Value, PopCharacter, PushCharacter);
        }
    }
}
