using Asteroids.Gameplay;
using Asteroids.GameStateMachine;
using Asteroids.SFX;
using Asteroids.UI;
using Asteroids.Utils;

namespace Asteroids.States
{
    class EndGame : State
    {
        private UIController _uiController;

        private const string startSfx = "StartGame";

        public override void ExecuteState()
        {
            ShowEndGameScreen();
        }

        private void ShowEndGameScreen()
        {
            _uiController = ServiceLocator.Instance.Get<UIController>();
            var gameOver = _uiController.ShowGameOverUI();
            gameOver.RestartButton.onClick.AddListener(RestartGame);

            var gameController = ServiceLocator.Instance.Get<GameController>();
            gameOver.ShowFinalScore(gameController.GetCurrentScore());

            EndAllServices();
        }

        private void EndAllServices()
        {
            //Make sure all services are initialized at the begining of the game
            var services = ServiceLocator.Instance.GetAllRegisteredServices();
            foreach (var service in services)
            {
                service.EndService();
            }
        }

        private void RestartGame()
        {
            var sfxController = ServiceLocator.Instance.Get<SFXController>();
            sfxController.PlaySfx(startSfx);

            _uiController.HideGameOverUI();

            _context.TransitionTo(new PlayLevel());
            _context.ExecuteState();
        }
    }
}