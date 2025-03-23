using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            {
                SendNotification("Attempt to load hazardous cargo into liquid container with serial number: " +
                                 SerialNumber +
                                 " , when cargo's mass is more than 50% of container's maximum payload.");
                return;
            }
            if (!isCargoHazardous && mass > MaximumPayload * 0.9)
            {
                SendNotification("Attempt to load cargo into liquid container with serial number: " +
                                 SerialNumber +
                                 " , when cargo's mass is more than 90% of container's maximum payload.");
                return;
            }
            else base.LoadContainer(mass);
        }

        // -- Pass container type abbriviation 'LC' into serial number generator -- //
        public override string GenerateSerialNumber(string containerType)
        {
            return base.GenerateSerialNumber("LC");
        }

        public override void SetContainerManually()
        {
            base.SetContainerManually();
            string pattern = @"^KON-LC-\d+(?:\.\d+)?$";
            Regex regex = new Regex(pattern);
            while (!regex.IsMatch(SerialNumber))
            {
                Console.WriteLine("Entered Serial Number is incorrect. Try again please!");
                base.SetSerialNumber(pattern);
            }
            Console.WriteLine("Is cargo hazardous? (y/n): ");
            string userChoice = Console.ReadLine().Trim().ToLower();
            if (userChoice == "y") isCargoHazardous = true;
            else if (userChoice == "n") isCargoHazardous = false;
        }

        public void SendNotification(string notification)
        {
            Console.WriteLine("!WARNING!\n" + notification);
        }

        public void SetIsCargoHazardous(bool isCargoHazardous) { this.isCargoHazardous = isCargoHazardous; }
        public bool IsCargoHazardous() { return isCargoHazardous;}

        public override string ToString()
        {
            string baseString = base.ToString();
            if (IsCargoHazardous()) baseString += "\nCargo is hazardous.";
            else baseString += "\nCargo is NOT hazardous.";
            return baseString;
        }
    }
}
