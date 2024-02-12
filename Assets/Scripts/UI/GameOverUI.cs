using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        public Button RestartButton => restartButton;

        [SerializeField] private TMP_Text scoreText;

        public void ShowFinalScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}