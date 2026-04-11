using UnityEngine;

namespace UI
{
    public class CanvaLookCamera : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 camForward = _camera.transform.forward;
            Vector3 camUp = _camera.transform.up;

            transform.LookAt(transform.position + camForward, camUp);
        }
    }
}
