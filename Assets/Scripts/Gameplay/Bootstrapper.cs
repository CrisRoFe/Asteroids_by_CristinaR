using UnityEngine;

using Asteroids.Utils;
using Asteroids.GameStateMachine;
using Asteroids.States;

namespace Asteroids.Gameplay
{
    public class Bootstrapper : MonoBehaviour
    {
        private static Bootstrapper Instance;

        private void Awake()
        {
            //Make sure there is only one point of entry on the scene
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;

            ServiceLocator.Initialize();
        }

        private void Start()
        {
            var context = new Context(new StartGame());
            context.ExecuteState();
        }
    }
}