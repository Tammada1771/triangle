using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ART.Triangle.BL.Test
{
    [TestClass]
    public class utTriangle
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(1, TriangleManager.Load().Count());
        }

        [TestMethod]
        public void CalcSideCTest()
        {
            int expected = 5;
            Models.Triangle triangle = new Models.Triangle();
            triangle.SideA = 3;
            triangle.SideB = 4;
            TriangleManager.CalcSideC(triangle);

            Assert.AreEqual(expected, triangle.SideC);
        }

        [TestMethod]
        public void InsertTest()
        {
            int results = TriangleManager.Insert(new Models.Triangle
            {
                Id = Guid.Empty,
                SideA = 10,
                SideB = 11,
                SideC = 12
            }, true);
            Assert.AreEqual(1, results);
        }
    }
}
