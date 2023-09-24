using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    [CreateAssetMenu(menuName = "States/Title")]
    public class TitleState : GameStateMachine.State
    {        
        public override void OnEnter(GameStateMachine machine)
        {
            SceneManager.LoadSceneAsync("Title", LoadSceneMode.Additive);
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync("Title");
        }
    }
}
