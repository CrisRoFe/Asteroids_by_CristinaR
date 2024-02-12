using Asteroids.GameStateMachine;
using Asteroids.SFX;
using Asteroids.UI;
using Asteroids.Utils;

namespace Asteroids.States
{
    class StartGame : State
    {
        private UIController _uiController;

        private const string startSfx = "StartGame";

        public override void ExecuteState()
        {
            InitAllServices();
            ShowStartMenu();
        }

        private void InitAllServices()
        {
            //Make sure all services are initialized at the begining of the game
            var services = ServiceLocator.Instance.GetAllRegisteredServices();
            foreach (var service in services)
            {
                service.InitService();
            }
        }

        private void ShowStartMenu()
        {
            _uiController = ServiceLocator.Instance.Get<UIController>();
            var startMenu = _uiController.ShowStartGameUI();
            startMenu.StartButton.onClick.AddListener(TransitionToPlayLevel);
        }

        private void TransitionToPlayLevel()
        {
            var sfxController = ServiceLocator.Instance.Get<SFXController>();
            sfxController.PlaySfx(startSfx);

            _uiController.HideStartGameUI();

            _context.TransitionTo(new PlayLevel());
            _context.ExecuteState();
        }
    }
}