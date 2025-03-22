using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_03.Containers
{
    public class RefrigeratedContainer : Container
    {
        private Dictionary<string, double> ProductsList { get; set; }
        private double Temperature { get; set; }
        private bool CanStoreOneTypeProducts { get; set; }

        public RefrigeratedContainer() { }

        

        public override void EmptyContainer()
        {
            base.EmptyContainer();
        }

        public override void LoadContainer(double mass)
        {

            base.LoadContainer(mass);
        }
    }
}
