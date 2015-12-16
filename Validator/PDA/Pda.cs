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
        public char[] Input { get; private set; }
    
        public IEnumerable<State> States { get; set; }

        public bool Run(string input)
        {
            bool retVal = false;
            

            input = input + " ";

            Input = input.ToCharArray();

            var runningState = States.FirstOrDefault(c => c.IsStart);

            if (runningState != null)
            {
                retVal = LoopAll(runningState, new char[] { Transition.EpsilonChar, '0', '0', '0', '1', '1', '1' });
            }

            return retVal;
        }

        private bool LoopAll(State currentState, char[] input, StringBuilder stack = null, int position = 0)
        {
            if (stack == null) stack = new StringBuilder();
            bool retVal = false;

            char? inputChar = null;
            char currentTopOfStack;

            int transationPosition = 0;
            Transition currentTransition = currentState.Transitions.ToList()[transationPosition];
            int countOfTransitions = currentState.Transitions.Count();

            while (input.Length >= position)
            {
                int? nextStateId = null;

                inputChar = input[position];
                position++;

                currentTopOfStack = stack.Length == 0 ? Transition.EpsilonChar : stack[stack.Length - 1];

                // Anna seuraava tila tai pysy tässä ja ota seuraava kirjain
                nextStateId = currentTransition.IsIn(inputChar.Value, currentTopOfStack);

                if (nextStateId == null)
                {
                    var epsilonMovement = currentState.Transitions.FirstOrDefault(c => c.IsEpsilonMove);
                    if (epsilonMovement != null)
                    {
                        transationPosition = 0;
                        currentState = States.FirstOrDefault(c => c.Id == epsilonMovement.StateIdOut);
                        countOfTransitions = currentState.Transitions.Count();
                        currentTransition = currentState.Transitions.ToList()[transationPosition];
                        position--;
                        continue;
                    }
                }

                if (nextStateId != null)
                {
                    if (currentTransition.PopCharacter != Transition.EpsilonChar)
                    {
                        // Pop
                        if (stack.Length > 0 && stack[stack.Length -1] == currentTransition.PopCharacter)
                        {
                            stack.Remove(stack.Length - 1, 1);
                        }
                        else
                        {
                            // Fail fast
                            return false;
                        }
                    }

                    if (currentTransition.PushCharacter != Transition.EpsilonChar)
                    { 
                        // Push
                        stack.Append(currentTransition.PushCharacter);
                    }

                    Debug.WriteLine("InputChar={0}, State={1}, Stack={2}", inputChar.HasValue ? inputChar : '?'
                        , currentState != null ? currentState.Id : -1, stack != null ? stack.ToString() : "");
                }
                else
                {
                    if (countOfTransitions >= transationPosition)
                    {
                        transationPosition++;
                        currentTransition = currentState.Transitions.ToList()[transationPosition];
                    }
                }

                if (nextStateId.HasValue && currentState.Id != nextStateId && countOfTransitions == 1)
                {
                    transationPosition = 0;
                    currentState = States.FirstOrDefault(c => c.Id == nextStateId);
                    countOfTransitions = currentState.Transitions.Count();
                    currentTransition = currentState.Transitions.ToList()[transationPosition];
                }
            }

            return false;
        }

       
    }
}
