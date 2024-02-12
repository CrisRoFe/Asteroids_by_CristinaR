using UnityEngine;
using System.Collections;

using Asteroids.Utils;
using Asteroids.SFX;

namespace Asteroids.GameEntities
{
    public class AsteroidController : GameplayObjectBase
    {
        [Header("Settings")]
        [SerializeField] private AsteroidSettings _settings;

        [Header("Animation")]
        [SerializeField] private Animator _animator;

        private SFXController _sfxController;

        private const float posOffset = 2f;
        private const string smallAsteroidPath = "GamePlay/SmallAsteroid";
        private const string mediumAsteroidPath = "GamePlay/MediumAsteroid";
        private const string hitSfx = "AsteroidHit";
        private const string destroyedBool = "IsDestroyed";
        private const string playerTag = "Player";
        private const string bulletTag = "Bullet";

        internal override void Init()
        {
            InitMovement();
            _sfxController = ServiceLocator.Instance.Get<SFXController>();
        }

        private void InitMovement()
        {
            //Give a random rotation
            _rb.angularVelocity = Random.Range(-90f, 90f);

            //Give the asteroid an initial push
            _rb.AddForce(transform.right * _settings.Speed);
        }

        private void Update()
        {
            CheckOutOfBounds();
        }

        private void SpawnAsteroids(AsteroidType asteroidType, int amount)
        {
            GameObject asteroid = null;
            switch (asteroidType)
            {
                case AsteroidType.Medium:
                    asteroid = Resources.Load<GameObject>(mediumAsteroidPath);
                    break;
                case AsteroidType.Small:
                    asteroid = Resources.Load<GameObject>(smallAsteroidPath);
                    break;
            }

            for (int i = 0; i < amount; i++)
            {
                var rndRotation = Quaternion.Euler(0, 0, Random.Range(0, 270));
                var asteroidObj = ObjectPool.Spawn(asteroid, transform.position * posOffset, rndRotation);

                _gameController.AddAsteroids(asteroidObj);
            }
        }

        private IEnumerator DestroyCoroutine()
        {
            _sfxController.PlaySfx(hitSfx);
            _animator.SetBool(destroyedBool, true);

            //Wait for the animation to finish, 0.6 is the duration of the explosion
            yield return new WaitForSeconds(0.6f);

            ObjectPool.Despawn(gameObject);

            _animator.SetBool(destroyedBool, false);

            _gameController.AddScore(_settings.Score);

            DestroyAsteroid();
        }

        private void DestroyAsteroid()
        {
            _rb.Sleep();

            switch (_settings.Type)
            {
                case AsteroidType.Large:
                    SpawnAsteroids(AsteroidType.Medium, _settings.AsteroidMultiplier);
                    break;
                case AsteroidType.Medium:
                    SpawnAsteroids(AsteroidType.Small, _settings.AsteroidMultiplier);
                    break;
                case AsteroidType.Small:
                    break;
            }

            _gameController.RemoveAsteroids(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(bulletTag))
            {
                ObjectPool.Despawn(collision.gameObject);
                StartCoroutine(DestroyCoroutine());
            }

            if (collision.CompareTag(playerTag))
            {
                StartCoroutine(DestroyCoroutine());

                //if it collides with an asteroid it will break up into pieces til dissapearing
                var shipController = collision.GetComponent<ShipController>();
                if (shipController != null)
                {
                    shipController.RemoveLife();
                }
            }
        }
    }
}