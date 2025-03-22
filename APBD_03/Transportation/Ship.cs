using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_03.Transportation
{
    public class Ship : Transport
    {
        private int numberOfMembersOfCrew { get; set; }

        public override string ToString()
        {
            return base.ToString() + $"\nNumber of members of crew: {numberOfMembersOfCrew}\n";
        }
    }
}
