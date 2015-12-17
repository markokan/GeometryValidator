using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidatorUtil.PDA;
using System.Collections.Generic;

namespace ValidatorTests
{
    [TestClass]
    public class PdaTest
    {
        [TestMethod]
        public void TrySimpleZeroOne_Accept() //0^n1^n
        {
            // Arrange
            var zeroOnePda = new Pda();
            zeroOnePda.States = new List<State>
            {
                new State(0, true)
                {
                   Transitions = new List<Transition>
                   {
                       new Transition(Transition.EpsilonChar, Transition.EpsilonChar, '$', 1)
                   }
                },
                new State(1)
                {
                    Transitions = new List<Transition>
                    {
                        new Transition(Transition.EpsilonChar, Transition.EpsilonChar, Transition.EpsilonChar, 2),
                        new Transition('0', Transition.EpsilonChar, '0', 1)
                    }
                },
                new State(2)
                {
                    Transitions = new List<Transition>
                    {
                        new Transition('1', '0', Transition.EpsilonChar, 2),
                        new Transition(Transition.EpsilonChar, '$', Transition.EpsilonChar, 3)
                    }
                },
                new State(3,false, true)
            };

            // Act
            var retval = zeroOnePda.IsAcceptable("000111");

            // Assert
            Assert.IsTrue(retval);
        }

        [TestMethod]
        public void TrySimpleZeroOne_Reject() //0^n1^n
        {
            // Arrange
            var zeroOnePda = new Pda();
            zeroOnePda.States = new List<State>
            {
                new State(0, true)
                {
                   Transitions = new List<Transition>
                   {
                       new Transition(Transition.EpsilonChar, Transition.EpsilonChar, '$', 1)
                   }
                },
                new State(1)
                {
                    Transitions = new List<Transition>
                    {
                        new Transition(Transition.EpsilonChar, Transition.EpsilonChar, Transition.EpsilonChar, 2),
                        new Transition('0', Transition.EpsilonChar, '0', 1)
                    }
                },
                new State(2)
                {
                    Transitions = new List<Transition>
                    {
                        new Transition('1', '0', Transition.EpsilonChar, 2),
                        new Transition(Transition.EpsilonChar, '$', Transition.EpsilonChar, 3)
                    }
                },
                new State(3,false, true)
            };

            // Act
            var retval = zeroOnePda.IsAcceptable("00111");

            // Assert
            Assert.IsFalse(retval);
        }
    }
}
