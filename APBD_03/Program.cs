using APBD_03.Containers;
using APBD_03.Transportation;
using System.Net.Sockets;

namespace APBD_03
{
    internal class Program
    {
        private static Container container;
        private static Ship ship;       
        private static DataProcessor processor = new DataProcessor();

        public static void Main(String[] args)
        {
            while (true)
            {
                int userChoice = Menu();
                if (userChoice == 0) System.Environment.Exit(0);
                
                switch (userChoice)
                {
                    case 1:
                        {
                            container = processor.CreateContainer();
                            break;
                        }
                    case 2:
                        {
                            ship = processor.CreateShip();
                            processor.AddShip(ship);
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Choose container: ");
                            container = processor.ChooseContainer();
                            if (container == null) break;
                            Console.WriteLine("Enter mass of load: ");
                            if (!double.TryParse(Console.ReadLine(), out double mass))
                            {
                                Console.WriteLine("You entered invalid number");
                                break;
                            }

                            try
                            {
                                container.LoadContainer(mass);
                            } catch (OverfillException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Choose a container: ");
                            container = processor.ChooseContainer();
                            if (container == null) break;

                            Console.WriteLine("Choose a ship to load container: ");
                            ship = processor.ChooseShip();
                            if (ship == null) break;

                            processor.LoadContainerOnTransport(ship, container);
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Choose a list of containers to be transported: ");
                            var containersList = processor.ChooseListOfContainers();
                            if(containersList == null) break;

                            Console.WriteLine("Choose ship to load these containers: ");
                            ship = processor.ChooseShip();
                            if (ship == null) break;

                            processor.LoadListOfContainersOnShip(ship, containersList);
                            break;
                        }
                    case 6:{
                            Console.WriteLine("Choose ship: ");
                            ship = processor.ChooseShip();
                            if(ship == null) break;

                            Console.WriteLine("Choose container to be removed: ");
                            container = processor.ChooseContainer(ship);
                            if(container == null) break;

                            processor.RemoveContainer(ship, container);
                            break;

                        }
                    case 7:
                        {
                            Console.WriteLine("Enter container you want to unload: ");
                            container = processor.ChooseContainer();
                            
                            processor.UnloadContainer(container);                            
                            break;

                        }
                    case 8:
                        {
                            Console.WriteLine("Choose ship for replacement: ");
                            ship = processor.ChooseShip();
                            if (ship == null) break;

                            Container replacer = new Container();
                            Console.WriteLine("Choose an old container to be replaced: ");
                            container = processor.ChooseContainer(ship);
                            if(container == null) break;

                            Console.WriteLine("Do you want to create a new container to replace an old one with? (y/n): ");
                            string usrChoice = Console.ReadLine().Trim().ToLower();
                            if (usrChoice == "y") replacer = processor.CreateContainer();
                            else if (usrChoice == "n") replacer = processor.ChooseContainer();
                            else Console.WriteLine("Error occured! Incorrect option chosen.");
                            if (replacer == null) break;

                            processor.ReplaceContainer(ship, container, replacer);
                            break;
                        }
                    case 9:
                        {

                            Console.WriteLine("Choose ship FROM which container will be transfered: ");
                            ship = processor.ChooseShip();
                            if (ship == null) break;

                            Console.WriteLine("Choose ship TO which container will be transfered: ");
                            var newShip = processor.ChooseShip();
                            if (newShip == null) break;

                            Console.WriteLine("Choose container to be transfered: ");
                            container = processor.ChooseContainer(ship);
                            if (container == null) break;

                            processor.TransferContainer(ship, newShip, container);
                            break;
                        }
                    case 10:
                        {
                            Console.WriteLine("Enter serial number of container to get an information: ");
                            Console.WriteLine(processor.GetContainer(Console.ReadLine().Trim()));
                            break;
                        }
                    case 11:
                        {
                            Console.WriteLine("Enter transport serial number of ship to get an information: ");
                            Console.WriteLine(processor.GetShip(Console.ReadLine().Trim()));
                            break;
                        }
                }
            }
        }

        public static int Menu()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("List of containers: ");
            foreach(var keyValuePair in processor.GetContainers())
                Console.WriteLine(keyValuePair.Key + $"\nStatus: {keyValuePair.Value}\n");
            Console.WriteLine(new string('-', 50) + '\n' + new string('/', 50) + '\n' + new string('-', 50));
            Console.WriteLine($"List of ships:\n {String.Join('\n', processor.GetShips())}\n");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine(" -- Menu -- ");
            Console.WriteLine("0 - Exit");
            Console.WriteLine("1 - Create a container of given type");
            Console.WriteLine("2 - Create a vehicle of given type");
            Console.WriteLine("3 - Load cargo into container");
            Console.WriteLine("4 - Load container onto some kind of transport");
            Console.WriteLine("5 - Load list of containers onto some kind of transport");
            Console.WriteLine("6 - Remove a container from the transport");
            Console.WriteLine("7 - Unload container");
            Console.WriteLine("8 - Replace container with another one");
            Console.WriteLine("9 - Transfer container to another transport");
            Console.WriteLine("10 - Show information about some specific container");
            Console.WriteLine("11 - Show information about some transport");
            Console.WriteLine("Enter your choice: ");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}


