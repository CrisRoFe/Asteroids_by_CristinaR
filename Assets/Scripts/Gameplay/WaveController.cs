using UnityEngine;

using Asteroids.Utils;

namespace Asteroids.Gameplay
{
    public class WaveController
    {
        private GameController _gameController;
        private int _wave = 0;

        //Used so there is a bit of logic when moving to the next wave
        private const int defaultAsteroids = 2;
        private const string largeAsteroidPath = "GamePlay/LargeAsteroid";

        public WaveController()
        {
            _gameController = ServiceLocator.Instance.Get<GameController>();
        }

        public void InitWaves()
        {
            _gameController.OnRemoveAsteroid += CheckNextWave;

            _wave = 0;
            CheckNextWave();
        }

        public void EndGame()
        {
            _gameController.OnRemoveAsteroid -= CheckNextWave;
        }

        private void CheckNextWave()
        {
            if (_gameController.GetAsteroidsCount() > 0) return;

            _wave++;

            //Simple logic to spawn the asteroids per wave
            var waveAsteroids = _wave + defaultAsteroids;
            SpawnWaveAsteroids(waveAsteroids);
        }

        private void SpawnWaveAsteroids(int amount)
        {
            var asteroid = Resources.Load<GameObject>(largeAsteroidPath);

            for (int i = 0; i < amount; i++)
            {
                var rndPos = new Vector2(Random.Range(-_gameController.WidthBound, _gameController.WidthBound),
                    Random.Range(-_gameController.HeightBound, _gameController.HeightBound));
                var rndRotation = Quaternion.Euler(0, 0, Random.Range(0, 270));

                var asteroidObj = ObjectPool.Spawn(asteroid, rndPos, rndRotation);
                _gameController.AddAsteroids(asteroidObj);
            }
        }
    }
}