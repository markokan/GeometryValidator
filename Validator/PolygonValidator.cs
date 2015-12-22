using System;
using System.Collections.Generic;
using System.Text;
using ValidatorUtil.PDA;

namespace ValidatorUtil
{
    /// <summary>
    /// Validates Polygon strings. Supported Coordinate and PosList.
    /// Check is string correct format and polygon is closed.
    /// </summary>
    public class PolygonValidator : BaseValidator
    {
        private Pda _stringReversePda;
        private char _symbol;

        public PolygonValidator(ListType typeOfList = ListType.PosList) : base(typeOfList)
        {
            _symbol = typeOfList == ListType.Coordinate ? ',' : ' ';

            _stringReversePda = new Pda();
            _stringReversePda.States = new List<State>
            {
                new State(1, true)
                {
                    Transitions = new List<Transition>
                    {
                        new Transition('0', Transition.EpsilonChar, '0', 1),
                        new Transition('1', Transition.EpsilonChar, '1', 1),
                        new Transition('2', Transition.EpsilonChar, '2', 1),
                        new Transition('3', Transition.EpsilonChar, '3', 1),
                        new Transition('4', Transition.EpsilonChar, '4', 1),
                        new Transition('5', Transition.EpsilonChar, '5', 1),
                        new Transition('6', Transition.EpsilonChar, '6', 1),
                        new Transition('7', Transition.EpsilonChar, '7', 1),
                        new Transition('8', Transition.EpsilonChar, '8', 1),
                        new Transition('9', Transition.EpsilonChar, '9', 1),
                        new Transition('.', Transition.EpsilonChar, '.', 1),
                        new Transition(_symbol, Transition.EpsilonChar, _symbol, 1),
                        new Transition('|', Transition.EpsilonChar, Transition.EpsilonChar, 2)
                    }
                },
                new State(2, false, true)
                {
                    Transitions = new List<Transition>
                    {
                        new Transition('0', '0', Transition.EpsilonChar, 2),
                        new Transition('1', '1', Transition.EpsilonChar, 2),
                        new Transition('2', '2', Transition.EpsilonChar, 2),
                        new Transition('3', '3', Transition.EpsilonChar, 2),
                        new Transition('4', '4', Transition.EpsilonChar, 2),
                        new Transition('5', '5', Transition.EpsilonChar, 2),
                        new Transition('6', '6', Transition.EpsilonChar, 2),
                        new Transition('7', '7', Transition.EpsilonChar, 2),
                        new Transition('8', '8', Transition.EpsilonChar, 2),
                        new Transition('.', '.', Transition.EpsilonChar, 2),
                        new Transition('9', '9', Transition.EpsilonChar, 2),
                        new Transition(_symbol, _symbol, Transition.EpsilonChar, 2)
                    }
                }
            };
        }

        public override bool Validate(string input)
        {
            // Check basic input is valid and there is paired amount 
            bool retval = IsValid(input);

            if (retval)
            {
                // Check is polygon closed
                string[] splitted = input.Replace("\n", " ")
                                         .Replace("\t", " ")
                                         .Replace(",", " ")
                                         .RemoveExtraWhiteSpace()
                                         .Split(' ');

                string lastPart = string.Format("{0}{1}{2}", splitted[splitted.Length - 2], _symbol, splitted[splitted.Length - 1]);
                string inputCreated = string.Format("{0}{1}{2}{3}{4}", splitted[0], _symbol, splitted[1], '|', lastPart.Reverse());

                retval = _stringReversePda.IsAcceptable(inputCreated);
            }

            return retval;
        }
    }
}
