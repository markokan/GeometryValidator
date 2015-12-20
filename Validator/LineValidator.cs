using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorUtil
{
    public class LineValidator : BaseValidator
    {
        public LineValidator() :base()
        { }

        public override bool Validate(string input, ListType typeOfList = ListType.PosList)
        {
            bool retVal = IsValid(input, typeOfList);

            return retVal;
        }
    }
}
