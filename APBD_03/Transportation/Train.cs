using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_03.Transportation
{
    public class Train : Transport
    {
        bool useCoal { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString() + "\nTrain ");
            if (useCoal) sb.Append("uses coal.\n");
            else sb.Append("does not use coal.\n");
            return sb.ToString();
        }
    }
}
