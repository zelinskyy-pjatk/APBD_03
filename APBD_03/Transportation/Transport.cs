using System;
using APBD_03.Containers;

namespace APBD_03.Transportation
{
    public class Transport
    {
        private List<Container> ContainersList = new List<Container>();
        private double MaxSpeed { get; set; }
        private double MaxContainersCapacity { get; set; }
        private double MaxContainersWeight { get; set; }
        private double TransportSerialNumber { get; set; }
        
        public virtual void LoadContainer(Container container)
        {
            if(ContainersList.Count >= MaxContainersCapacity) 
            {
                Console.WriteLine($"Vehicle {TransportSerialNumber} cannot place container {container.SerialNumber}. Maximum capacity for it: {MaxContainersCapacity}");
                return;
            } 
        }

        public virtual void LoadContainers(List<Container> Containers)
        {
            if(ContainersList.Count + Containers.Count >= MaxContainersCapacity)
            {
                Console.WriteLine($"Vehicle {TransportSerialNumber} cannot place any more containers");
            }
        }

        public virtual void RemoveContainer(Container container)
        {

        }

        public virtual void ReplaceContainer(string OldContainerSerialNumber, string NewContainerSerialNumber)
        {
            if (OldContainerSerialNumber == NewContainerSerialNumber) return;

        }

    }
}
