using migs.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace migs.EventSystem
{
    public class EventsManager : BaseSingleton<EventsManager>
    {
        private EventsContainerList<Type> _typeEvents = new EventsContainerList<Type>();
        private EventsContainerList<string> _stringEvents = new EventsContainerList<string>();

        public void Unsubscribe(string key, Guid uniqueId)
        {
            this._stringEvents.Unsubscribe(key, uniqueId);
        }

        public void Unsubscribe<T>(Guid uniqueId)
        {
            this._typeEvents.Unsubscribe(typeof(T), uniqueId);
        }

        public void Unsubscribe(Type type, Guid uniqueId)
        {
            this._typeEvents.Unsubscribe(type, uniqueId);
        }

        public Guid Subscribe<T>(string key, Action<T> action)
        {
            if (!this._stringEvents.ContainsKey(key))
                this._stringEvents.Add(new BaseEventsContainer<string>(key));

            var del = new EventDelegate(action);
            this._stringEvents[key].Delegates.Add(del);

            return del.UniqueId;
        }

        public Guid Subscribe<T>(Action<T> action)
        {
            var type = typeof(T);

            if (!this._typeEvents.ContainsKey(type))
                this._typeEvents.Add(new BaseEventsContainer<Type>(type));


            var del = new EventDelegate(action);
            this._typeEvents[type].Delegates.Add(del);

            return del.UniqueId;
        }

        public void Publish<T>(string key, T payload)
        {
            if (!this._stringEvents.ContainsKey(key))
                return;

            foreach (var action in this._stringEvents[key].Delegates)
            {
                action.Delegate.DynamicInvoke(payload);
            }
        }

        public void Publish<T>(T payload)
        {
            var type = typeof(T);

            if (!this._typeEvents.ContainsKey(type))
                return;

            foreach (var action in this._typeEvents[type].Delegates)
            {
                action.Delegate.DynamicInvoke(payload);
            }
        }
    }
}
