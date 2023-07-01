
using UnityEngine;

namespace Player.Scripts
{
    public interface IMouseInteraction
    {
        public Quaternion GetRotationToMousePosition(out Vector3 horizontalDirection, Vector3 objPos);
        public Vector3 GetPointToMousePosition();
    }
}
