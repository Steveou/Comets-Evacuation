using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;

namespace CometsEvacuation
{
    public class CollisionEventArgs : EventArgs
    {
        public GameObject Object1 { get; private set; }
        public GameObject Object2 { get; private set; }

        public CollisionEventArgs(GameObject object1, GameObject object2)
        {
            Object1 = object1;
            Object2 = object2;
        }
    }
}
