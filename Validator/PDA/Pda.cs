﻿using System;
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
    /// This Acceptes only if both stack is empty and state is accepted!
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

        public Pda()
        { }

        /// <summary>
        /// Check PDA has enough information to run.
        /// </summary>
        /// <returns></returns>
        private StringBuilder Validate()
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

            StringBuilder errors = Validate();
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

                try
                {
                    retVal = IsAccepted(runningState, Input);
                }
                catch (InvalidPdaException f)
                {
                    Debug.WriteLine(f.ToString());
                    retVal = false;
                }
            }

            return retVal;
        }

        private bool IsAccepted(State currentState, char[] input, List<char> stack = null, int position = 0)
        {
            if (stack == null) stack = new List<char>();

            char currentTopOfStack;

            while (input.Length > position)
            {
                currentTopOfStack = stack.Count == 0 ? Transition.EpsilonChar : stack[stack.Count - 1];

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
                            return currentState.IsAccept && stack.Count == 0;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                if (possibleTransitions.Count > 1)
                {
                    foreach (var transition in possibleTransitions)
                    {
                        bool isAccept = false;
                        var state = States.FirstOrDefault(c => c.Id == transition.StateIdOut);
                        if (state != null)
                        {
                            List<char> copyOfList = new List<char>(stack);
                            isAccept = IsAccepted(state, input, copyOfList, position);

                            if (isAccept)
                            {
                                return true;
                            }
                        }
                    }
                   
                }
                else
                {
                    var currentTransition = possibleTransitions.FirstOrDefault();
                    // Pop&Push
                    SeekStack(currentTransition, ref stack);

                    Debug.WriteLine("InputChar={0}, State={1}, Stack={2}", input[position],
                        currentState != null ? currentState.Id : -1, stack != null ? stack.GetStack() : "");

                    position++;

                    // Change state if needed
                    if (currentTransition.StateIdOut != currentState.Id)
                        currentState = States.FirstOrDefault(c => c.Id == currentTransition.StateIdOut);
                }
            }

            // Not accepted state (check is there epsilon movement)
            if (!currentState.IsAccept && currentState.EpsilonTransition != null)
            {
                if (SeekStack(currentState.EpsilonTransition, ref stack))
                {
                    currentState = States.FirstOrDefault(c => c.Id == currentState.EpsilonTransition.StateIdOut);
                }
            }

            return currentState.IsAccept &&  stack.Count == 0;
        }

        private bool SeekStack(Transition currentTransition, ref List<char> stack)
        {
            if (currentTransition.PopCharacter != Transition.EpsilonChar && currentTransition.PushCharacter == Transition.EpsilonChar)
            {
                // Pop
                if (stack.Count > 0 && stack[stack.Count - 1] == currentTransition.PopCharacter)
                {
                    Debug.WriteLine("[POP] stack {0} -> {1}", stack.GetStack(), currentTransition.PopCharacter);
                    stack.RemoveAt(stack.Count - 1);

                }
                else
                {
                    // Fail fast
                    throw new InvalidPdaException("Stack doesn't contain value. Something totally messed up!");
                }
            }

            if (currentTransition.PushCharacter.HasValue && currentTransition.PushCharacter.Value != Transition.EpsilonChar)
            {
                // Push
                Debug.WriteLine("[PUSH] stack {0} -> {1}", stack.GetStack(), currentTransition.PushCharacter.Value);
                stack.Add(currentTransition.PushCharacter.Value);
            }

            return true;
        }
    }
}
