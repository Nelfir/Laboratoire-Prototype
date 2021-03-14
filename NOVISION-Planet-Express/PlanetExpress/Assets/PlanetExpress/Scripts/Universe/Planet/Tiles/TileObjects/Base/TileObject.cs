using System;
using PlanetExpress.Scripts.Core;
using PlanetExpress.Scripts.Universe.Planet.Tiles.Shared;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots;
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

        [HideInInspector] public int HealthCurrent;
        [HideInInspector] public int HealthMax;

        private CanvasTile _canvasTile;

        // [HideInInspector]

        public void Start()
        {
            _canvasTile = GetComponentInChildren<CanvasTile>();
            UpdateUI();

            AddDamageableComponent();
        }

        private void AddDamageableComponent()
        {
            DamageableBehaviour d = gameObject.AddComponent<DamageableBehaviour>();
            
            d.OnDamaged.AddListener((amount) =>
            {
                HealthCurrent -= amount;
                UpdateHealthUI();
            });
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