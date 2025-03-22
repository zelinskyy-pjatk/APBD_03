using APBD_03.Containers;
using APBD_03.Transportation;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;
using System.Xml.Serialization;

namespace APBD_03
{
    internal class Program
    {
        private static APBD_03.Containers.Container container = new APBD_03.Containers.Container();
        private static Transport transport = new Transport();
        private static List<APBD_03.Containers.Container> containers = new List<APBD_03.Containers.Container>();
        private static List<Transport> transportList = new List<Transport>();


        // -- Create Container of Type Function -- //
        public static APBD_03.Containers.Container CreateContainer()
        {
            Console.WriteLine(" -- Container Types -- "
                            + '\n' + "1 - Gas Container" + '\n'
                            + "2 - Liquid Container" + '\n'
                            + "3 - Refrigirated Container" + '\n'
                            + "Enter your choice: ");
            switch (Convert.ToInt32(Console.ReadLine()))
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
            }
            container.SetContainerManually();
            return container;
        }

        // -- Create Transport of Type Function -- //
        public static Transport CreateTransport()
        {
            if (container is null) Console.WriteLine("There was no container to load");
            Console.WriteLine(" -- Transportation Options -- " + '\n'
                            + "1 - Ship" + '\n' + "2 - Train" + '\n'
                            + "3 - Truck" + '\n' + "Enter your choice: ");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1: return new Ship();
                case 2: return new Train();
                case 3: return new Truck();
            }
            return transport;
        }
        
        // -- Choose Container Function -- //
        public static APBD_03.Containers.Container ChooseContainer()
        {
            int j = 0;
            for (int i = 0; i < containers.Count; i++)
            {
                j = i + 1;
                Console.WriteLine(j + " - " + containers[i].GetType() + " " + containers[i].SerialNumber);
            }
            Console.WriteLine("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            return containers[choice -= 1];
        }

        // -- Choose Containers List Function -- //
        public static List<APBD_03.Containers.Container> ChooseListOfContainers()
        {
            List<APBD_03.Containers.Container> listOfContainers = new List<APBD_03.Containers.Container> ();
            int j = 0;
            for (int i = 0; i < containers.Count; i++)
            {
                j = i + 1;
                Console.WriteLine(j + " - " + containers[i].GetType() + " " + containers[i].SerialNumber);
            }
            int choice = 0;
            do
            {
                Console.WriteLine("Enter 0 - if you want to exit\nEnter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());
                if (listOfContainers.Contains(containers[choice])) Console.WriteLine("Erorr! This container is already on the list.");
                listOfContainers.Add(containers[choice - 1]);
            } while (choice != 0);
            return listOfContainers;
        }


        // -- Choose Transport Function -- //
        public static Transport ChooseTransport() {


            return null;
        
        }


        public static void Main(String[] args)
        {
            while (true)
            {
                int userChoice = Menu();
                if (userChoice == 0) System.Environment.Exit(0);
                else
                {
                    switch (userChoice)
                    {
                        case 1: 
                            {
                                container = CreateContainer();
                                containers.Add(container);
                                break;
                            }
                        case 2:
                            {
                                transport = CreateTransport();
                                transportList.Add(transport);   
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Choose container: ");
                                container = ChooseContainer();
                                Console.WriteLine("Enter mass of load you want to load: ");
                                container.LoadContainer(Double.Parse(Console.ReadLine()));
                                break;
                            }
                        case 4:
                            {
                                transport.LoadContainer(container);
                                break;
                            }
                        case 5:
                            {

                                break;
                            }
                        case 6:
                            {
                                transport.RemoveContainer(container);
                                break;
                                
                            }
                        case 7:
                            {
                                //transport.UnloadContainer();
                                break;
                                
                            }
                        case 8:
                            {
                                Console.WriteLine("Choose a new container to be loaded onto transport");
                                APBD_03.Containers.Container newContainer = ChooseContainer();
                                //transport.ReplaceContainer(container, newContainer);
                                break;
                            }
                        case 9:
                            {
                                break;
                            }
                        case 10:
                            {
                                Console.WriteLine("Enter serial number of container to get an information: ");
                                foreach (APBD_03.Containers.Container c in containers)
                                    if (c.SerialNumber.Equals(Console.ReadLine()))
                                        Console.WriteLine(c);
                                break;
                            }
                            case 11:
                            {
                                // /????
                                Console.WriteLine("Choose transport to show information about.");
                                transport = CreateTransport();
                                Console.WriteLine(transport);
                                break;
                            }
                    }
                }
            }
        }

        public static int Menu()
        {
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


