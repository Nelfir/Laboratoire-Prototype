using PlanetExpress.Scripts.Utils.Scripts.Utils.Objects;
using UnityEngine;
using UnityEngine.Events;

namespace PlanetExpress.Scripts.Currency.Base
{
    public class ValueChangeEvent<T> : UnityEvent<int>
    {
    }

    public class ValueWatcher<T> : Singleton<ValueWatcher<T>>
    {
        private ValueChangeEvent<T> onValueChangeEvent;

        public int Value
        {
            get => Value;
            set
            {
                Value += value;
                onChange.Invoke(Value);
            }
        }

        public ValueChangeEvent<T> onChange;

        protected override void Awake()
        {
            base.Awake();
            Value = int.MaxValue;
            onChange = new ValueChangeEvent<T>();
        }
    }
}