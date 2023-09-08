using Common.CommonScripts;
using Common.CommonScripts.States;
using Player.Scripts.States;
using UnityEngine;

namespace Enemies.States
{
    public class EnemyDieState : StateBase<EnemyBase.Scripts.EnemyBase>
    {
        public EnemyDieState(EnemyBase.Scripts.EnemyBase controller) : base(controller)
        {
            IsPrimaryState = true;
        }

        private const float TimeToDisableEnemy = 8f;
        
        public override void Enter()
        {
            Controller.OnDied?.Invoke();
            Controller.SetState(State.Die);
            Controller.NavAgent.speed = 0;
            
            Controller.PlaySound(ActionSoundType.Death);
            
            Controller.GameManager.AddSubtractMoney(Controller.enemyData.countMoneyForKill);
                
            Timer.StartTimer(TimeToDisableEnemy, () => Controller.gameObject.SetActive(false));
        }

        public override void Update()
        {
           
        }
        
    }
}