using ART.Triangle.PL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ART.Triangle.BL
{
    public static class TriangleManager
    {
        public static int Insert(BL.Models.Triangle triangle, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using(TriangleEntities dc = new TriangleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblTriangle tbltriangle = new tblTriangle();
                    tbltriangle.Id = Guid.NewGuid();
                    tbltriangle.SideA = (decimal)triangle.SideA;
                    tbltriangle.SideB = (decimal)triangle.SideB;
                    tbltriangle.SideC = (decimal)triangle.SideC;
                    tbltriangle.UserId = triangle.UserId;
                    tbltriangle.ChangeDate = DateTime.Now;

                    //backfill the transport class id
                    triangle.Id = tbltriangle.Id;

                    dc.tblTriangles.Add(tbltriangle);
                    int results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;

                }
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public static int Update(BL.Models.Triangle triangle, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (TriangleEntities dc = new TriangleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblTriangle tbltriangle = dc.tblTriangles.FirstOrDefault(t => t.Id == triangle.Id);

                    if (tbltriangle != null)
                    {
                        tbltriangle.SideA = (decimal)triangle.SideA;
                        tbltriangle.SideB = (decimal)triangle.SideB;
                        tbltriangle.SideC = (decimal)triangle.SideC;
                        tbltriangle.UserId = triangle.UserId;
                        tbltriangle.ChangeDate = DateTime.Now;

                        int results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();

                        return results; 
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Delete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (TriangleEntities dc = new TriangleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblTriangle tbltriangle = dc.tblTriangles.FirstOrDefault(t => t.Id == id);

                    if (tbltriangle != null)
                    {
                        dc.tblTriangles.Remove(tbltriangle);
                        int results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();

                        return results;
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<BL.Models.Triangle> Load()
        {
            try
            {
                List<BL.Models.Triangle> triangles = new List<Models.Triangle>();

                using (TriangleEntities dc = new TriangleEntities())
                {
                    dc.tblTriangles.ToList().ForEach(t => triangles.Add(new BL.Models.Triangle
                    {
                        Id = t.Id,
                        SideA = (double)t.SideA,
                        SideB = (double)t.SideB,
                        SideC = (double)t.SideC,
                        UserId = t.UserId,
                        ChangeDate = t.ChangeDate
                    }));
                    return triangles;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static BL.Models.Triangle LoadById(Guid id)
        {
            try
            {
                BL.Models.Triangle triangle = new BL.Models.Triangle();

                using (TriangleEntities dc = new TriangleEntities())
                {
                    var tbltriangle = dc.tblTriangles.FirstOrDefault(t => t.Id == id);


                    triangle.Id = tbltriangle.Id;
                    triangle.SideA = (double)tbltriangle.SideA;
                    triangle.SideB = (double)tbltriangle.SideB;
                    triangle.SideC = (double)tbltriangle.SideC;
                    triangle.UserId = tbltriangle.UserId;
                    triangle.ChangeDate = tbltriangle.ChangeDate;

                    return triangle;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void CalcSideC(BL.Models.Triangle triangle)
        {
            try
            {
                using (TriangleEntities dc = new TriangleEntities())
                {
                    var parameterSideA = new SqlParameter
                    {
                        ParameterName = "SideA",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Value = triangle.SideA
                    };

                    var parameterSideB = new SqlParameter
                    {
                        ParameterName = "SideB",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Value = triangle.SideB
                    };

                    var results = dc.Set<spCalcSideCResult>().FromSqlRaw("exec spCalcSideC @SideA, @SideB", parameterSideA, parameterSideB).ToList();

                    foreach (var r in results)
                    {
                        triangle.SideC = r.SideC;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
