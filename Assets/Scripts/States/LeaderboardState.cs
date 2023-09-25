using System;
using System.Collections;
using Leaderboard;
using Leaderboard.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    [CreateAssetMenu(menuName = "States/Leaderboard")]
    public class LeaderboardState : GameStateMachine.State
    {
        public LeaderboardEntry EntryPrefab;
        public GameStateMachine.State NextState;

        [NonSerialized]
        public int LastScore;

        private GameStateMachine Machine;
        private LocalLeaderboard Leaderboard;
        
        public override void OnEnter(GameStateMachine machine)
        {
            Machine = machine;
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

            var leaderboard = GameObject.FindWithTag("Leaderboard")
                .GetComponent<RectTransform>();

            leaderboard.sizeDelta *= Vector2.right;

            foreach (LocalLeaderboard.Entry entry in Leaderboard.GetEntries())
            {
                LeaderboardEntry uiEntry = Instantiate(EntryPrefab, leaderboard);
                leaderboard.sizeDelta += Vector2.up * EntryPrefab.GetComponent<RectTransform>().sizeDelta.y;
                uiEntry.Name.text = entry.Name.ToLower();
                uiEntry.Score.text = entry.Score.ToString();
            }

            Machine.StartCoroutine(WaitForPlayAgain());
        }

        private IEnumerator WaitForPlayAgain()
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            Machine.PushState(NextState);
        }
    }
}
