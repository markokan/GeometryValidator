﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidatorUtil;

namespace ValidatorTests
{
    [TestClass]
    public class ValidateTest
    {
        private string _goodPosListPolygon, _goodPoslitPolygon2, _badPosListPolygon2, _goodCoordinate1, _badCoordinate1;
        private PolygonValidator _polygonValidator, _polygonValidatorCoordinate;

        [TestInitialize]
        public void InitTests()
        {
            _polygonValidatorCoordinate = new PolygonValidator(ListType.Coordinate);
            _polygonValidator = new PolygonValidator();

            _goodPosListPolygon = "552195.58089905 6932799.75180975 552195.128651859 6932799.98832659 552126.852809161 " +
                "6932716.19843894 552113.441011408 6932699.73945017 552113.715168711 6932699.28495579 552119.007865341 " +
                "6932690.51079848 552151.97584287 6932662.6063041 552158.591573207 6932657.0063041 552159.21573051 " +
                "6932657.48551758 552174.616854106 6932660.5310232 552181.707865342 6932662.86023668 552172.612359724 " +
                "6932669.60798949 552170.571910285 \n\n6932676.28664118 552167.425281072 6932686.58383219 552167.425281072 " +
                "6932696.72203443 552179.264606914 6932698.40967489 552182.700561971 6932712.68158501 552180.746067589 " +
                "6932728.31585467 552179.567415903 \t6932740.34001198 552177.680899049 6932745.05574231 552184.046629387 " +
                "6932754.01529288 552189.019663095 6932751.84787715 552197.394943994 6932754.7607985 552199.844943994 "+
                "6932762.60068614 552213.074719275 6932765.05124794 552218.955056354 6932779.2613603 552231.205056355 " +
                "6932781.22147266 552234.144943995 6932792.49169738 552229.244943995 6932793.47203447 552216.014606916 " +
                "6932789.06136031 552195.58089905 6932799.75180975";

            _goodPoslitPolygon2 = "552195.58089905 6932799.75180975 552195.128651859 6932799.98832659 552126.852809161 " +
                "6932716.19843894 552113.441011408 6932699.73945017 552113.715168711 6932699.28495579 552119.007865341 " +
                "6932690.51079848 552151.97584287 6932662.6063041 552158.591573207 6932657.0063041 552159.21573051 " +
                "6932657.48551758 552174.616854106 6932660.5310232 552181.707865342 6932662.86023668 552172.612359724 " +
                "6932669.60798949 552170.571910285\n\n6932676.28664118 552167.425281072 6932686.58383219 552167.425281072 " +
                "6932696.72203443 552179.264606914 6932698.40967489 552182.700561971 6932712.68158501 552180.746067589 " +
                "6932728.31585467 552179.567415903\t6932740.34001198 552177.680899049 6932745.05574231 552184.046629387 " +
                "6932754.01529288 552189.019663095 6932751.84787715 552197.394943994 6932754.7607985 552199.844943994 " +
                "6932762.60068614 552213.074719275 6932765.05124794 552218.955056354 6932779.2613603 552231.205056355 " +
                "6932781.22147266 552234.144943995 6932792.49169738 552229.244943995 6932793.47203447 552216.014606916 " +
                "6932789.06136031 552195.58089905 6932799.75180975";

            _badPosListPolygon2 = "552195.58089905 6932799.75180975 552195.128651859 6932799.98832659 552126.852809161 " +
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

            _goodCoordinate1 = "0.0,0.0 100.0,0.0 100.0,100.0 0.0,100.0 0.0,0.0";
            _badCoordinate1 = "0.0,0.0 100.0,0.0 100,0,100.0 0.0,100.0 0.0,0.0";

            

        }

        [TestMethod]
        public void Validate_PosList_Polygon_Accept()
        {
            // Arrange
            // Act
            bool retVal = _polygonValidator.Validate(_goodPosListPolygon);

            // Assert
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void Validate_Coordinate_Polygon_Accept()
        {
            // Arrange
            // Act
            bool retVal = _polygonValidatorCoordinate.Validate(_goodCoordinate1);

            // Assert
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void Validate_Coordinate_Polygon_1_Reject()
        {
            // Arrange
            // Act
            bool retVal = _polygonValidatorCoordinate.Validate(_badCoordinate1);

            // Assert
            Assert.IsFalse(retVal);
        }


        [TestMethod]
        public void Validate_PosList_Polygon_1_Reject()
        {
            // Arrange
            // Act
            bool retVal = _polygonValidator.Validate(_goodPoslitPolygon2);

            // Assert
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void Validate_Polygon_2_Reject()
        {
            // Arrange
            // Act
            bool retVal = _polygonValidator.Validate(_badPosListPolygon2);

            // Assert
            Assert.IsFalse(retVal);
        }

        [TestMethod]
        public void Validate_Polygon_Coordinate3_Success()
        {
            // Arrange /act
            bool retVal = _polygonValidatorCoordinate.Validate("0.0,0.0 100.0,0.0 100.0,100.0 0.0,100.0 0.0,0.0");

            // Assert
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void Validate_Line_Success()
        {
            // Arrange
            var lineValidator = new LineValidator(ListType.Coordinate);

            // Act
            bool retVal = lineValidator.Validate("100,200 150,300");

            // Assert
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void Validate_Line_PosList_Success()
        {
            // Arrange
            var lineValidator = new LineValidator();

            // Act
            bool retVal = lineValidator.Validate("45.67 88.56 55.56 89.44");

            // Assert
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void Validate_Line_PosList_Reject()
        {
            // Arrange
            var lineValidator = new LineValidator();

            // Act
            bool retVal = lineValidator.Validate("100.0\t150.0 300.0");

            // Assert
            Assert.IsFalse(retVal);
        }


        [TestMethod]
        public void Validate_Line_Coordinate_Reject()
        {
            // Arrange
            var lineValidator = new LineValidator(ListType.Coordinate);

            // Act
            bool retVal = lineValidator.Validate("100,,200 150,300");

            // Assert
            Assert.IsFalse(retVal);
        }

        [TestMethod]
        public void Validate_Line_Coordinate1_Reject()
        {
            // Arrange
            var lineValidator = new LineValidator(ListType.Coordinate);

            // Act
            bool retVal = lineValidator.Validate("100,200A150,300");

            // Assert
            Assert.IsFalse(retVal);
        }

        [TestMethod]
        public void Validate_Point_Coordinate_Success()
        {
            // Arrange
            var pointValidator = new PointValidator(ListType.Coordinate);

            // Act
            bool retVal = pointValidator.Validate("100.0,200.0");

            // Assert
            Assert.IsTrue(retVal);
        }


        [TestMethod]
        public void Validate_PointNoDot_Coordinate_Success()
        {
            // Arrange
            var pointValidator = new PointValidator(ListType.Coordinate);

            // Act
            bool retVal = pointValidator.Validate("100,200");

            // Assert
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void Validate_Point_PosList_Success()
        {
            // Arrange
            var pointValidator = new PointValidator();

            // Act
            bool retVal = pointValidator.Validate("100.0 200.0");

            // Assert
            Assert.IsTrue(retVal);
        }


        [TestMethod]
        public void Validate_PointNoDot_PosList_Success()
        {
            // Arrange
            var pointValidator = new PointValidator();

            // Act
            bool retVal = pointValidator.Validate("100 200");

            // Assert
            Assert.IsTrue(retVal);
        }

        [TestMethod]
        public void Validate_PointNoDot_PosList_Failed()
        {
            // Arrange
            var pointValidator = new PointValidator();

            // Act
            bool retVal = pointValidator.Validate("100 200 3000");

            // Assert
            Assert.IsFalse(retVal);
        }

        [TestMethod]
        public void Validate_PointNoDot_Coordinate_Failed()
        {
            // Arrange
            var pointValidator = new PointValidator(ListType.Coordinate);

            // Act
            bool retVal = pointValidator.Validate("100,200 3000");

            // Assert
            Assert.IsFalse(retVal);
        }

        [TestMethod]
        public void Validate_Point_Coordinate_Failed()
        {
            // Arrange
            var pointValidator = new PointValidator(ListType.Coordinate);

            // Act
            bool retVal = pointValidator.Validate("100,,200");

            // Assert
            Assert.IsFalse(retVal);
        }

        [TestMethod]
        public void Validate_Point_PosList_Failed()
        {
            // Arrange
            var pointValidator = new PointValidator();

            // Act
            bool retVal = pointValidator.Validate("100  200 1");

            // Assert
            Assert.IsFalse(retVal);
        }
    }
}
