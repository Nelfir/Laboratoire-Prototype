using PlanetExpress.Scripts.Utils.Scripts.Utils.Objects;
using UnityEngine;
using UnityEngine.Events;

namespace PlanetExpress.Scripts.Currency.Base
{
    public class ValueChangeEvent<T> : UnityEvent<T>
    {
    }

    public class ValueWatcher<T> : Singleton<ValueWatcher<T>>
    {
        public T Value
        {
            get => Value;
            set
            {
                Value = value;
                OnValueChangeEvent.Invoke(Value);
            }
        }

        public ValueChangeEvent<T> OnValueChangeEvent;

        protected override void Awake()
        {
            base.Awake();
            OnValueChangeEvent = new ValueChangeEvent<T>();
        }
    }
}