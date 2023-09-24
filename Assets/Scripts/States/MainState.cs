using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    [CreateAssetMenu(fileName = "Main State", menuName = "States/Main")]
    public class MainState : GameStateMachine.State
    {
        public Object MainScene;
        
        public override void OnEnter(GameStateMachine machine)
        {
            SceneManager.LoadSceneAsync(MainScene.name, LoadSceneMode.Additive);
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync(MainScene.name);
        }
    }
}
