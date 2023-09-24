using Entity;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    [CreateAssetMenu(fileName = "Main State", menuName = "States/Main")]
    public class MainState : GameStateMachine.State
    {
        /// The state to push when the game ends.
        public GameStateMachine.State NextState;

        /// The player's score.
        public int Score { get; set; }
        
        private GameStateMachine Machine;
        
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
        }

        private void PlayerDied()
        {
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
