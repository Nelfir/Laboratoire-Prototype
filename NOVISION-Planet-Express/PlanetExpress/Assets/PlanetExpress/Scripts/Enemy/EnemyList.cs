using System;
using UnityEngine;

namespace PlanetExpress.Scripts.Enemy
{
    
    [DefaultExecutionOrder(-999)]
    public class EnemyList : MonoBehaviour
    {
        private const string prefix = "Space Ship/";

        public static GameObject NormalRocket;
        public static GameObject NormalMachineGun;

        public static GameObject FragileSpeedsterRocket;
        public static GameObject FragileSpeedsterMachineGun;

        public static GameObject MightyGlacierRocket;
        public static GameObject MightyGlacierMachineGun;

        public static GameObject HybridRocket;
        public static GameObject HybridMachineGun;

        public void Awake()
        {
            NormalRocket = Resources.Load<GameObject>(prefix + "Normal (Rocket)");
            NormalMachineGun = Resources.Load<GameObject>(prefix + "Normal (Machine Gun)");

            FragileSpeedsterRocket = Resources.Load<GameObject>(prefix + "Fragile Speedster (Rocket)");
            FragileSpeedsterMachineGun = Resources.Load<GameObject>(prefix + "Fragile Speedster (Machine Gun)");

            MightyGlacierRocket = Resources.Load<GameObject>(prefix + "Mighty Glacier (Rocket)");
            MightyGlacierMachineGun = Resources.Load<GameObject>(prefix + "Mighty Glacier (Machine Gun)");

            HybridRocket = Resources.Load<GameObject>(prefix + "Hybrid (Rocket)");
            HybridMachineGun = Resources.Load<GameObject>(prefix + "Hybrid (Machine Gun)");
        }
    }
}