﻿using PlanetExpress.Scripts.Utils.Scripts.Utils.Objects;

namespace PlanetExpress.Scripts.Currency
{
    public class CurrencyController : Singleton<CurrencyController>
    {
        public int Money { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Money = int.MaxValue;
        }

        public void UpdateMoney(int Amount)
        {
            // TODO make money sound
            Money += Amount;
        }
    }
}