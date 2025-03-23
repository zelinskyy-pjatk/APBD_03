using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APBD_03.Containers
{
    public class RefrigeratedContainer : Container
    {
        public string ProductType { get; set; }
        public double ProductMass { get; set; }
        public double Temperature { get; set; } // current container temperature
        public double CurrentMinReqTemperature { get; private set; } // current minimum required temperature for specific product type

        public RefrigeratedContainer() { }

        public void SetTemperature(double newTemperature)
        {
            if(newTemperature < CurrentMinReqTemperature)
                throw new InvalidOperationException(
                    $"Cannot set container temperature to {newTemperature}\nMinim required temperature is {CurrentMinReqTemperature}");
            Temperature = newTemperature;
        }
        
        public void LoadProduct(string productType, double massToLoad, double requiredTemperature) 
        {
            if (string.IsNullOrWhiteSpace(productType))
            {
                throw new ArgumentException("Product type cannot be empty.");
            }

            if (this.ProductType == null)
            {
                this.ProductType = productType;
                this.ProductMass = 0.0;
                Temperature = requiredTemperature;
                CurrentMinReqTemperature = requiredTemperature;
            }
            else
            {
                if (!string.Equals(this.ProductType, productType, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException(
                        $"Cannot load product of other type. Current type {ProductType}.");
                if (requiredTemperature > CurrentMinReqTemperature)
                    CurrentMinReqTemperature = requiredTemperature;
            }

            if(Temperature < requiredTemperature)
            {
                throw new InvalidOperationException(
                    $"Product {productType} cannot be loaded to the container {SerialNumber} because its temperature {Temperature} is lower than required for this type of product {CurrentMinReqTemperature}."
                    );
            }

            base.LoadContainer(massToLoad);

            ProductMass += massToLoad;
            Console.WriteLine($"Product {productType} with mass {massToLoad} was loaded into container {SerialNumber}.");
        }

        public override void SetContainerManually()
        {
            base.SetContainerManually();
            string pattern = @"^KON-RC-\d+(?:\.\d+)?$";
            Regex regex = new Regex(pattern);
            while(!regex.IsMatch(SerialNumber))
            {
                Console.WriteLine("Entered Serial Number is incorrect. Try again please!");
                base.SetSerialNumber(pattern);
            }
            Console.WriteLine("Enter product type and it's min required storage temperature: ");
            string userInput = "";
            Console.WriteLine("Enter product type: ");
            userInput = Console.ReadLine().Trim();
            Console.WriteLine($"Enter {userInput} mass: ");
            double productMass = Convert.ToDouble(Console.ReadLine().Trim());
            Console.WriteLine($"Enter {userInput} min required storage temperature: ");
            double minReqStTemp = Convert.ToDouble(Console.ReadLine().Trim());
            LoadProduct(userInput, productMass, minReqStTemp);
        }
        
        public override void EmptyContainer()
        {
            base.EmptyContainer();
            ProductType = null;
            ProductMass = 0.0;
            CurrentMinReqTemperature = 0.0;
        }

        public override string GenerateSerialNumber(string containerType)
        {
            return base.GenerateSerialNumber("RC");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append($"\nProduct Type: {ProductType} | Product Mass: {ProductMass} | Min Required Temperature: {CurrentMinReqTemperature}\n");
            return sb.ToString();
        }
    }
}
