using Common.CommonScripts.States;
using Enemies.States;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies.DefaultZombie.Scripts
{
    public class DefaultZombieController : EnemyBase
    {
        [SerializeField] private GameObject[] zombieMeshes;

        private float _toTargetDistance;

        private IState _chaseState;
        private IState _attackState;
        private IState _idleState;
        private IState _dieState;

        private void Awake()
        {
            SetEnemyBaseData();
            
            _chaseState = new EnemyChaseState(this);
            _attackState = new EnemyAttackState(this , LatencyToAttackDamage);

            _idleState = new EnemyIdleState(this);
            _dieState = new EnemyDieState(this);
            
            Mesh = Instantiate(zombieMeshes[Random.Range(0, zombieMeshes.Length)], transform);
            NavAgent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            SetEnemyBaseData();
            OnDamaged += () => StateMachine.SetState(_chaseState);
        }

        private void Update()
        {
           UpdateState();
        }
        
        private void UpdateState()
        {
            if (IsDied)
            {
                StateMachine.SetState(_dieState);
            }
            else
            {
                _toTargetDistance = Vector3.Distance(transform.position, TargetTransform.position);
            
                if (_toTargetDistance >= ChasingRadius)
                {
                    StateMachine.SetState(_idleState);
                }
                else if(_toTargetDistance <= AttackRadius)
                {
                    StateMachine.SetState(_attackState);
                }
                else
                {
                    StateMachine.SetState(_chaseState);
                }
            }
            StateMachine.UpdateStateMachine();
        }

        private void OnDisable()
        {
            OnDamaged -= () => StateMachine.SetState(_chaseState);
        }

        public override void DamageTarget()
        {
            if(_toTargetDistance <= AttackRadius)
            {
                TargetDamageable.TookDamage(Damage);
            }
        }
    }
}
