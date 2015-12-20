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

            // Start same number and ends same number also eq amount 0 and 1. (DPDA)
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
        public void Bracket_Failed()
        {
            //Arrange
            //Act
            var retval = _bracketPda.IsAcceptable("(()");

            // Assert
            Assert.IsFalse(retval);
        }

        [TestMethod]
        public void NonDetermiscinPda_Success()
        {
            // Just testing normal non-determic pda is possible -> If nfa type of pda is possible
            // Nfa is possible

            // Arrange
            var nfaPda = new Pda();
            nfaPda.States = new List<State>
            {
                new State(1, true)
                {
                    Transitions = new List<Transition>
                    {
                        new Transition('0', Transition.EpsilonChar, Transition.EpsilonChar, 2),
                        new Transition('1', Transition.EpsilonChar, Transition.EpsilonChar, 3),
                        new Transition(new char[] {'0','1' }, Transition.EpsilonChar, Transition.EpsilonChar, 1)
                    }
                },
                new State(2)
                {
                    Transitions = new List<Transition>
                    {
                      new Transition('0', Transition.EpsilonChar, Transition.EpsilonChar, 4)
                    }
                },
                new State(3)
                {
                    Transitions = new List<Transition>
                    {
                      new Transition('1', Transition.EpsilonChar, Transition.EpsilonChar, 5)
                    }
                },
                new State(4)
                {
                    Transitions = new List<Transition>
                    {
                      new Transition('0', Transition.EpsilonChar, Transition.EpsilonChar, 6)
                    }
                },
                new State(5)
                {
                    Transitions = new List<Transition>
                    {
                      new Transition('1', Transition.EpsilonChar, Transition.EpsilonChar, 6)
                    }
                },
                new State(6, false, true)
                {
                    Transitions = new List<Transition>
                    {
                       new Transition('1', Transition.EpsilonChar, Transition.EpsilonChar, 6),
                       new Transition('0', Transition.EpsilonChar, Transition.EpsilonChar, 6)
                    }
                }

            };

            // Act
            var retval = nfaPda.IsAcceptable("0110001");

            // Assert
            Assert.IsTrue(retval);
        }
    }
}
