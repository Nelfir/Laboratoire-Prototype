using System.Collections;
using System.Collections.Generic;
using PlanetExpress.Scripts.Planet;
using UnityEngine;
using Random = UnityEngine.Random;

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
        public Stack<GameObject> Enemies = new Stack<GameObject>();

        #region Builder Patern for Enemies

        public WaveSpawnDetails Add(GameObject enemyType, int count = 1)
        {
            for (int i = 0; i < count; i++) Enemies.Push(enemyType);
            return this;
        }

        public GameObject EnemyPopNext()
        {
            return Enemies.Pop();
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
                        .Add(EnemyList.Instance.NormalRocket, 10)
                )
            },
            {
                2, new Wave(2, "Second Wave", 60,
                    new WaveSpawnDetails()
                        .Add(EnemyList.Instance.NormalRocket, 5)
                )
            },
            {
                3, new Wave(3, "Third Wave", 60,
                    new WaveSpawnDetails()
                        .Add(EnemyList.Instance.NormalMachineGun, 2)
                )
            },
            {
                4, new Wave(4, "Fourth Wave", 60,
                    new WaveSpawnDetails()
                        .Add(EnemyList.Instance.NormalRocket, 10)
                )
            },
            {
                5, new Wave(5, "Fifth Wave", 60,
                    new WaveSpawnDetails()
                        .Add(EnemyList.Instance.NormalRocket, 3)
                        .Add(EnemyList.Instance.NormalMachineGun, 2)
                        .Add(EnemyList.Instance.MightyGlacierRocket)
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

        public readonly float TotalWaveDurationSeconds;

        public WaveSpawnDetails WaveSpawnDetails;

        public Wave(int index, string name, float totalWaveDurationSeconds, WaveSpawnDetails waveSpawnDetails)
        {
            Index = index;
            Name = name;
            TotalWaveDurationSeconds = totalWaveDurationSeconds;
            WaveSpawnDetails = waveSpawnDetails;
        }

        public float EnemySpawnDelay => TotalWaveDurationSeconds / WaveSpawnDetails.Enemies.Count;

        public int EnemyCount => WaveSpawnDetails.Enemies.Count;
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
            StartCoroutine(nameof(GoWave));
        }

        private IEnumerator GoWave()
        {
            for (int i = 0; i < Waves.CurrentWave.EnemyCount; i++)
            {
                GameObject nextEnemy = Waves.CurrentWave.WaveSpawnDetails.EnemyPopNext();

                if (nextEnemy == null)
                {
                    Debug.LogError("Can't spawn enemy, it is null. " + nextEnemy);
                    continue;
                }

                Debug.Log("Spawning : " + nextEnemy.name);
                GameObject g = Instantiate(nextEnemy);


                Vector3 TileToAttack = PlanetController.Instance.GetNextEnemyTargetTile();

                if (TileToAttack == Vector3.negativeInfinity)
                {
                    yield break;
                }


                // vector pointing from the planet to the player
                Vector3 difference = TileToAttack - PlanetController.Instance.transform.position;

                // the direction of the launch, normalized
                Vector3 directionOnly = difference.normalized;

                // the point along this vector you are requesting
                Vector3 startPos = PlanetController.Instance.transform.position + directionOnly * 10 + Random.insideUnitSphere * 2;

                //Randomize this point a little bit
                var endPos = (PlanetController.Instance.transform.position + directionOnly * 2);

                g.transform.position = startPos;
                
                EnemyBehaviour enemyBehaviour = g.GetComponent<EnemyBehaviour>();
                enemyBehaviour.GetComponent<EnemyBehaviour>().StartMove(endPos);
                enemyBehaviour.GetComponent<EnemyBehaviour>().StartShooting(TileToAttack);

                /* Debug.Log("Waiting for " + Waves.CurrentWave.EnemySpawnDelay + " seconds (...");
                 yield return new WaitForSeconds(Waves.CurrentWave.EnemySpawnDelay);*/
            }

            Debug.Log("Waiting for " + Waves.CurrentWave.EnemySpawnDelay + " seconds (after wave)...");
            yield return new WaitForSeconds(Waves.CurrentWave.TotalWaveDurationSeconds);

            NextWave();
        }


        private Vector3 GetNextEnemyPosition()
        {
            return transform.position * Random.insideUnitCircle * 5;
        }
    }
}