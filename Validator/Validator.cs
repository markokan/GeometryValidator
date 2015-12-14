using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidatorUtil.PDA;

namespace ValidatorUtil
{
    public static class PolygonStringValidator
    {
        public static bool IsValid(string input, ListType typeOfList = ListType.PosList)
        {
            bool retval = false;

            char compareChar = typeOfList == ListType.PosList ? ' ' : ',';

            // Tarkasta onko oikea määrä pareja ja sisältö oikein (PDA)
            var pdaCheckPolygon = new Pda();
            pdaCheckPolygon.States = new List<State>
            {
               new State(0, true)
               {
                   Transitions = new List<Transition>
                   {
                       new Transition(new []{ '0', '1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, 0),
                       new Transition('.', 'A', 1)
                   }
               },
               new State(1)
               {
                   Transitions = new List<Transition>
                   {
                     new Transition(new []{ '0', '1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, 1),
                     new Transition(compareChar, 'B', 2)
                   }
               },
               new State(2)
               {
                    Transitions = new List<Transition>
                    {
                        new Transition(' ', Transition.EpsilonChar, 2),
                        new Transition('\t', Transition.EpsilonChar, 2),
                        new Transition('\n', Transition.EpsilonChar, 2),
                        new Transition(new []{ '0', '1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, 2),
                        new Transition('.', Transition.PopChar, 3),
                    }
               },
               new State(3)
               {
                    Transitions = new List<Transition>
                    {
                        new Transition(new []{ '0', '1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, 3),
                        new Transition(compareChar, Transition.PopChar, 4)
                    }
               },
               new State(4, false, true)
               {
                    Transitions = new List<Transition>
                    {
                        new Transition(' ', Transition.EpsilonChar, 4),
                        new Transition('\t', Transition.EpsilonChar, 4),
                        new Transition('\n', Transition.EpsilonChar, 4),
                        new Transition(new []{'0','1','2','3','4','5','6','7','8','9'}, Transition.EpsilonChar, 0)
                    }
               }
            };

            retval = pdaCheckPolygon.Run(input);

            // Tarkasta onko sama vika kuin eka (PDA)
            if (retval)
            {

            }

            return retval;
        }
    }
}
