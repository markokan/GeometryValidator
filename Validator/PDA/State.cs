using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorUtil.PDA
{
    public class State
    {
        public int Id { get; private set; }
        public bool IsStart { get; private set; }
        public bool IsAccept { get; private set; }
       
        public IEnumerable<Transition> Transitions { get; set; }

        public State(int id, bool isStart = false, bool isAccept = false)
        {
            Id = id;
            IsStart = isStart;
            IsAccept = isAccept;
        }
    }
}
