using Entity;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    [CreateAssetMenu(fileName = "Main State", menuName = "States/Main")]
    public class MainState : GameStateMachine.State
    {
        /// The state to push when the game ends.
        public LeaderboardState NextState;

        /// The player's score.
        public int Score
        {
            get => ScoreBacking;
            set => ScoreText.text = $"Score: {ScoreBacking = value}";
        }
        private int ScoreBacking;
        
        private GameStateMachine Machine;
        private TextMeshProUGUI ScoreText;
        
        public override void OnEnter(GameStateMachine machine)
        {
            Machine = machine;
            
            SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive)
                .completed += MainSceneLoaded;
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync("Main");
        }

        private void MainSceneLoaded(AsyncOperation op)
        {
            GameObject.FindWithTag("Player")
                .GetComponent<Health>()
                .Die
                .AddListener(PlayerDied);
            
            FindObjectOfType<EnemySpawner>()
                .EnemySpawned
                .AddListener(EnemySpawned);

            ScoreText = GameObject.FindWithTag("ScoreText")
                .GetComponent<TextMeshProUGUI>();
        }

        private void PlayerDied()
        {
            NextState.LastScore = Score;
            Machine.PushState(NextState);
        }

        private void EnemySpawned(Enemy enemy)
        {
            enemy.Die.AddListener(() => EnemyDied(enemy));
        }

        private void EnemyDied(Enemy enemy)
        {
            Score += enemy.Score;
        }
    }
}
