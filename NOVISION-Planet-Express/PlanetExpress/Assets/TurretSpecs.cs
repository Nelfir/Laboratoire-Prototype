using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New Turret", menuName = "Turret")]
    public class TurretSpecs : ScriptableObject
    {
        public int delay;
    }
}