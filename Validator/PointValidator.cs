using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidatorUtil.PDA;

namespace ValidatorUtil
{
    /// <summary>
    /// Point validator. Accepted formats are:
    /// number.number,number.number
    /// number,number
    /// number.number number.number
    /// number number
    /// </summary>
    public class PointValidator : BaseValidator
    {

        public PointValidator() : base()
        { }

        /// <summary>
        /// Validates point.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="typeOfList"></param>
        /// <returns></returns>
        public override bool Validate(string input, ListType typeOfList = ListType.PosList)
        {
            bool retVal = false;

            char symbol = typeOfList == ListType.PosList ? ' ' : ',';

            var pointPda = new Pda();
            pointPda.States = new List<State>
            {
                new State(1, true)
                {
                    Transitions = new List<Transition>
                    {
                        new Transition(new []{'\t', '\n', '0', '1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, Transition.EpsilonChar, 1),
                        new Transition('.', Transition.EpsilonChar, 'A', 3),
                        new Transition(symbol, Transition.EpsilonChar, Transition.EpsilonChar , 2)
                    }
                },
                new State(2, false, true)
                {
                    Transitions = new List<Transition>
                    {
                        new Transition(new []{ '\t', '\n', '0', '1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, Transition.EpsilonChar, 2),
                        new Transition('.', Transition.EpsilonChar, 'B', 4),
                        new Transition(symbol, Transition.EpsilonChar, Transition.EpsilonChar, 5)
                    }
                },
                new State(3)
                {
                    Transitions = new List<Transition>
                    {
                        new Transition(new []{'\t', '\n', '0','1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, Transition.EpsilonChar, 3),
                        new Transition(symbol, 'A', Transition.EpsilonChar , 2)
                    }
                },
                new State(4)
                {
                    Transitions = new List<Transition>
                    {
                        new Transition(new []{'\t', '\n', '0', '1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, Transition.EpsilonChar, 4),
                        new Transition(Transition.EpsilonChar, 'B', Transition.EpsilonChar , 2)
                    }
                },
                new State(5)
            };

            retVal = pointPda.IsAcceptable(input);

            return retVal;
        }
    }
}