using System.Linq;
using Leaderboard;
using UnityEngine;

namespace States
{
    public class GameStateMachine : MonoBehaviour
    {
        public State InitialState;

        public ILeaderboard Leaderboard;
        
        private State CurrentState;

        public void PushState(State newState)
        {
            if (CurrentState)
            {
                CurrentState.OnExit();
            }
            
            CurrentState = newState;

            if (CurrentState)
            {
                CurrentState.OnEnter(this);
            }
        }

        private void Awake()
        {
            if (FindObjectsOfType<GameStateMachine>()
                .Any(obj => obj != this))
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            PushState(InitialState);

            LocalLeaderboard.LoadFrom($@"{Application.persistentDataPath}\leaderboard.json")
                .ContinueWith(task => Leaderboard = task.Result);
        }

        private void Update()
        {
            if (CurrentState)
            {
                CurrentState.OnUpdate();
            }
        }

        public abstract class State : ScriptableObject
        {
            public virtual void OnEnter(GameStateMachine machine)
            {
            }

            public virtual void OnUpdate()
            {
            }

            public virtual void OnExit()
            {
            }
        }
    }
}
