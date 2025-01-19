using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace migs.EventSystem
{
    internal class EventDelegate
    {
        public Delegate Delegate { get; set; }
        public Guid UniqueId { get; set; }

        public EventDelegate(Delegate action)
        {
            UniqueId = Guid.NewGuid();
            Delegate = action;
        }
    }
}
