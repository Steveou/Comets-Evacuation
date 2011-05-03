using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;

namespace CometsEvacuation.Components
{
    public enum ItemType
    {
        None,
        TimeBonus,
        PitchLarger,
        PitchSmaller,
        ChangeKeys,
        Weapon,
        AddBomb,
        PitchFaster,
        PitchSlower
    }

    public class ItemComponent : Component
    {
        public ItemType Type { get; set; }

        public override void SetDefaultValues()
        {
            Type = ItemType.None;
        }
    }
}
