using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorUtil
{
    /// <summary>
    /// Line validator. Accepted formats: (Uses pointValidator)
    /// number.number,number.number number.number,number.number
    /// number.number number.number number.number number.number
    /// </summary>
    public class LineValidator : BaseValidator
    {
        private PointValidator _pointValidator;

        public LineValidator() :base()
        {
            _pointValidator = new PointValidator();
        }

        /// <summary>
        /// Validates current line
        /// </summary>
        /// <param name="input"></param>
        /// <param name="typeOfList"></param>
        /// <returns></returns>
        public override bool Validate(string input, ListType typeOfList = ListType.PosList)
        {
            bool retVal = false;

            if (typeOfList == ListType.Coordinate)
            {
                string[] splitted = input.Replace("\n", " ")
                                           .Replace("\t", " ")
                                           .RemoveExtraWhiteSpace()
                                           .Split(' ');

                for (int i = 0; i < splitted.Length; i++)
                {
                    retVal = _pointValidator.Validate(string.Format("{0}", splitted[i]), ListType.Coordinate);
                    if (!retVal) break;
                }
            }

            return retVal;
        }
    }
}
