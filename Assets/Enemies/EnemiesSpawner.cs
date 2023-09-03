using System.Collections.Generic;
using Common.CommonScripts;
using Common.CommonScripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Enemies
{
    [RequireComponent(typeof(EnemiesPool))]
    public class EnemiesSpawner : MonoBehaviour , IEnemiesSpawner
    {
        [SerializeField] private Transform[] spawnPoints;
        
        [SerializeField] private float minSpawnDistance;
        [SerializeField] private float maxSpawnDistance;

        [SerializeField] private float maxEnemiesSpawnRadiusToPoint = 25f;

        private ITarget _target; //Range will be calculated from target position
        private IEnemiesPool _enemiesPool;

        [Inject]
        private void Constructor(ITarget target)
        {
            _target = target;
        }
        
        private void Awake()
        {
            _enemiesPool = GetComponent<IEnemiesPool>();
        }

        public void SpawnEnemy(EnemyType type, int count)
        {
            Transform[] rightSpawnPoints = GetRightSpawnPoints();

            if (rightSpawnPoints.Length == 0)
            {
                return;
            }
            
            for (int i = 0; i < count; i++)
            {
                var enemy = _enemiesPool.GetEnemy(type);
                
                enemy.transform.position = GetSpawnPointForEnemy(rightSpawnPoints);
                enemy.SetActive(true);
            }
        }


        private Vector3 GetSpawnPointForEnemy(Transform[] rightSpawnPoints) //Returns random point with random range for spawning enemy
        {
            var spawnPoint = rightSpawnPoints[Random.Range(0, rightSpawnPoints.Length)].position
                             + new Vector3(Random.Range(0, maxEnemiesSpawnRadiusToPoint), 0,
                                 Random.Range(0, maxEnemiesSpawnRadiusToPoint));
            return spawnPoint;
        }
        
        private Transform[] GetRightSpawnPoints()
        {
            List<Transform> rightSpawnPoints = new List<Transform>();
            Vector3 targetPosition = _target.GetTargetTransform().position;
            foreach (var point in spawnPoints)
            {
                var distance = Vector3.Distance(point.position, targetPosition);
                if (distance < minSpawnDistance || distance > maxSpawnDistance)
                {
                    continue;
                }
                rightSpawnPoints.Add(point);
            }

            if (rightSpawnPoints.Count == 0)
            {
                return spawnPoints;
            }

            return rightSpawnPoints.ToArray();
        }
    }
}
