using System;
using System.Linq;
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

        private LocalLeaderboard Leaderboard;
        
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

        private void SceneLoaded(AsyncOperation obj)
        {
            Leaderboard.AddEntry("You", LastScore);
            Debug.Log(string.Join(", ", string.Join("\n", Leaderboard.GetEntries()
                .Select(entry => $"{entry.Name}: {entry.Score}"))));
        }
    }
}
