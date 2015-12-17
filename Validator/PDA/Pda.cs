using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ValidatorUtil.PDA
{
    /// <summary>
    /// Implementation of push down automata state machine.
    /// Can be used to do different kinds push down automatas.
    /// Use States to generate states with transitions. 
    /// This state machine can handle epsilon transitions.
    /// </summary>
    public class Pda
    {
        /// <summary>
        /// Current input string
        /// </summary>
        public char[] Input { get; private set; }
    
        /// <summary>
        /// Machine states
        /// </summary>
        public IEnumerable<State> States { get; set; }

        /// <summary>
        /// Check PDA has enough information to run.
        /// </summary>
        /// <returns></returns>
        private StringBuilder CheckPda()
        {
            StringBuilder errors = new StringBuilder();

            if (States == null)
            {
                errors.Append("Missing states!\n");
            }
            else
            {
                var hasStart = States.FirstOrDefault(c => c.IsStart);
                if (hasStart == null)
                {
                    errors.Append("Missing start State!\n");
                }
            }

            return errors;
        }

        /// <summary>
        /// Is Machine state after input which?
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Accept = true, Reject = false</returns>
        public bool IsAcceptable(string input)
        {
            bool retVal = false;

            StringBuilder errors = CheckPda();
            if (!string.IsNullOrEmpty(errors.ToString()))
            {
                throw new InvalidPdaException(errors.ToString());
            }

            var runningState = States.FirstOrDefault(c => c.IsStart);

            if (runningState != null)
            {
                var firstEpsilonMove = runningState.Transitions.FirstOrDefault(c => c.IsEpsilonMove);

                if (firstEpsilonMove != null)
                {
                    input = Transition.EpsilonChar + input;
                }

                Input = input.ToCharArray();

                retVal = IsAccepted(runningState, Input);
            }

            return retVal;
        }

        private bool IsAccepted(State currentState, char[] input)
        {
            int position = 0;
            StringBuilder stack = new StringBuilder();
            char currentTopOfStack;

            while (input.Length > position)
            {
                currentTopOfStack = stack.Length == 0 ? Transition.EpsilonChar : stack[stack.Length - 1];

                var possibleTransitions = currentState.GetPossibleTransitions(input[position], currentTopOfStack);
                if (possibleTransitions == null)
                {
                    if (currentState.EpsilonTransition != null)
                    {
                        if (currentState.EpsilonTransition.IsIn(Transition.EpsilonChar, currentTopOfStack).HasValue)
                        {
                            if (SeekStack(currentState.EpsilonTransition, ref stack))
                            {
                                currentState = States.FirstOrDefault(c => c.Id == currentState.EpsilonTransition.StateIdOut);
                                continue;
                            }
                        }
                        else
                        {
                            return currentState.IsAccept;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                var currentTransition = possibleTransitions.FirstOrDefault();
                // käytä tilaa
                SeekStack(currentTransition, ref stack);

                Debug.WriteLine("InputChar={0}, State={1}, Stack={2}", input[position],
            currentState != null ? currentState.Id : -1, stack != null ? stack.ToString() : "");

                position++;

                // vaihda tilaa jos pitää
                if (currentTransition.StateIdOut != currentState.Id)
                    currentState = States.FirstOrDefault(c => c.Id == currentTransition.StateIdOut);
            }

            // Jos ei olla hyväksyvässä tilaas tarkista vielä epsilon siirtymä
            if (!currentState.IsAccept && currentState.EpsilonTransition != null)
            {
                if (SeekStack(currentState.EpsilonTransition, ref stack))
                {
                    currentState = States.FirstOrDefault(c => c.Id == currentState.EpsilonTransition.StateIdOut);
                }
            }

            return currentState.IsAccept;
        }

        private bool SeekStack(Transition currentTransition, ref StringBuilder stack)
        {
            if (currentTransition.PopCharacter != Transition.EpsilonChar)
            {
                // Pop
                if (stack.Length > 0 && stack[stack.Length - 1] == currentTransition.PopCharacter)
                {
                    stack.Remove(stack.Length - 1, 1);
                }
                else
                {
                    // Fail fast
                    throw new ApplicationException("Stack doesn't contain value. Something totally messed up!");
                }
            }

            if (currentTransition.PushCharacter != Transition.EpsilonChar)
            {
                // Push
                stack.Append(currentTransition.PushCharacter);
            }

            return true;
        }
    }
}
