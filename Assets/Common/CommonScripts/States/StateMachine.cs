using Player.Scripts.States;
using UnityEngine;

namespace Common.CommonScripts.States
{
    public class StateMachine : MonoBehaviour
    {
        private IState _state;
        protected bool IsNewState;

        public void SetState(IState state)
        {
            if (_state != null)
            {
                if (state.GetIsPrimaryState() == false)
                {
                    if (_state.CanChangeToAnotherState() == false)
                    {
                        return;
                    }
                }

                var previousState = _state;
                if (previousState != state)
                {
                    state.Enter();
                }
            }
            else
            {
                state.Enter();
            }
            _state = state;
        }

        public void UpdateStateMachine()
        {
            if (_state != null)
            {
                _state.Update();
            }
        }
    }
}