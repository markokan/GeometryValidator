using System.Collections.Generic;
using System.Linq;

namespace ValidatorUtil.PDA
{
    /// <summary>
    /// Push down automata State.
    /// </summary>
    public class State
    {
        /// <summary>
        /// Identity of state
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Is Start State
        /// </summary>
        public bool IsStart { get; private set; }

        /// <summary>
        /// Is Accept state
        /// </summary>
        public bool IsAccept { get; private set; }
       
        /// <summary>
        /// Current State Transitions
        /// </summary>
        public IEnumerable<Transition> Transitions { get; set; }

        /// <summary>
        /// Get EpsilonTransition if current state contains it.
        /// </summary>
        public Transition EpsilonTransition
        {
            get
            {
                if (Transitions != null)
                    return Transitions.FirstOrDefault(c => c.IsEpsilonMove);

                return null;
            }
        }

        /// <summary>
        /// Initializes new instace of State.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isStart"></param>
        /// <param name="isAccept"></param>
        public State(int id, bool isStart = false, bool isAccept = false)
        {
            Id = id;
            IsStart = isStart;
            IsAccept = isAccept;
        }

        /// <summary>
        /// Get possible Transitions
        /// </summary>
        /// <param name="input"></param>
        /// <param name="stack"></param>
        /// <returns></returns>
        public List<Transition> GetPossibleTransitions(char input, char stack)
        {
            List<Transition> transitions = null;

            if (Transitions != null)
            {
                foreach (var transition in Transitions)
                {
                    if (transition.IsIn(input, stack).HasValue)
                    {
                        if (transitions == null) transitions = new List<Transition>();
                        transitions.Add(transition);
                    }
                }
            }

            return transitions;
        }
    }
}
