using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_03.Containers
{
    public class GasContainer : Container, IHazardNotifier
    {
        public double Pressure { get; set; }

        public GasContainer() { }
        public GasContainer(double mass, double height,
            double tareWeight, double depth,
            string serialNumber, double maximumPayload, double pressure)
            : base(mass, height, tareWeight, depth, serialNumber,
                maximumPayload)
        {
            this.Pressure = pressure;
        }

        public override void EmptyContainer()
        {
            this.Mass -= Mass * 0.95;
        }

        public override void LoadContainer(double mass)
        {

            base.LoadContainer(mass);
        }

        public void SendNotification(string notification)
        {
            throw new NotImplementedException();
        }
    }
}
