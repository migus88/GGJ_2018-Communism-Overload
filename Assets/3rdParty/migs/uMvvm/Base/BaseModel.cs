using migs.EventSystem;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;

namespace migs.uMvvm.Base
{
    [Serializable]
    public abstract class BaseModel : IDisposable
    {
        public string ViewModelName { get; protected set; }
        public string ModelPropertyName { get; protected set; }

        protected List<PropertyInfo> _properties;
        protected Dictionary<string, Guid> _subscriptionKeys = new Dictionary<string, Guid>();

        public BaseModel(string modelPropertyName, string viewModelName)
        {
            ViewModelName = viewModelName;
            ModelPropertyName = modelPropertyName;

            var baseProperties = typeof(BaseModel).GetProperties().ToList();
            this._properties = this.GetType().GetProperties().Where(p => !baseProperties.Exists(bp => bp.Name == p.Name)).ToList();
            
            this.subscribeToEvents();
        }

        protected virtual void subscribeToEvents()
        {
            foreach (var prop in this._properties)
            {
                var key1 = $"<{ViewModelName}.{ModelPropertyName}.{prop.Name}";
                var key2 = $">>{ViewModelName}.{ModelPropertyName}.{prop.Name}";

                var val1 = EventsManager.Instance.Subscribe<string>(key1, (vObj) =>
                {
                    prop.SetValue(this, prop.GetValue(this));
                });

                var val2 = EventsManager.Instance.Subscribe<object>(key2, (vObj) =>
                {
                    prop.SetValue(this, vObj);
                });

                this._subscriptionKeys.Add(key1, val1);
                this._subscriptionKeys.Add(key2, val2);
            }
        }

        protected virtual void notifyPropertyChanged<T>(T value, [CallerMemberName]string propertyName = "")
        {
            EventsManager.Instance.Publish<T>($">{ViewModelName}.{ModelPropertyName}.{propertyName}", value);
        }

        public void Dispose()
        {
            foreach (var item in this._subscriptionKeys)
            {
                EventsManager.Instance.Unsubscribe(item.Key, item.Value);
            }
        }
    }
}
