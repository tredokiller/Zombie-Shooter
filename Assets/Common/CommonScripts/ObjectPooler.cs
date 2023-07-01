using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.CommonScripts
{
    [Serializable]
    public class ObjectPooler
    {
        public EnemyType enemyType;
        public GameObject objPrefab;
        public int initialPoolSize;
        [NonSerialized] public List<GameObject> ListOfObjects = new List<GameObject>();
    }
}