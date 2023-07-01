using System.Collections.Generic;
using System.Linq;
using Common.CommonScripts;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class EnemiesPool : MonoBehaviour , IEnemiesPool
    {
        [SerializeField] private GameObject enemiesParent;

        [SerializeField] private List<ObjectPooler> poolers;

        [Inject] private DiContainer _diContainer;
        

        private void Start()
        {
            for (int i = 0; i < poolers.Count; i++)
            {
                GameObject enemy = _diContainer.InstantiatePrefab(poolers[i].objPrefab, enemiesParent.transform);
                enemy.SetActive(false);
                poolers[i].ListOfObjects.Add(enemy);
            }
        }


        public GameObject GetEnemy(EnemyType enemyType)
        {
            var selectedPooler = poolers.First();

            foreach (var pooler in poolers)
            {
                if (pooler.enemyType == enemyType)
                {
                    selectedPooler = pooler;
                }
            }
            
            foreach (GameObject enemy in selectedPooler.ListOfObjects)
            {
                if (!enemy.activeSelf)
                {
                    enemy.SetActive(false);
                    return enemy;
                }
            }
            
            GameObject newEnemy= _diContainer.InstantiatePrefab(selectedPooler.objPrefab, enemiesParent.transform);
            newEnemy.SetActive(false);
            selectedPooler.ListOfObjects.Add(newEnemy);

            return newEnemy;
        }
    }
}