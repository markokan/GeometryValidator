using System.Collections.Generic;
using ValidatorUtil.PDA;

namespace ValidatorUtil
{
    public abstract class BaseValidator
    {
        private Pda _checkGeometryTuples;
        public ListType ListType
        {
            get; private set;
        }

        public BaseValidator(ListType typeOfList = ListType.PosList)
        {
            ListType = typeOfList;
            char compareChar = typeOfList == ListType.PosList ? ' ' : ',';

            _checkGeometryTuples = new Pda();
            _checkGeometryTuples.States = new List<State>
            {
               new State(0, true)
               {
                   Transitions = new List<Transition>
                   {
                       new Transition(new []{ '0', '1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, Transition.EpsilonChar, 0),
                       new Transition('.', Transition.EpsilonChar, 'A', 1)
                   }
               },
               new State(1)
               {
                   Transitions = new List<Transition>
                   {
                     new Transition(new []{ '0', '1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, Transition.EpsilonChar, 1),
                     new Transition(compareChar, Transition.EpsilonChar, 'B', 2),
                     new Transition('\t', Transition.EpsilonChar, 'B', 2),
                     new Transition('\n', Transition.EpsilonChar, 'B', 2),
                   }
               },
               new State(2)
               {
                    Transitions = new List<Transition>
                    {
                        new Transition('\t', Transition.EpsilonChar, Transition.EpsilonChar, 2),
                        new Transition('\n', Transition.EpsilonChar, Transition.EpsilonChar, 2),
                        new Transition(new []{ '0', '1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, Transition.EpsilonChar, 2),
                        new Transition('.', 'B', Transition.EpsilonChar, 3),
                    }
               },
               new State(3)
               {
                    Transitions = new List<Transition>
                    {
                        new Transition(new []{ '0', '1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, Transition.EpsilonChar, 3),
                        new Transition(compareChar, 'A', Transition.EpsilonChar, 4),
                        new Transition(Transition.EpsilonChar, 'A', Transition.EpsilonChar, 4)
                    }
               },
               new State(4, false, true)
               {
                    Transitions = new List<Transition>
                    {
                        new Transition(' ', Transition.EpsilonChar, Transition.EpsilonChar, 4),
                        new Transition('\t', Transition.EpsilonChar, Transition.EpsilonChar, 4),
                        new Transition('\n', Transition.EpsilonChar, Transition.EpsilonChar, 4),
                        new Transition(new []{'0','1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, Transition.EpsilonChar, 0)
                    }
               }
            };
        }

        protected bool IsValid(string input)
        {
            return _checkGeometryTuples.IsAcceptable(input);
        }

        public abstract bool Validate(string input);
    }
}
