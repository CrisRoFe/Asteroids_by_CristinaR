using UnityEngine;

namespace Asteroids.GameEntities
{
    [CreateAssetMenu(fileName = "AsteroidSettings", menuName = "Game Entities/Asteroid")]
    public class AsteroidSettings : ScriptableObject
    {
        public AsteroidType Type;
        public float Speed = 50f;
        public int Score = 1000;
        public int AsteroidMultiplier = 2;
    }

    public enum AsteroidType
    {
        Large,
        Medium,
        Small
    }
}