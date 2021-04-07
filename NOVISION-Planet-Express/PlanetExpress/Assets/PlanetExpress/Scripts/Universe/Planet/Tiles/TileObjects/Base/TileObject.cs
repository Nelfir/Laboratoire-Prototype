using System.Collections;
using System.Collections.Generic;
using PlanetExpress.Scripts.Core;
using PlanetExpress.Scripts.Universe.Planet.Tiles.Shared;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots;
using PlanetExpress.Scripts.Utils.VR;
using UnityEngine;

namespace PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base
{
    /// <summary>
    /// A tile Object is an object that can be put in a TileSlot, such as a Turret or a Forest.
    /// </summary>
    public class TileObject : Tile
    {
        public string Name;

        [HideInInspector] public TileSlot ParentSlot;

        [HideInInspector] public int Level;

        public int HealthMax;
        private int HealthCurrent;

        private CanvasTile _canvasTile;

        // [HideInInspector]

        public void Start()
        {
            HealthCurrent = HealthMax;

            _canvasTile = GetComponentInChildren<CanvasTile>();
            UpdateUI();

            AddLockToPointComponent();
            AddDamageableComponent();
        }

        private void AddLockToPointComponent()
        {
            gameObject.AddComponent<LockToPointOrigin>();
        }

        private void AddDamageableComponent()
        {
            DamageableBehaviour d = gameObject.AddComponent<DamageableBehaviour>();
            d.Init(Squad.Friendly);

            d.OnDamaged.AddListener((amount) =>
            {
                // TODO play damaging sound

                HealthCurrent -= amount;
                UpdateHealthUI();

                CheckIfDead();

                Debug.Log(name + " damaged for " + HealthCurrent + " / " + HealthMax + ".");
            });


            StartCoroutine(nameof(BeginHealing));
        }
        
        public IEnumerator BeginHealing()
        {
            if (HealthCurrent < HealthMax)
            {
                HealthCurrent += 1;
            }

            yield return new WaitForSeconds(1);

            StartCoroutine(BeginHealing());
            yield break;
        }

        private void CheckIfDead()
        {
            if (HealthCurrent <= 0)
            {
                Debug.Log("Tile " + name + " was destroyed!");
                Destroy(gameObject);
                // TODO play explosion sound
            }
        }

        public void UpdateUI()
        {
            _canvasTile.UpdateName(Name);
            _canvasTile.UpdateLevel(Level);
            UpdateHealthUI();
        }

        public void UpdateHealthUI()
        {
            _canvasTile.UpdateHealth(HealthCurrent, HealthMax);
        }
    }
}