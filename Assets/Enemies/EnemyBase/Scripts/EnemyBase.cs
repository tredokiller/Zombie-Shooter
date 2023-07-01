using System;
using Common.CommonScripts;
using Common.CommonScripts.States;
using UnityEngine;
using UnityEngine.AI;
using Weapons.Scripts;
using Zenject;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(Collider))]
    public abstract class EnemyBase : MonoBehaviour, IDamageable, IStateAble
    {
        [NonSerialized] public NavMeshAgent NavAgent;
        public GameObject Mesh { protected set; get; }
        public Transform TargetTransform { private set; get; }
        public IDamageable TargetDamageable { private set; get; }

        protected StateMachine StateMachine;

        private Collider _collider;
        private State _enemyState;

        public Action OnDamaged;
        public Action OnAttacked;
        public Action OnDied;
        
        protected bool IsDied;
        
        public EnemyData enemyData;

        protected float Health;
        public float ChasingSpeed { private set; get; }
        protected float Damage { private set; get; }
        protected float LatencyToAttackDamage;
        protected float AttackRadius;
        protected float ChasingRadius;

        public float AttackCooldown { private set; get; }

        [Inject]
        private void Constructor(ITarget target)
        {
            TargetTransform = target.GetTargetTransform();
            TargetDamageable = TargetTransform.gameObject.GetComponent<IDamageable>();
            StateMachine = GetComponent<StateMachine>();
            _collider = GetComponent<Collider>();
        }
        protected void SetEnemyBaseData()
        {
            Health = enemyData.health;
            ChasingSpeed = enemyData.chasingSpeed;
            Damage = enemyData.attackDamage;
            AttackRadius = enemyData.attackRadius;
            AttackCooldown = enemyData.attackCooldown;
            ChasingRadius = enemyData.chasingRadius;
            LatencyToAttackDamage = enemyData.latencyToAttackDamage;

            IsDied = false;
            _collider.enabled = true;
        }

        public abstract void DamageTarget();


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Tags.BulletTag))
            {
                IBullet bullet = collision.gameObject.GetComponent<IBullet>();
                bullet.SetBulletActive(false);
                TookDamage(bullet.GetDamage());
            }
        }


        public void TookDamage(float tookDamage)
        {
            Health -= tookDamage;
            if (Health <= 0)
            {
                IsDied = true;
                _collider.enabled = false;
                return;
            }

            OnDamaged?.Invoke();
        }

        public State GetState()
        {
            return _enemyState;
        }

        public void SetState(State newState)
        {
            _enemyState = newState;
        }
    }
}