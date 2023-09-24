using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    [CreateAssetMenu(menuName = "States/Load")]
    public class LoadState : GameStateMachine.State
    {
        public GameStateMachine.State FirstState;
        
        private GameStateMachine Machine;
        
        public override void OnEnter(GameStateMachine machine)
        {
            Machine = machine;
            Machine.StartCoroutine(Load());
        }

        private IEnumerator Load()
        {
            yield return SceneManager.LoadSceneAsync("Background", LoadSceneMode.Additive);
            Machine.PushState(FirstState);
            yield return SceneManager.UnloadSceneAsync("Load");
        }
    }
}
