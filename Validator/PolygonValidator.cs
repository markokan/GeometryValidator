using System.Collections.Generic;
using ValidatorUtil.PDA;

namespace ValidatorUtil
{
    /// <summary>
    /// Validates Polygon strings. Supported Coordinate and PosList.
    /// Check is string correct format and polygon is closed.
    /// </summary>
    public class PolygonValidator : BaseValidator
    {
        public PolygonValidator() : base()
        { }

        public override bool Validate(string input, ListType typeOfList = ListType.PosList)
        {
            bool retval = IsValid(input, typeOfList);

            // Tarkasta onko sama vika kuin eka (PDA)
            if (retval)
            {

            }

            return retval;
        }
    }
}
