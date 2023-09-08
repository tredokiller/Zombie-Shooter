namespace Common.CommonScripts.States
{
    public interface IState
    {
        public void Enter();
        public void Update();
        public bool CanChangeToAnotherState();
        public bool GetIsPrimaryState();
    }
}