using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;

namespace CometsEvacuation.Components
{
    [ComponentDependency(typeof(CollisionComponent))]
    public class ExplodableComponent : Component
    {
        // define the type of particle system which will be created
    }
}