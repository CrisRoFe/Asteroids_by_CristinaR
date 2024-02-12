using UnityEngine;
using TMPro;

using Asteroids.Utils;
using Asteroids.Gameplay;

namespace Asteroids.UI
{
    public class UIController : MonoBehaviour, IGameService
    {
        [Header("Canvas")]
        [SerializeField] private Transform canvasTransf;

        [Header("Screen Text")]
        [SerializeField] private TMP_Text lifeText;
        [SerializeField] private TMP_Text scoreText;

        private StartGameUI _startGameUI;
        private GameOverUI _gameOverUI;

        private GameController _gameController;

        private const string startUIPath = "UI/StartGameUI";
        private const string gameOverUIPath = "UI/GameOverUI";

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);
        }

        public void InitService()
        {
            _gameController = ServiceLocator.Instance.Get<GameController>();
            _gameController.OnLifeChanged += UpdateLife;
            _gameController.OnScoreChanged += UpdateScore;
        }

        public void EndService(){}

        public StartGameUI ShowStartGameUI()
        {
            var startGameUI = Resources.Load<StartGameUI>(startUIPath);
            _startGameUI = Instantiate(startGameUI, canvasTransf);

            return _startGameUI;
        }

        public void HideStartGameUI()
        {
            Destroy(_startGameUI.gameObject);
            _startGameUI = null;
        }

        public GameOverUI ShowGameOverUI()
        {
            var gameOverUI = Resources.Load<GameOverUI>(gameOverUIPath);
            _gameOverUI = Instantiate(gameOverUI, canvasTransf);

            return _gameOverUI;
        }

        public void HideGameOverUI()
        {
            Destroy(_gameOverUI.gameObject);
            _gameOverUI = null;
        }

        public void UpdateLife(int life)
        {
            lifeText.text = life.ToString();
        }

        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}