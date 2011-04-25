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
    }
}