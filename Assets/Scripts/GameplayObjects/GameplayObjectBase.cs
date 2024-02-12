using UnityEngine;

using Asteroids.Gameplay;
using Asteroids.Utils;

namespace Asteroids.GameEntities
{
    public abstract class GameplayObjectBase : MonoBehaviour
    {
        internal Rigidbody2D _rb;
        internal GameController _gameController;

        //Offset so the objects are always in-bounds when respawned
        private const float respawnOffset = 0.1f;

        private void OnEnable()
        {
            _rb = GetComponent<Rigidbody2D>();
            if (_rb == null) _rb = gameObject.AddComponent<Rigidbody2D>();

            _gameController = ServiceLocator.Instance.Get<GameController>();

            Init();
        }

        internal abstract void Init();

        internal void CheckOutOfBounds()
        {
            if (transform.position.x > _gameController.WidthBound)
            {
                transform.position = new Vector3(-_gameController.WidthBound + respawnOffset, transform.position.y, 0);
            }

            if (transform.position.x < -_gameController.WidthBound)
            {
                transform.position = new Vector3(_gameController.WidthBound - respawnOffset, transform.position.y, 0);
            }

            if (transform.position.y > _gameController.HeightBound)
            {
                transform.position = new Vector3(transform.position.x, -_gameController.HeightBound + respawnOffset, 0);
            }

            if (transform.position.y < -_gameController.HeightBound)
            {
                transform.position = new Vector3(transform.position.x, _gameController.HeightBound - respawnOffset, 0);
            }
        }
    }
}