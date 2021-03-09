using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetExpress.Scripts.Enemy
{
    public enum EnemyType
    {
        NormalRocket,
        NormalMachineGun,
        FragileSpeedersRocket,
        FragileSpeedersMachineGun,
        MightyGlacierRocket,
        MightyGlacierMachineGun,
        HybridMachineGun,
        HybridRocket,
    }

    /// <summary>
    /// WaveSpawnDetails holds a reference to the count of different types of enemies to be spawned during a wave.
    /// </summary>
    public class WaveSpawnDetails
    {
        public List<EnemyType> Enemies = new List<EnemyType>();

        #region Builder Patern for Enemies

        public WaveSpawnDetails Add(EnemyType enemyType, int count = 1)
        {
            for (int i = 0; i < count; i++) Enemies.Add(enemyType);
            return this;
        }

        #endregion
    }

    /// <summary>
    /// Holds a static reference to all of the waves data.
    /// </summary>
    public static class Waves
    {
        public static int CurrentWaveIndex = 0;
        public static Wave CurrentWave;

        public static void NextWave()
        {
            CurrentWaveIndex++;
            Debug.Log("Next wave : " + CurrentWaveIndex + "!");

            if (!WavesDict.ContainsKey(CurrentWaveIndex))
            {
                Debug.LogError("There is no wave with index " + CurrentWaveIndex + "!");
            }

            CurrentWave = WavesDict[CurrentWaveIndex];
        }

        public static Dictionary<int, Wave> WavesDict = new Dictionary<int, Wave>()
        {
            // Cooldown before the game begins
            {
                0,
                new Wave(0, "Cooldown", 5, new WaveSpawnDetails())
            },
            {
                1, new Wave(1, "First Wave", 60,
                    new WaveSpawnDetails()
                        .Add(EnemyType.NormalRocket, 3)
                )
            },
            {
                2, new Wave(2, "Second Wave", 60,
                    new WaveSpawnDetails()
                        .Add(EnemyType.NormalRocket, 5)
                )
            },
            {
                3, new Wave(3, "Third Wave", 60,
                    new WaveSpawnDetails()
                        .Add(EnemyType.NormalMachineGun, 2)
                )
            },
            {
                4, new Wave(4, "Fourth Wave", 60,
                    new WaveSpawnDetails()
                        .Add(EnemyType.NormalRocket, 10)
                )
            },
            {
                5, new Wave(5, "Fifth Wave", 60,
                    new WaveSpawnDetails()
                        .Add(EnemyType.NormalRocket, 3)
                        .Add(EnemyType.NormalMachineGun, 2)
                        .Add(EnemyType.MightyGlacierRocket)
                )
            },
            // End wave
            {
                6,
                new EndWave(6, "End Wave")
            },
        };
    }

    public class EndWave : Wave
    {
        public EndWave(int index, string name) : base(index, name, float.MaxValue, new WaveSpawnDetails())
        {
        }
    }

    /// <summary>
    /// A wave is a period of time where enemies (WaveSpawnDetails) are spawned by the EnemySpawner.
    /// </summary>
    public class Wave
    {
        public readonly int Index;
        public readonly string Name;

        public readonly float DurationSeconds;

        public WaveSpawnDetails WaveSpawnDetails;

        public Wave(int index, string name, float durationSeconds, WaveSpawnDetails waveSpawnDetails)
        {
            Index = index;
            Name = name;
            DurationSeconds = durationSeconds;
            WaveSpawnDetails = waveSpawnDetails;
        }
    }


    public class EnemySpawner : MonoBehaviour
    {
        private EnemyList _enemyList;

        public void Start()
        {
            _enemyList = GetComponent<EnemyList>();
            NextWave();
        }

        private void NextWave()
        {
            Waves.NextWave();
            Debug.Log("Next wave : " + Waves.CurrentWave.Name);
        }
    }
}