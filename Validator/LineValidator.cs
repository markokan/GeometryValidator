using System;
using System.Diagnostics;

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

        public LineValidator(ListType typeOfList = ListType.PosList) :base(typeOfList)
        {
            _pointValidator = new PointValidator(typeOfList);
        }

        /// <summary>
        /// Validates current line
        /// </summary>
        /// <param name="input"></param>
        /// <param name="typeOfList"></param>
        /// <returns></returns>
        public override bool Validate(string input)
        {
            bool retVal = false;
            string[] splitted = input.Replace("\n", " ")
                                                .Replace("\t", " ")
                                                .RemoveExtraWhiteSpace()
                                                .Split(' ');


            try
            {
                retVal = ValidateLinePoints(splitted);
            } catch (IndexOutOfRangeException err)
            {
                Debug.WriteLine("Invalid input {0}", err.Message);
                retVal = false;
            }

            return retVal;
        }

        private bool ValidateLinePoints(string[] splitted)
        {
            if (splitted == null) return false;
            bool retVal = false;

            switch (ListType)
            {
                case ListType.Coordinate:
                    for (int i = 0; i < splitted.Length; i++)
                    {
                        retVal = _pointValidator.Validate(string.Format("{0}", splitted[i]));
                        if (!retVal) break;
                    }
                    break;
                default:
                    for (int i = 0; i < splitted.Length; i += 2)
                    {
                        retVal = _pointValidator.Validate(string.Format("{0} {1}", splitted[i], splitted[i + 1]));
                        if (!retVal) break;
                    }
                    break;
            } 

            return retVal;
        }
    }
}
