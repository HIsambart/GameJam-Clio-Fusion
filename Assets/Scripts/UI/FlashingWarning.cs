using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FlashingWarning : MonoBehaviour
{
    [SerializeField] private Image _image;

    private void Start()
    {
            
        _image.DOFade(1f, 0.6f)
            .From(0f)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
