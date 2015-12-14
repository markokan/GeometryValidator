using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorUtil.PDA
{
    public class Pda
    {
        public int Position { get; private set; }
        public char[] Input { get; private set; }

        public StringBuilder Stack { get; private set; }

        public bool IsEnd
        {
            get { return Input != null && Input.Length < Position + 1; }
        }

        public IEnumerable<State> States { get; set; }

        public bool Run(string input)
        {
            bool retVal = false;
            Stack = new StringBuilder();

            input = input + " ";

            Input = input.ToCharArray();

            var runningState = States.FirstOrDefault(c => c.IsStart);

            while (runningState != null && IsEnd == false)
            {
                char inputChar = Input[Position];

                foreach (var transition in runningState.Transitions)
                {
                    var nextStateId = transition.IsIn(inputChar);
                    if (nextStateId != null)
                    {
                        runningState = States.FirstOrDefault(c => c.Id == nextStateId);

                        switch (transition.StackSymbol)
                        {
                            case Transition.PopChar:
                                if (Stack.Length > 0)
                                {
                                    Stack.Remove(Stack.Length - 1, 1);
                                }
                                else
                                {
                                    runningState = null;
                                }
                                break;
                            case Transition.EpsilonChar:
                                break;
                            default:
                                Stack.Append(transition.StackSymbol);
                                break;
                        };
                        break;
                    }
                    else
                    {
                        runningState = null;
                    }
                }

                Debug.WriteLine("InputChar={0}, State={1}, Stack={2}", inputChar, runningState != null ? runningState.Id : -1, Stack.ToString());

                Position++;
            }

            retVal = String.IsNullOrEmpty(Stack.ToString()) && (runningState != null && runningState.IsAccept);

            return retVal;
        }
    }
}
