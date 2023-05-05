using UnityEngine;

namespace GameLogic.Movement.Components {

    internal struct Movable {
        public Vector3 velocity;
        public float maxAcceleration;
        public float accelerationForce;
        public float slowDown;
    }
}