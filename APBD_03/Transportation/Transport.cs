using System;
using System.Threading.Channels;
using APBD_03.Containers;

namespace APBD_03.Transportation
{
    public class Transport
    {
        public List<Container> ContainersList = new List<Container>();
        public double MaxSpeed { get; set; }
        public int MaxContainersCapacity { get; set; }
        public double MaxContainersWeight { get; set; }
        public string TransportSerialNumber { get; set; }

        public double CurrentTotalWeight => ContainersList.Sum(c => c.Mass + c.TareWeight);

        // -- Load One Containe on the Transport -- //
        public virtual void LoadContainer(Container container)
        {
            if (container == null)
            {
                Console.WriteLine($"Erro! Container was not provided.");
                return;
            }

            if (ContainersList.Count >= MaxContainersCapacity)
            {
                Console.WriteLine($"Transport {TransportSerialNumber} cannot place container {container.SerialNumber}. Maximum capacity for it: {MaxContainersCapacity}");
                return;
            }

            double checkWeight = CurrentTotalWeight + container.Mass + container.TareWeight;
            if (checkWeight > MaxContainersWeight)
            {
                Console.WriteLine($"Transport {TransportSerialNumber} cannot place container {container.SerialNumber}\n" +
                    $"There is no enough space {CurrentTotalWeight} + {container.Mass + container.TareWeight} > {MaxContainersWeight}");
            }

            ContainersList.Add(container);
            Console.WriteLine($"Container {container.SerialNumber} was successfully loaded onto {TransportSerialNumber}");
        }

        // -- Load List of Containers on the Transport -- //
        public virtual void LoadListOfContainers(List<Container> containersToLoad)
        {
            if (containersToLoad == null || containersToLoad.Count == 0)
            {
                Console.WriteLine("No containers were provided.");
                return;
            }

            int newContainersCount = ContainersList.Count + containersToLoad.Count;
            if (newContainersCount >= MaxContainersCapacity)
            {
                Console.WriteLine($"Vehicle {TransportSerialNumber} cannot place all of these containers." +
                    $"\nReason: {TransportSerialNumber} transport can only place {MaxContainersCapacity}.");
                return;
            }

            double totNewWeigth = CurrentTotalWeight + containersToLoad.Sum(c => c.Mass + c.TareWeight);
            if (totNewWeigth > MaxContainersWeight)
            {
                Console.WriteLine($"{TransportSerialNumber} cannot place all of these containers.\nTransport mass would exceed max weight limit {MaxContainersWeight}");
                return;
            }

            ContainersList.AddRange(containersToLoad);
            Console.WriteLine($"List of containers was successfully loaded onto {TransportSerialNumber}");
        }

        // -- Remove Container from the Transport -- //
        public virtual void RemoveContainer(Container container)
        {
            if (container == null)
            {
                Console.WriteLine($"Erro! Container was not provided.");
                return;
            }

            bool isRemoved = ContainersList.Remove(container);
            if (isRemoved) Console.WriteLine($"Container {container.SerialNumber} was removed from {TransportSerialNumber}");
            else Console.WriteLine($"Container {container.SerialNumber} was not found on {TransportSerialNumber}");
        }

        // -- Replace one container with another one on transport -- //
        public virtual void ReplaceContainer(Container oldContainer, Container newContainer)
        {
            if (oldContainer == null || newContainer == null)
            {
                Console.WriteLine($"One or both containers were not provided.");
                return;
            }
            if (oldContainer == newContainer) 
            {
                Console.WriteLine("Old and new containers are the same.");
                return;
            };

            // Check whethe container in on the transport (ship)
            if(!ContainersList.Contains(oldContainer))
            {
                Console.WriteLine($"There are no container {oldContainer.SerialNumber} on the transport {TransportSerialNumber}.");
                return;
            }

            double totNewWeight = (CurrentTotalWeight - (oldContainer.Mass + oldContainer.TareWeight)) + (newContainer.Mass + newContainer.TareWeight);
            if (totNewWeight > MaxContainersWeight)
            {
                Console.WriteLine($"{TransportSerialNumber} cannot replace old container with new one \nTransport mass would exceed max weight limit {MaxContainersWeight}");
                return;
            }

            ContainersList.Remove(oldContainer);
            ContainersList.Add(newContainer);

            Console.WriteLine($"Container {oldContainer.SerialNumber} was replaces with {newContainer.SerialNumber} on transport {TransportSerialNumber}"); ;
        }

        public virtual void SetTransportManually()
        {
            Console.WriteLine("Enter max speed: ");
            MaxSpeed = Convert.ToDouble(Console.ReadLine()?.Trim());
            Console.WriteLine("Enter max containers capacity: ");
            MaxContainersCapacity = Convert.ToInt32(Console.ReadLine()?.Trim());
            Console.WriteLine("Enter max containers weight: ");
            MaxContainersWeight = Convert.ToDouble(Console.ReadLine()?.Trim());
            Console.WriteLine("Enter transport serial number: ");
            TransportSerialNumber = Console.ReadLine().Trim(); 
        }

        public override string ToString()
        {
            return $" -- {TransportSerialNumber} Transport Information -- \n" +
                   $"Max Speed: {MaxSpeed}\n" +
                   $"Max Containers Capacity: {MaxContainersCapacity}\n" +
                   $"Max Containers Weight: {MaxContainersWeight}\n" +
                   $"Current Container Count: {ContainersList.Count}\n" +
                   $"Current Weight: {CurrentTotalWeight}\n";
        }
    }   
}
