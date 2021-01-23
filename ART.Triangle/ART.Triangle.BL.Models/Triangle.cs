using System;

namespace ART.Triangle.BL.Models
{
    public class Triangle
    {
        public Guid Id { get; set; }
        public double SideA { get; set; }
        public double SideB { get; set; }
        public double SideC { get; set; }
        public Guid UserId { get; set; }
        public DateTime ChangeDate { get; set; }


    }
}
