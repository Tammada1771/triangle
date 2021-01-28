using Microsoft.VisualStudio.TestTools.UnitTesting;
using ART.Triangle.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ART.Trianlge_PL.Test
{
    [TestClass]
    public class utTriangle
    {
        protected TriangleEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            dc = new TriangleEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }

        [TestMethod]
        public void LoadTest()
        {
            int expected = 1;
            int actual = 0;

            actual = dc.tblTriangles.Count(); // SELECT * FROM tblTriangle

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalcSideCTest()
        {
            // test the call with 3 and 4. should result in 5;
            double expected = 5;
            double? actual = 0;

            var parameterSideA = new SqlParameter
            {
                ParameterName = "SideA",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Value = 3
            };

            var parameterSideB = new SqlParameter
            {
                ParameterName = "SideB",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Value = 4
            };

            var results = dc.Set<spCalcSideCResult>().FromSqlRaw("exec spCalcSideC @SideA, @SideB", parameterSideA, parameterSideB).ToList();

            foreach(var r in results)
            {
                actual = r.SideC;
            }
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            int expected = 1;
            int actual = 0;

            tblTriangle newrow = new tblTriangle();
            newrow.Id = Guid.NewGuid();
            newrow.UserId = Guid.NewGuid();
            newrow.SideA = 5;
            newrow.SideB = 12;
            newrow.SideC = 13;
            newrow.ChangeDate = DateTime.Now;

            dc.tblTriangles.Add(newrow);
            actual = dc.SaveChanges();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            //Select * from tbltriangle t where t.SideA = 5 and t.SideB = 12
            tblTriangle row = dc.tblTriangles.Where(t => t.SideA == 5 && t.SideB == 12).FirstOrDefault();

            int actual = 0;

            if(row != null)
            {
                row.SideA = 6;
                row.SideB = 7;
                row.SideC = 8;
                actual = dc.SaveChanges();

            }

            Assert.IsTrue(actual > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            //Select * from tbltriangle t where t.SideA = 5 and t.SideB = 12
            tblTriangle row = dc.tblTriangles.Where(t => t.SideA == 5 && t.SideB == 12).FirstOrDefault();

            int actual = 0;

            if (row != null)
            {

                dc.tblTriangles.Remove(row);
                //dc.tblTriangles.RemoveRange(Array of tblTriangles);

                actual = dc.SaveChanges();

            }

            Assert.IsTrue(actual > 0);
        }
    }
}
