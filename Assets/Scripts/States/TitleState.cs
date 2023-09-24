using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    [CreateAssetMenu(menuName = "States/Title")]
    public class TitleState : GameStateMachine.State
    {
        public Object TitleScene;
        
        public override void OnEnter(GameStateMachine machine)
        {
            SceneManager.LoadSceneAsync(TitleScene.name, LoadSceneMode.Additive);
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync(TitleScene.name);
        }
    }
}
