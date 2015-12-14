﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidatorUtil;

namespace ValidatorTests
{
    [TestClass]
    public class ValidateTest
    {
        private string _goodPosListPolygon, _badPosListPolygon;

        [TestInitialize]
        public void InitTests()
        {
            _goodPosListPolygon = "552195.58089905 6932799.75180975 552195.128651859 6932799.98832659 552126.852809161 " +
                "6932716.19843894 552113.441011408 6932699.73945017 552113.715168711 6932699.28495579 552119.007865341 " +
                "6932690.51079848 552151.97584287 6932662.6063041 552158.591573207 6932657.0063041 552159.21573051 " +
                "6932657.48551758 552174.616854106 6932660.5310232 552181.707865342 6932662.86023668 552172.612359724 " +
                "6932669.60798949 552170.571910285 6932676.28664118 552167.425281072 6932686.58383219 552167.425281072 " +
                "6932696.72203443 552179.264606914 6932698.40967489 552182.700561971 6932712.68158501 552180.746067589 " +
                "6932728.31585467 552179.567415903 6932740.34001198 552177.680899049 6932745.05574231 552184.046629387 " +
                "6932754.01529288 552189.019663095 6932751.84787715 552197.394943994 6932754.7607985 552199.844943994 "+
                "6932762.60068614 552213.074719275 6932765.05124794 552218.955056354 6932779.2613603 552231.205056355 " +
                "6932781.22147266 552234.144943995 6932792.49169738 552229.244943995 6932793.47203447 552216.014606916 " +
                "6932789.06136031 552195.58089905 6932799.75180975";

            _badPosListPolygon = "552195.58089905 6932799.75180975 552195.128651859 6932799.98832659 552126.852809161 " +
              "6932716.19843894 552113.441011408 6932699.73945017 552113.715168711 6932699.28495579 552119.007865341 " +
              "6932690.51079848 552151.97584287 6932662.6063041 552158.591573207 6932657.0063041 552159.21573051 " +
              "6932657.48551758 552174.616854106 6932660.5310232 552181.707865342 6932662.86023668 552172.612359724 " +
              "6932669.60798949 552170.571910285 6932676.28664118 552167.425281072 6932686.58383219 552167.425281072 " +
              "6932696.72203443 552179.264606914 552180.746067589 " +
              "6932728.31585467 552179.567415903 6932740.34001198 552177.680899049 6932745.05574231 552184.046629387 " +
              "6932754.01529288 552189.019663095 6932751.84787715 552197.394943994 6932754.7607985 552199.844943994 " +
              "6932762.60068614 552213.074719275 6932765.05124794 552218.955056354 6932779.2613603 552231.205056355 " +
              "6932781.22147266 552234.144943995 6932792.49169738 552229.244943995 6932793.47203447 552216.014606916 " +
              "6932789.06136031 552195.58089905 6932799.75180975";
        }

        [TestMethod]
        public void Validate_Polygon_Success()
        {
            // Arrange
            // Act
            bool retVal = PolygonStringValidator.IsValid(_goodPosListPolygon);

            // Assert
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void Validate_Polygon_Failed()
        {
            // Arrange
            // Act
            bool retVal = PolygonStringValidator.IsValid(_badPosListPolygon);

            // Assert
            Assert.IsFalse(retVal);
        }



        [TestMethod]
        public void Validate_Line_Success()
        {

        }

        [TestMethod]
        public void Validate_Point_Success()
        {

        }

    }
}