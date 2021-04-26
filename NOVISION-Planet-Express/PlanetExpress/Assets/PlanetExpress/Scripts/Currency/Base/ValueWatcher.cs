using PlanetExpress.Scripts.Utils.Scripts.Utils.Objects;
using UnityEngine.Events;

namespace PlanetExpress.Scripts.Currency.Base
{
    public class ValueChangeEvent<T> : UnityEvent<T>
    {
    }

    public class ValueWatcher<T> : Singleton<ValueWatcher<T>>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChangeEvent.Invoke(_value);
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