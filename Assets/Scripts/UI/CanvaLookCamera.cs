using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CanvaLookCamera : MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private Image _image;

        private void Start()
        {
            _camera = Camera.main;
            
            _image.DOFade(1f, 0.6f)
                .From(0f)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void Update()
        {
            Vector3 camForward = _camera.transform.forward;
            Vector3 camUp = _camera.transform.up;

            transform.LookAt(transform.position + camForward, camUp);
        }
    }
}
