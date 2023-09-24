using States;
using UnityEngine;

namespace UI
{
    public class StateLoader : MonoBehaviour
    {
        public GameStateMachine.State State;

        public void Load()
        {
            FindObjectOfType<GameStateMachine>()
                .PushState(State);
        }
    }
}
