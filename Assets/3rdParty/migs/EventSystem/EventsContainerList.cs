using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace migs.EventSystem
{
    internal class EventsContainerList<T> : List<BaseEventsContainer<T>>
    {
        public void Unsubscribe(T obj, Guid uniqueId)
        {
            if (!this.ContainsKey(obj))
                return;

            this[obj].Unsubscribe(uniqueId);
        }

        public bool ContainsKey(T key)
        {
            return this.Exists(e => EqualityComparer<T>.Default.Equals(e.PayloadObject, key));
        }

        public BaseEventsContainer<T> this[T key]
        {
            get
            {
                return this.FirstOrDefault(c => EqualityComparer<T>.Default.Equals(c.PayloadObject, key));
            }
        }
    }
}