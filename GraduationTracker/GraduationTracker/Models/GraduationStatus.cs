using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public class GraduationStatus
    {
        public Boolean HasGraduated { get; set; }
        public Standing Standing { get; set; } = Standing.None;
    }
}
