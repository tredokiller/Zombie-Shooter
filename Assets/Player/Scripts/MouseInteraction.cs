using UnityEngine;

namespace Player.Scripts
{
    public class MouseInteraction : MonoBehaviour , IMouseInteraction
    {
        [SerializeField] private LayerMask layerMask;
        private Camera _camera;
        private void Awake()
        {
            _camera = Camera.main;
        }


        private void Update()
        {
            transform.position = GetPointToMousePosition();
        }

        public Vector3 GetPointToMousePosition()
        {
            Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(cameraRay, out hit , layerMask))
            {
                return hit.point;
            }
            
            return Vector3.zero;
        }

        public Quaternion GetRotationToMousePosition(out Vector3 horizontalDirection , Vector3 objPos)
        {
            var point = transform.position;

            horizontalDirection = point - objPos;
            horizontalDirection.y = 0;
            
            return Quaternion.LookRotation(horizontalDirection);
        }
    }
}
