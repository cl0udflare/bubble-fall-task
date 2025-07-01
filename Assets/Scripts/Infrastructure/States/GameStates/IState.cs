namespace Infrastructure.States.GameStates
{
    public interface IState
    {
        void Enter();
        
        void Exit();
    }
}