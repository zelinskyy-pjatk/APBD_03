using APBD_03.Containers;
using APBD_03.Transportation;
using System.ComponentModel;
using System.Xml.Linq;
using Container = APBD_03.Containers.Container;

namespace APBD_03
{
    internal class DataProcessor
    {
        // Key - container object, Value - container status ("free-to-go" or "loaded")
        private static Dictionary<Container, string> containers = new Dictionary<Container, string>(); // container number + status
        
        // A list of all ships in our system
        private static List<Ship> listOfShips = new List<Ship>();


        // -- Create Methods -- //


        // ** Create new container of chosen type(GC, LC or RC) ** //
        public Container CreateContainer()
        {
            Container container = null;
            while (true)
            {
                Console.WriteLine(" -- Container Types -- "
                            + "\n0 - Cancel Container Creation"
                            + "\n1 - Gas Container"
                            + "\n2 - Liquid Container"
                            + "\n3 - Refrigirated Container"
                            + "\nEnter your choice: ");
                string userInput = Console.ReadLine().Trim();
                if(!int.TryParse(userInput, out int choice))
                {
                    Console.WriteLine("Invalid user input.");
                    continue;
                }
                if(choice == 0)
                {
                    Console.WriteLine("Container was not created.\nReason: Container creation process was canceled.");
                    return null;
                }

                switch (choice)
                {
                    case 1:
                        container = new GasContainer();
                        break;
                    case 2:
                        container = new LiquidContainer();
                        break;
                    case 3:
                        container = new RefrigeratedContainer();
                        break;
                    default:
                        Console.WriteLine("Unknown container type option was chosen. Try again.");
                        continue;
                }
                break;
            }

            if(container != null)
            {
                container.SetContainerManually();
                containers[container] = "free-to-go"; //"free-to-go" - default status to show that container is available to be loaded on the transport
                Console.WriteLine($"Container {container.SerialNumber} was created with default status 'free-to-go'.");
            }

            return container;
        }

        // ** Create a new transport of type Ship ** //
        public Ship CreateShip()
        {
            Ship ship = new Ship();
            ship.SetTransportManually();
            return ship;
        }


        // -- Choose Methods -- //


        // ** Choose container out of available ones ** //
        public Container? ChooseContainer()
        {
            if(containers.Count == 0)
            {
                Console.WriteLine("There are no containers available on the list.");
                return null;
            }

            int i = 1;
            Console.WriteLine(new string('-',50));
            Console.WriteLine("\n -- Available Containers -- ");
            foreach (var pair in containers)
            {
                Container c = pair.Key;
                string c_status = pair.Value;
                if (c_status == "free-to-go") Console.WriteLine($"{i} - {c}\nStatus: {c_status}");
                i++;
            }
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Enter your choice: ");
            if(!int.TryParse(Console.ReadLine().Trim(), out int choice)
                || choice < 1 || choice > containers.Count) 
            {
                Console.WriteLine("Invalid choice.");
                return null;
            }

            return containers.ElementAt(choice - 1).Key;
        }

        public Container? ChooseContainer(Ship ship)
        {
            int i = 1;

            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"\n -- [{ship.TransportSerialNumber}] Ship Containers -- ");
            foreach (var c in ship.ContainersList)
            {
                Console.WriteLine(new string('-', 20));
                Console.WriteLine($"{i} - {c}");
                Console.WriteLine(new string('-', 20));
                i++;
            }
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Enter your choice: ");
            if (!int.TryParse(Console.ReadLine().Trim(), out int choice)
                || choice < 1 || choice > containers.Count)
            {
                Console.WriteLine("Invalid choice.");
                return null;
            }

            return ship.ContainersList.ElementAt(choice - 1);
        }
        // ** Choose list of containers out of available ones ** //
        public List<Container>? ChooseListOfContainers()
        {
            if (containers.Count == 0)
            {
                Console.WriteLine("There are no containers available on the list.");
                return null;
            }

            List<Container> chosenListOfContainers = new List<Container>();
            while (true)
            {
                int i = 1;

                Console.WriteLine(new string('-', 50));
                Console.WriteLine("\n -- Available Containers -- ");
                foreach (var pair in containers)
                {
                    Container c = pair.Key;
                    string c_status = pair.Value;
                    if (c_status == "free-to-go") Console.WriteLine($"{i} - {c}\nStatus: {c_status}");
                    i++;
                }
                Console.WriteLine(new string('-', 50));

                Console.WriteLine("Enter container number to add it or 0 to finish: ");
                if (!int.TryParse(Console.ReadLine().Trim(), out int choice)
                    || choice < 0 || choice > containers.Count)
                {
                    Console.WriteLine("Invalid choice.");
                    return null;
                }

                if (choice == 0) break;

                var chosenContainer = containers.ElementAt(choice - 1).Key;
                chosenListOfContainers.Add(chosenContainer);
                Console.WriteLine($"Container {chosenContainer.SerialNumber} was added to list of chosen containers.");
            }

            return chosenListOfContainers;
        }

        // ** Choose Transport Function ** //
        public Ship ChooseShip()
        {
            if(listOfShips.Count == 0)
            {
                Console.WriteLine("There are no ships available.");
                return null;
            }

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("\n -- Available Ship -- ");
            for (int i = 0; i < listOfShips.Count; i++)
                Console.WriteLine($"{i + 1} - {listOfShips[i].TransportSerialNumber}");
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Enter your choice: ");
            if (!int.TryParse(Console.ReadLine().Trim(), out int choice)
                    || choice < 1 || choice > listOfShips.Count)
            {
                Console.WriteLine("Invalid choice.");
                return null;
            }

            return listOfShips[choice -= 1];
        }


        // -- Setters and Getters -- //


        // ** Add ship to our global list in the system ** //
        public void AddShip(Ship ship)
        {
            if(ship == null)
            {
                Console.WriteLine("Erorr! Ship object is null and was not added to the list.");
                return;
            }
            listOfShips.Add(ship);
            Console.WriteLine($"Ship {ship.TransportSerialNumber} was added to the system.");
        }

        // ** Return a container with given serial number ** //
        public Container GetContainer(string containerSerialNumber)
        {
            foreach (var keyValuePair in containers)
                if (keyValuePair.Key.SerialNumber == containerSerialNumber) return keyValuePair.Key;
            return null;
        }

        // ** Return a ship with given transport serial number ** //
        public Ship GetShip(string transportSerialNumber)
        {
            foreach (Ship ship in listOfShips)
                if (ship.TransportSerialNumber == transportSerialNumber) return ship;
            return null;
        }

        // ** Return a list of all containers along with their statuses ** //
        public Dictionary<Container, string> GetContainers()
        {
            return containers;
        }
        
        // ** Return a list of all ships in the system ** //
        public List<Ship> GetShips() { return listOfShips; }


        // -- (Un)Load Methods -- //

        
        // ** Load container on the ship ** //
        public void LoadContainerOnTransport(Ship ship, Container container)
        {
            if (ship == null)
            {
                Console.WriteLine("Error! Ship for transportation was not provided.");
                return;
            }
            if (container == null) 
            { 
                Console.WriteLine("Erorr! Container was not provided."); 
                return; 
            }

            if (!containers.TryGetValue(container, out string status))
            {
                Console.WriteLine($"Container {container.SerialNumber} was not found in the system.");
            }
            if (status != "free-to-go")
            {
                Console.WriteLine($"Container {container.SerialNumber} is not free-to-go.");
                return;
            }

            ship.LoadContainer(container);

            // Check whether ship loaded this container
            if (ship.ContainersList.Contains(container))
            {
                containers[container] = "loaded";
                Console.WriteLine($"Container {container.SerialNumber} was successfully loaded on the ship {ship.TransportSerialNumber}.");
            } else Console.WriteLine($"Container {container.SerialNumber} was not loaded on the ship {ship.TransportSerialNumber}.");
        }

        // ** Load list of containers on ship ** //
        public void LoadListOfContainersOnShip(Ship ship, List<Container> listOfContainers)
        {
            if (ship == null)
            {
                Console.WriteLine("Error! Ship for transportation was not provided.");
                return;
            }

            if (listOfContainers == null || listOfContainers.Count == 0)
            {
                Console.WriteLine("List of containers was not provided.");
                return;
            }

            foreach (Container container in listOfContainers)
            {
                if (!containers.TryGetValue(container, out string status)) 
                {
                    Console.WriteLine($"Container {container.SerialNumber} not found in the system.");
                    continue;
                }
                if (status != "free-to-go")
                {
                    Console.WriteLine($"Container {container.SerialNumber} is not free-to-go.");
                    continue;
                }

                ship.LoadContainer(container);

                // Check whether ship loaded this container
                if (ship.ContainersList.Contains(container))
                {
                    containers[container] = "loaded";
                    Console.WriteLine($"Container {container.SerialNumber} was successfully loaded on the ship {ship.TransportSerialNumber}.");
                }
                else Console.WriteLine($"Container {container.SerialNumber} was not loaded on the ship {ship.TransportSerialNumber}.");
            }
        }

        // ** Remove container from the ship ** //
        public void RemoveContainer(Ship ship, Container container)
        {
            if (ship == null)
            {
                Console.WriteLine("Error! Ship for transportation was not provided.");
                return;
            }
            if (container == null)
            {
                Console.WriteLine("Erorr! Container was not provided.");
                return;
            }

            ship.RemoveContainer(container);

            // Check whether ship removed this container from its list of containers
            if (!ship.ContainersList.Contains(container))
            {
                containers[container] = "free-to-go";
                Console.WriteLine($"Container {container.SerialNumber} was successfully removed from the ship {ship.TransportSerialNumber}.");
            } else Console.WriteLine($"Container {container.SerialNumber} was not removed from the ship {ship.TransportSerialNumber}");
        }

        // ** Unload contianer from the ship ** //
        public void UnloadContainer(Container container)
        {
            if (container == null)
            {
                Console.WriteLine("Erorr! Container was not provided.");
                return;
            }

            container.EmptyContainer();

            Console.WriteLine($"Container {container.SerialNumber} was unloaded successfully.");
        }


        // -- Replace and Transfer Methods -- //


        // ** Replace container with given serial number with another one on some specific transport (ship) ** //
        public void ReplaceContainer(Ship ship, Container oldContainer, Container newContainer) 
        {
            if (ship == null)
            {
                Console.WriteLine("Error! Ship for transportation was not provided.");
                return;
            }
            if (oldContainer == null || newContainer == null)
            {
                Console.WriteLine("Erorr! One or both containers were not provided.");
                return;
            }
            if(oldContainer == newContainer)
            {
                Console.WriteLine("Error! Containers are the same.");
                return;
            }
            
            // Check whether old container is actually on this ship 
            if(!ship.ContainersList.Contains(oldContainer))
            {
                Console.WriteLine($"Container {oldContainer.SerialNumber} was not found on the ship {ship.TransportSerialNumber}.");
                return;
            }

            if(!containers.TryGetValue(newContainer, out string newContainerStatus) || newContainerStatus != "free-to-go")
            {
                Console.WriteLine($"New container {newContainer.SerialNumber} is not free-to-go");
                return;
            }

            ship.ReplaceContainer(oldContainer, newContainer);

            // Check whether new container was accepted by the ship
            if (ship.ContainersList.Contains(newContainer)) containers[newContainer] = "loaded";

            // Refresh status of old container
            if (!ship.ContainersList.Contains(oldContainer) && containers.ContainsKey(oldContainer)) containers[oldContainer] = "free-to-go";
        }

        public void TransferContainer(Ship ship, Ship newShip, Container container)
        {
            if (ship == null || newShip == null)
            {
                Console.WriteLine("Error! One or both ships were not provided.");
                return;
            }
            if (container == null)
            {
                Console.WriteLine("Erorr! Container was not provided.");
                return;
            }
            if(!ship.ContainersList.Contains(container))
            {
                Console.WriteLine($"There is not container {container.SerialNumber} on the ship {ship.TransportSerialNumber}.");
                return;
            }

            ship.RemoveContainer(container);

            if (!ship.ContainersList.Contains(container)) containers[container] = "free-to-go";
            else
            {
                Console.WriteLine($"Container {container.SerialNumber} was not removed from the ship {ship.TransportSerialNumber}");
                return;
            }

            newShip.LoadContainer(container);
            if(newShip.ContainersList.Contains(container))
            {
                containers[container] = "loaded";
                Console.WriteLine($"Container {container.SerialNumber} was transferred from ship {ship.TransportSerialNumber} to ship {newShip.TransportSerialNumber}.");
            } else Console.WriteLine($"Container {container.SerialNumber} was NOT transferred from ship {ship.TransportSerialNumber} to ship {newShip.TransportSerialNumber}.");
        }


        // -- Validation Methods -- //


        // ** Check whether container with given serial number is in the system ** //
        public bool isInContainers(string serialNumber)
        {
            return containers.Any(pair => pair.Key.SerialNumber == serialNumber);
        }

        // ** Check whether container with given serial number is on the specific transport (ship) ** //
        public bool isOnTransport(Transport transport, string containerSerialNumber)
        {
            if (transport == null) return false;
            return transport.ContainersList.Any(el => el.SerialNumber == containerSerialNumber);
        }
    }
}
