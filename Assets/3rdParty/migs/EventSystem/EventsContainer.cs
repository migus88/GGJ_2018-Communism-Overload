using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace migs.EventSystem
{
    internal class BaseEventsContainer<T>
    {
        public List<EventDelegate> Delegates { get; set; }
        public T PayloadObject { get; set; }
        
        public BaseEventsContainer(T obj)
        {
            Delegates = new List<EventDelegate>();
            PayloadObject = obj;
        }

        public void Unsubscribe(Guid uniqueId)
        {
            var existing = Delegates.FirstOrDefault(d => d.UniqueId == uniqueId);

            if (existing != null)
                Delegates.Remove(existing);
        }
    }
}
