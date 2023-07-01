using Common.CommonScripts;
using Common.CommonScripts.States;
using Player.Scripts.States;
using UnityEngine;

namespace Enemies.States
{
    public class EnemyAttackState : StateBase<EnemyBase>
    {
        public EnemyAttackState(EnemyBase controller, float latencyToGiveDamageToPlayer) : base(controller)
        {
            _latencyToGiveDamageToPlayer = latencyToGiveDamageToPlayer;
        }
        private bool _canAttack = true;
        private const float RotationToPlayer = 7f;
        

        private const float LatencyToAttack = 0.2f;
        private readonly float _latencyToGiveDamageToPlayer = 0.7f;

        public override void Enter()
        {
            Controller.NavAgent.speed = 0;
            Controller.SetState(State.Attack);
            IsCanChangeState = true;
        }

        private void TryToAttack()
        {
            RotationToTarget();
            if (_canAttack)
            {
                IsCanChangeState = true;
                _canAttack = false;

                Timer.StartTimer(LatencyToAttack, Attack);
            }
        }
        private void Attack()
        {
            IsCanChangeState = false;
            Controller.OnAttacked?.Invoke();
            Timer.StartTimer(Controller.AttackCooldown, () => _canAttack = true);
            Timer.StartTimer(_latencyToGiveDamageToPlayer , Controller.DamageTarget);
        }

        private void RotationToTarget()
        {
            var direction = Controller.TargetTransform.position - Controller.transform.position;

            Quaternion rotation = Quaternion.LookRotation(direction);
            Controller.transform.rotation = Quaternion.Lerp(Controller.transform.rotation,
                Quaternion.Euler(Controller.transform.rotation.eulerAngles.x, rotation.eulerAngles.y,
                    Controller.transform.rotation.eulerAngles.z), RotationToPlayer * Time.deltaTime);
        }

        public override void Update()
        {
            TryToAttack();
        }
    }
}