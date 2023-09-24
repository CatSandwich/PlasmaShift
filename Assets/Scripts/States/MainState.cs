using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    [CreateAssetMenu(fileName = "Main State", menuName = "States/Main")]
    public class MainState : GameStateMachine.State
    {
        public GameStateMachine.State NextState;
        
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
            GameObject player = GameObject.FindWithTag("Player");

            player.GetComponent<Health>().Die.AddListener(() =>
            {
                Machine.PushState(NextState);
            });
        }
    }
}
