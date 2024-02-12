using UnityEngine;
using System;
using System.Collections.Generic;

using Asteroids.Utils;

namespace Asteroids.Gameplay
{
    public class GameController : MonoBehaviour, IGameService
    {
        public float WidthBound { get; private set; }
        public float HeightBound { get; private set; }

        private List<GameObject> _currentAsteroids;
        public Action OnRemoveAsteroid;

        public Action<int> OnLifeChanged;

        private int _currentScore;
        public Action<int> OnScoreChanged;

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);
        }

        public void InitService()
        {
            GetScreenBounds();

            _currentAsteroids ??= new List<GameObject>();
            RemoveAllAsteroids();
        }

        public void EndService()
        {
            ResetScore();
            RemoveAllAsteroids();
        }

        public void ResetScore()
        {
            _currentScore = 0;
            OnScoreChanged?.Invoke(_currentScore);
        }

        public void AddScore(int score)
        {
            _currentScore += score;
            OnScoreChanged?.Invoke(_currentScore);
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }

        public void AddAsteroids(GameObject asteroid)
        {
            _currentAsteroids.Add(asteroid);
        }

        public void RemoveAsteroids(GameObject asteroid)
        {
            _currentAsteroids.Remove(asteroid);
            OnRemoveAsteroid?.Invoke();
        }

        public int GetAsteroidsCount()
        {
            return _currentAsteroids.Count;
        }

        private void GetScreenBounds()
        {
            var bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            WidthBound = bounds.x;
            HeightBound = bounds.y;
        }

        private void RemoveAllAsteroids()
        {
            //Despawn outstanding asteroids
            foreach (var asteroid in _currentAsteroids)
            {
                ObjectPool.Despawn(asteroid);
            }
            _currentAsteroids.Clear();
        }
    }
}