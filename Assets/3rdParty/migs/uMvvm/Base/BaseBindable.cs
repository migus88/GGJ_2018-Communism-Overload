using migs.EventSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace migs.uMvvm.Base
{
    public abstract class BaseBindable : MonoBehaviour
    {
        [HideInInspector]
        public string ViewModelName;
        [HideInInspector]
        public string ModelPropertyName;
        [HideInInspector]
        public string PropertyName;
    }
    
    public abstract class BaseBindable<TValue, THolder> : BaseBindable
    {
        public THolder Holder { get; set; }
        public TValue Value
        {
            get
            {
                return this._value;
            }
            protected set
            {
                this._value = value;
                EventsManager.Instance.Publish<object>($">>{ViewModelName}.{ModelPropertyName}.{PropertyName}", (object)value);
            }
        }
        
        private TValue _value;
        protected Guid _guid;

        protected virtual void Awake()
        {
            Holder = GetComponent<THolder>();
            this._guid = EventsManager.Instance.Subscribe<TValue>($">{ViewModelName}.{ModelPropertyName}.{PropertyName}", this.processValue);
        }

        protected virtual void OnEnable()
        {
            this.requestUpdate();
        }

        protected virtual void Start()
        {
            this.requestUpdate();
        }

        protected virtual void OnDestroy()
        {
            EventsManager.Instance.Unsubscribe($">{ViewModelName}.{ModelPropertyName}.{PropertyName}", this._guid);
        }

        protected virtual void onValueChanged(TValue value)
        {
            if (Comparer<TValue>.Default.Compare(Value, value) != 0)
                Value = value;
        }

        protected virtual void processValue(TValue value)
        {
            this.handleValueUpdate(value);

            if (Comparer<TValue>.Default.Compare(Value, value) == 0)
                return;

            Value = value;
        }

        protected virtual void requestUpdate()
        {
            EventsManager.Instance.Publish<string>($"<{ViewModelName}.{ModelPropertyName}.{PropertyName}", string.Empty);
        }

        protected abstract void handleValueUpdate(TValue value);

    }
}