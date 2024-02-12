using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UI
{
    public class StartGameUI : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        public Button StartButton => startButton;
    }
}