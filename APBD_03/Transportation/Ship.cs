using APBD_03.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_03.Transportation
{
    public class Ship : Transport
    {
        private int NumberOfCrewMembers{ get; set; }

        public Ship() : base() { }
        public Ship(int numberOfCrewMembers)
        {
            NumberOfCrewMembers = numberOfCrewMembers;
        }

        public override void LoadContainer(Container container)
        {
            base.LoadContainer(container);
        }

        public override void ReplaceContainer(Container oldContainer, Container newContainer)
        {
            base.ReplaceContainer(oldContainer, newContainer);
        }

        public override void RemoveContainer(Container container)
        {
            base.RemoveContainer(container);
        }

        public override void SetTransportManually()
        {
            base.SetTransportManually();

            Console.WriteLine("Enter number of crew members: ");
            this.NumberOfCrewMembers = Convert.ToInt32(Console.ReadLine().Trim());
        }

        public override string ToString()
        {
            return base.ToString() + $"Number of members of crew: {NumberOfCrewMembers}\n";
        }

    }
}
