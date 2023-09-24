using UnityEngine;

namespace States
{
    [CreateAssetMenu(menuName = "States/Leaderboard")]
    public class LeaderboardState : GameStateMachine.State
    {
        public GameStateMachine.State NextState;
        
        public override void OnEnter(GameStateMachine machine)
        {
            machine.PushState(NextState);
        }
    }
}
