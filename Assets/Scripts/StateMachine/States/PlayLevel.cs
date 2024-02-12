using UnityEngine;

using Asteroids.GameStateMachine;
using Asteroids.Utils;
using Asteroids.Gameplay;

namespace Asteroids.States
{
	class PlayLevel : State
	{
        private GameObject _player;
        private WaveController _waveController;
        private GameController _gameController;

        private const string playerShipPath = "GamePlay/PlayerShip";

        public override void ExecuteState()
        {
            StartMinigame();
        }

        private void StartMinigame()
        {
            //Instantiate the player on the middle of the scene
            var playerGo = Resources.Load<GameObject>(playerShipPath);
            _player ??= Object.Instantiate(playerGo, Vector3.zero, Quaternion.identity);

            //Start asteroids wave
            _waveController ??= new WaveController();
            _waveController.InitWaves();

            _gameController = ServiceLocator.Instance.Get<GameController>();
            _gameController.OnLifeChanged += TransitionToEndGame;
        }

        private void TransitionToEndGame(int life)
        {
            if (life > 0) return;

            _gameController.OnLifeChanged -= TransitionToEndGame;

            _waveController.EndGame();

            //Making sure the player is no longer on the scene
            Object.Destroy(_player);
            _player = null;

            _context.TransitionTo(new EndGame());
            _context.ExecuteState();
        }
    }
}