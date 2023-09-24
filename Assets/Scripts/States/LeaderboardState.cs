using System;
using Leaderboard;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    [CreateAssetMenu(menuName = "States/Leaderboard")]
    public class LeaderboardState : GameStateMachine.State
    {
        public GameStateMachine.State NextState;

        [NonSerialized]
        public int LastScore;

        private ILeaderboard Leaderboard;
        
        public override void OnEnter(GameStateMachine machine)
        {
            Leaderboard = machine.Leaderboard;
            
            SceneManager.LoadSceneAsync("Leaderboard", LoadSceneMode.Additive)
                .completed += SceneLoaded;
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync("Leaderboard");
        }

        private async void SceneLoaded(AsyncOperation obj)
        {
            await Leaderboard.AddEntry("You", LastScore);
            Debug.Log(Leaderboard.GetEntries());
        }
    }
}
