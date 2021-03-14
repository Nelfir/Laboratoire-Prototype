using PlanetExpress.Scripts.Utils.Scripts.Utils.Objects;
using UnityEngine;

namespace PlanetExpress.Scripts.Enemy
{
    [DefaultExecutionOrder(-999)]
    public class EnemyList : Singleton<EnemyList>
    {
        private const string prefix = "Space Ship/";

        public  GameObject NormalRocket;
        public  GameObject NormalMachineGun;

        public  GameObject FragileSpeedsterRocket;
        public  GameObject FragileSpeedsterMachineGun;

        public  GameObject MightyGlacierRocket;
        public  GameObject MightyGlacierMachineGun;

        public  GameObject HybridRocket;
        public  GameObject HybridMachineGun;

        public new void Awake()
        {
            base.Awake();
            
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