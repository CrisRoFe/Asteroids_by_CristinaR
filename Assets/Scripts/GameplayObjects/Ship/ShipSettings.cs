using UnityEngine;

namespace Asteroids.GameEntities
{
    [CreateAssetMenu(fileName = "ShipSettings", menuName = "Game Entities/Ship")]
    public class ShipSettings : ScriptableObject
    {
        [Header("Ship Movement")]
        public float RotationSpeed = 100f;
        public float ThrustForce = 3f;
        public float MaxSpeed = 10f;
        public int StartingLife = 5;
    }
}