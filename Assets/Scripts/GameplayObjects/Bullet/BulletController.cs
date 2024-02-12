using UnityEngine;

using Asteroids.Gameplay;
using Asteroids.Utils;

namespace Asteroids.GameEntities
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private BulletSettings settings;

        private Rigidbody2D _rb;
        private GameController _gameController;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _gameController = ServiceLocator.Instance.Get<GameController>();
        }

        private void Update()
        {
            CheckOutOfBounds();
        }

        private void CheckOutOfBounds()
        {
            if (transform.position.x > _gameController.WidthBound || transform.position.x < -_gameController.WidthBound
                || transform.position.y > _gameController.HeightBound || transform.position.y < -_gameController.HeightBound)
            {
                ObjectPool.Despawn(gameObject);
            }
        }

        private void OnEnable()
        {
            _rb.AddForce(transform.right * settings.Speed);
        }

        private void OnDisable()
        {
            _rb.Sleep();
        }
    }
}