using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_03.Containers
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        private bool isCargoHazardous = false;

        public LiquidContainer() { }
        public LiquidContainer(double mass, double height,
            double tareWeight, double depth,
            string serialNumber, double maximumPayload,
            bool isCargoHazardous)
            : base(mass, height, tareWeight, depth, serialNumber,
                maximumPayload)
        {
            this.isCargoHazardous = isCargoHazardous;
        }

        public override void EmptyContainer()
        {
            base.EmptyContainer();
            isCargoHazardous = false;
        }

        public override void LoadContainer(double mass)
        {
            if (isCargoHazardous && mass > MaximumPayload / 2)
                SendNotification("Attempt to load hazardous cargo into liquid container with serial number: " +
                                 SerialNumber +
                                 " , when cargo's mass is more than 50% of container's maximum payload.");
            if (!isCargoHazardous && mass > MaximumPayload * 0.9)
                SendNotification("Attempt to load cargo into liquid container with serial number: " +
                                 SerialNumber +
                                 " , when cargo's mass is more than 90% of container's maximum payload.");
            else base.LoadContainer(mass);
        }

        public void SendNotification(string notification)
        {
            Console.WriteLine("!WARNING!\n" + notification);
        }
    }
}
