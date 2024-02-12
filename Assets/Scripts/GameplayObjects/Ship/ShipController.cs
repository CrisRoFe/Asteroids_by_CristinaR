using UnityEngine;

using Asteroids.Utils;
using Asteroids.SFX;

namespace Asteroids.GameEntities
{
    public class ShipController : GameplayObjectBase
    {
        [Header("Settings")]
        [SerializeField] private ShipSettings shipSettings;

        [Header("Shooting")]
        [SerializeField] private Transform bulletSpawnPoint;

        private int _playerLife;
        private SFXController _sfxController;

        private const string bulletPath = "Gameplay/Bullet";
        private const string shootSfx = "Shoot";
        private const string hitSfx = "ShipHit";

        internal override void Init()
        {
            //Make sure the player life is reset
            _playerLife = shipSettings.StartingLife;
            _gameController.OnLifeChanged?.Invoke(_playerLife);

            _sfxController = ServiceLocator.Instance.Get<SFXController>();
        }

        private void Update()
        {
            CheckOutOfBounds();
            UpdateMovement();
            Shoot();
        }

        private void UpdateMovement()
        {
            RotateShip();
            MoveShip();
        }

        private void RotateShip()
        {
            transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * shipSettings.RotationSpeed * Time.deltaTime);
        }

        private void MoveShip()
        {
            if (_rb == null) return;
            _rb.AddForce(transform.right * shipSettings.ThrustForce * Input.GetAxis("Vertical"));

            //Applying a limit on the rb speed
            var clampedVelocity = Vector3.ClampMagnitude(_rb.velocity, shipSettings.MaxSpeed);
            _rb.velocity = clampedVelocity;
        }

        private void Shoot()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var bulletPrefab = Resources.Load<GameObject>(bulletPath);
                ObjectPool.Spawn(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

                _sfxController.PlaySfx(shootSfx);
            }
        }

        private void OnDisable()
        {
            _rb.Sleep();
        }

        public void RemoveLife()
        {
            _playerLife--;
            _gameController.OnLifeChanged?.Invoke(_playerLife);

            _sfxController.PlaySfx(hitSfx);
        }
    }
}