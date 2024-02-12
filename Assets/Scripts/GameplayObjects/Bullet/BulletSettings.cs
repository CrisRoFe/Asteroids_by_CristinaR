using UnityEngine;

namespace Asteroids.GameEntities
{
    [CreateAssetMenu(fileName = "BulletSettings", menuName = "Game Entities/Bullet")]
    public class BulletSettings : ScriptableObject
    {
        [Header("Bullet Movement")]
        public float Speed = 400f;
    }
}