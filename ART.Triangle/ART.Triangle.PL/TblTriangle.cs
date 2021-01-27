using System;
using System.Collections.Generic;

#nullable disable

namespace ART.Triangle.PL
{
    public partial class tblTriangle
    {
        public Guid Id { get; set; }
        public decimal SideA { get; set; }
        public decimal SideB { get; set; }
        public decimal SideC { get; set; }
        public Guid UserId { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
