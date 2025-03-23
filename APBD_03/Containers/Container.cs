using System;
using System.Text.RegularExpressions;

namespace APBD_03.Containers
{
    public class Container
    {
        private int serialNumberCounter = 1;
        private DataProcessor processor = new DataProcessor();

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

        // -- Empty Container Function -- //
        public virtual void EmptyContainer()
        {
            Mass = 0.0;
        }

        // -- Load Container Function -- //
        public virtual void LoadContainer(double mass)
        {
            if (mass > MaximumPayload)
                throw new OverfillException(
                    "The mass must be less than maximum payload weight. " +
                    "Maximum payload weight for this container is: {MaximumPayload}");
            Mass += mass;
        }

        public virtual string GenerateSerialNumber(String containerType)
        {
            string generatedSerialNumber;
            do
            {
                generatedSerialNumber = $"KON-{containerType}-{serialNumberCounter++}";
            } while (processor.isInContainers(generatedSerialNumber));
            return generatedSerialNumber;
        }


        // -- Check Serial Number -- //
        public bool CheckSerialNumber(String serialNumber)
        {
            String pattern = @"^KON-[A-Z]+-\d+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(serialNumber) && !processor.isInContainers(serialNumber);
        }

        // -- Check Serial Number with Given Pattern -- //
        public bool CheckSerialNumber(String serialNumber, String pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(serialNumber) && !processor.isInContainers(serialNumber);
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

        public virtual void SetContainerManually()
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

            Console.WriteLine("Do you want serial number to be generated automatically for you? (y/n): ");
            String choice = Console.ReadLine().Trim().ToLower();
            if (choice == "y")
            {
                this.SerialNumber = GenerateSerialNumber("C");
                Console.WriteLine("Generated Serial Number: " + this.SerialNumber);
            }
            else
            {
                string userSerialNumber;
                do
                {
                    Console.WriteLine("Enter serial number using 'KON-C-1' format: ");
                    userSerialNumber = Console.ReadLine();
                    if (!CheckSerialNumber(userSerialNumber))
                    {
                        Console.WriteLine("Error! The serial number format is wrong or serial number is not unique. Try again.");
                    }
                } while (!CheckSerialNumber(userSerialNumber));
                this.SerialNumber = userSerialNumber;
            }
            Console.WriteLine("Enter maximum payload: ");
            this.MaximumPayload = Convert.ToDouble(Console.ReadLine());
        }

        public double GetMass() { return this.Mass; }
        
        public void SetSerialNumber(String pattern)
        {
            Console.WriteLine("Do you want serial number to be generated automatically for you? (y/n): ");
            String choice = Console.ReadLine().Trim().ToLower();
            if (choice == "y")
            {
                this.SerialNumber = GenerateSerialNumber("C");
                Console.WriteLine("Generated Serial Number: " + this.SerialNumber);
            }
            else
            {
                string userSerialNumber;
                do
                {
                    Console.WriteLine("Enter serial number using 'KON-C-1' format: ");
                    userSerialNumber = Console.ReadLine();
                    if (!CheckSerialNumber(userSerialNumber, pattern))
                    {
                        Console.WriteLine("Error! The serial number format is wrong or serial number is not unique. Try again.");
                    }
                } while (!CheckSerialNumber(userSerialNumber, pattern));
                this.SerialNumber = userSerialNumber;
            }
        }

        // -- ToString() method -- //
        public override string ToString()
        {
            return $" -- {SerialNumber} Container Information -- \n\nCargo Mass: {Mass} kg\nTare Container Weight: {TareWeight} kg\n" +
                   $"Height: {Height} cm\nDepth: {Depth} cm\nSerial Number: {SerialNumber} ";
        }
    }
}