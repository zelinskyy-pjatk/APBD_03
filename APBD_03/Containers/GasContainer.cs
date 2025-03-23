using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public override string GenerateSerialNumber(string containerType)
        {
            return base.GenerateSerialNumber("GC");
        }

        public override void SetContainerManually()
        {
            base.SetContainerManually();
            string pattern = @"^KON-GC-\d+(?:\.\d+)?$";
            Regex regex = new Regex(pattern);
            while (!regex.IsMatch(SerialNumber))
            {
                Console.WriteLine("Entered Serial Number is incorrect. Try again please!");
                base.SetSerialNumber(pattern);
            }
            Console.WriteLine("Enter pressure: ");
            Pressure = Convert.ToDouble(Console.ReadLine());
        }

        public void SendNotification(string notification)
        {
            Console.WriteLine($"Caution! There is high possibility of hazardous event happening. Container {SerialNumber}.");
        }

        public override string ToString()
        {
            return base.ToString() + "\nPressure: " + Pressure;
        }

        public void SetPressure(double Pressure) { this.Pressure = Pressure; }
        public double GetPressure() { return this.Pressure; }
    }
}
