

using System.Text.RegularExpressions;

namespace APBD_03.Containers
{
    public class Container
    {
        public double Mass { get; set; }
        public double Height { get; set; }
        public double TareWeight { get; set; }
        public double Depth { get; set; }
        public string SerialNumber { get; set; }
        public double MaximumPayload { get; set; }

        public Container() { }

        public Container(double Mass, double Height,
            double TareWeight, double Depth,
            string SerialNumber, double MaximumPayload)
        {
            this.Mass = Mass;
            this.Height = Height;
            this.TareWeight = TareWeight;
            this.Depth = Depth;
            this.SerialNumber = SerialNumber;
            this.MaximumPayload = MaximumPayload;
        }

        // -- Empty Container -- //
        public virtual void EmptyContainer()
        {
            Mass = 0.0;
        }

        // -- Load Container -- //
        public virtual void LoadContainer(double mass)
        {
            if (mass > MaximumPayload)
                throw new OverfillException(
                    "The mass must be less than maximum payload weight. Maximum payload weight for this container is: " +
                    MaximumPayload);
            else this.Mass = mass;
        }


        // -- Check Serial Number -- //
        public bool CheckSerialNumber(String serialNumber)
        {
            String pattern = "KON-C-\\d+(\\.\\d+)?\r\n";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(serialNumber)) return true;
            return false;
        }

        // -- Setters and Getters -- //
        public void SetContainer(double Mass, double Height,
                                 double TareWeight, double Depth,
                                 string SerialNumber, double MaximumPayload)
        {
            if(!CheckSerialNumber(SerialNumber))
            {
                Console.WriteLine("Error! The serial number format is wrong or serial number is not unique. Try again.");
                return;
            }
            this.Mass = Mass;
            this.Height = Height;
            this.TareWeight = TareWeight;
            this.Depth = Depth;
            this.SerialNumber = SerialNumber;
            this.MaximumPayload = MaximumPayload;
        }

        public void SetContainerManually()
        {
            Console.WriteLine("Enter container parameters: ");
            Console.WriteLine("Enter mass: ");
            this.Mass = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter height: ");
            this.Height = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter tare weight: ");
            this.TareWeight = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter depth: ");
            this.Depth = Convert.ToDouble(Console.ReadLine());
            do
            {
                Console.WriteLine("Enter serial number using 'KON-C-1' format: ");
                SerialNumber = Console.ReadLine();
                if (!CheckSerialNumber(SerialNumber))
                {
                    Console.WriteLine("Error! The serial number format is wrong or serial number is not unique. Try again.");
                    return;
                }
            } while(CheckSerialNumber(SerialNumber) == false);
            this.SerialNumber = SerialNumber;
            Console.WriteLine("Enter maximum payload: ");
            this.MaximumPayload = Convert.ToDouble(Console.ReadLine());
        }

        public double GetMass() { return this.Mass; }
        

        // -- ToString() method -- //
        public override string ToString()
        {
            return SerialNumber.ToString() +
                   $" Container Information\nCargo Mass: {Mass} kg\nTare Container Weight: {TareWeight} kg\n" +
                   $"Height: {Height} cm\nDepth: {Depth} cm\nSerial Number: {SerialNumber}\n";
        }
    }
}