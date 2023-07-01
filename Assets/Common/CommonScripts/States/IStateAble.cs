using Player.Scripts.States;

namespace Common.CommonScripts.States
{
    public interface IStateAble
    {
        public State GetState(); 
        public void SetState(State newState);
   }
}