using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;

namespace CometsEvacuation.Components
{
    [ComponentDependency(typeof(CollisionComponent))]
    public class DestroyableComponent : Component
    {

        public List<string> getsDestroyedBy;

        public override void SetDefaultValues()
        {
            getsDestroyedBy = new List<string>();
        }

    }
}