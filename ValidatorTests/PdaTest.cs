using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidatorUtil.PDA;
using System.Collections.Generic;

namespace ValidatorTests
{
    [TestClass]
    public class PdaTest
    {
        private Pda _zeroOneCountEq;
        private Pda _bracketPda;

        [TestInitialize]
        public void InitTest()
        {
            //0^n1^n (NPDA)
            _zeroOneCountEq = new Pda();
            _zeroOneCountEq.States = new List<State>
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

            _bracketPda = new Pda();
            _bracketPda.States = new List<State>
            {
                 new State(0, true)
                {
                   Transitions = new List<Transition>
                   {
                       new Transition(Transition.EpsilonChar, Transition.EpsilonChar,'$', 1)
                   }
                },
                new State(1)
                {
                   Transitions = new List<Transition>
                   {
                       new Transition('(', '$', '1', 2)
                   }
                },
                new State(2)
                {
                    Transitions = new List<Transition>
                    {
                        new Transition('(', '1','1', 2),
                        new Transition(')', '1', Transition.EpsilonChar, 3)
                    }
                },
                new State(3)
                {
                    Transitions = new List<Transition>
                    {
                        new Transition(')', '1', Transition.EpsilonChar, 3),
                        new Transition(Transition.EpsilonChar, '$', Transition.EpsilonChar ,4)
                    }
                },
                new  State(4, false, true)
            };
        }

        [TestMethod]
        public void TrySimpleZeroOne_Accept() //0^n1^n
        {
            // Arrange
            // Act
            var retval = _zeroOneCountEq.IsAcceptable("000111");

            // Assert
            Assert.IsTrue(retval);
        }

        [TestMethod]
        public void TrySimpleZeroOne_Reject() //0^n1^n
        {
            // Arrange
            // Act
            var retval = _zeroOneCountEq.IsAcceptable("00111");

            // Assert
            Assert.IsFalse(retval);
        }

        [TestMethod]
        public void Bracket_Success()
        {
            //Arrange
            //Act
            var retval = _bracketPda.IsAcceptable("(())");

            // Assert
            Assert.IsTrue(retval);
        }

        [TestMethod]
        public void Bracket_Empty_input_Reject()
        {
            //Arrange
            //Act
            var retval = _bracketPda.IsAcceptable("");

            // Assert
            Assert.IsFalse(retval);
        }


        [TestMethod]
        public void Bracket_Failed()
        {
            //Arrange
            //Act
            var retval = _bracketPda.IsAcceptable("(()");

            // Assert
            Assert.IsFalse(retval);
        }
    }
}
