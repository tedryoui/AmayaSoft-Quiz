using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Quiz
{
    public class LoadingOverlay : MonoBehaviour
    {
        [Header("Loading Parameters")]
        [SerializeField] private float _loadTime;
        [SerializeField] private AnimationCurve _loadCurve;
        
        [Header("Scene References")]
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroupTransition _canvasGroupTransition;

        [Header("Callbacks")]
        [SerializeField, Space(5)] private UnityEvent loaded;
        
        public void Show()
        {
            _canvasGroupTransition.FadeIn();
            
            DOVirtual.Float(0.0f, 1.0f, _loadTime, UpdateImage).SetEase(_loadCurve).OnComplete(() => loaded?.Invoke());
        }

        private void UpdateImage(float value)
        {
            _image.fillAmount = value;
        }
    }
}