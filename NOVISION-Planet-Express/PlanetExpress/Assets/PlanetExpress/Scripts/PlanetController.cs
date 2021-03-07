﻿using System;
using System.Collections.Generic;
using PlanetExpress.Scripts.Universe.Planet.Shared;
using PlanetExpress.Scripts.Universe.Planet.TileObjects.Base;
using PlanetExpress.Scripts.Universe.Planet.TileSlots.Base;
using UnityEngine;

namespace PlanetExpress.Scripts
{
    /// <summary>
    /// The PlanetController handles the init logic of the tiles and placing and removing tiles from its surface.
    /// </summary>
    public class PlanetController : MonoBehaviour
    {
        [HideInInspector] public List<TileSlot> TileSlots;

        public void Start()
        {
            var r = GetComponentsInChildren<ArrowController>();

            Debug.Log("Found " + r.Length + " faces.");

            int SmallCount = 0;
            int BigCount = 0;
            int UnknownCount = 0;

            foreach (ArrowController arrowController in r)
            {
                switch (arrowController.TileType)
                {
                    case TileType.Big:
                        BigCount++;
                        break;
                    case TileType.Small:
                        SmallCount++;
                        break;
                    case TileType.None:
                        UnknownCount++;
                        Debug.LogError("Unknown tile type for arrow controller!");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            Debug.Log(
                "Found " + BigCount + " big faces, " +
                SmallCount + " small faces and " +
                UnknownCount + " unknown faces for a total of " +
                r.Length + " faces.");
        }
    }
}