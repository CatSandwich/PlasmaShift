using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    [CreateAssetMenu(fileName = "Main State", menuName = "States/Main")]
    public class MainState : GameStateMachine.State
    {
        public override void OnEnter(GameStateMachine machine)
        {
            SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync("Main");
        }
    }
}
