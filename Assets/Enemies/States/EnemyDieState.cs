using Common.CommonScripts;
using Common.CommonScripts.States;
using Player.Scripts.States;
using UnityEngine;

namespace Enemies.States
{
    public class EnemyDieState : StateBase<EnemyBase>
    {
        public EnemyDieState(EnemyBase controller) : base(controller) { }

        private const float TimeToDisableEnemy = 8f;
        
        public override void Enter()
        {
            Controller.OnDied?.Invoke();
            Controller.SetState(State.Die);
            Controller.NavAgent.speed = 0;
                
            Timer.StartTimer(TimeToDisableEnemy, () => Controller.gameObject.SetActive(false));
        }

        public override void Update()
        {
           
        }
        
    }
}