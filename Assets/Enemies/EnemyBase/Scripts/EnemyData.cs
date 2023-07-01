using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public float attackRadius = 2f;
        public float chasingRadius = 10f;
        public float chasingSpeed = 5f;
        public float health = 100f;
        public float attackDamage = 15f;
        public float attackCooldown = 2f;
        public float latencyToAttackDamage = 0.5f;
    }
}
