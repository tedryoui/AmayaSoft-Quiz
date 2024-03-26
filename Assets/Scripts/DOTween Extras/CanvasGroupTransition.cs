using DG.Tweening;
using UnityEngine;

namespace Quiz
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupTransition : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Ease _inEase;
        [SerializeField] private Ease _outEase;
        
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeIn()
        {
            DOTween.Kill(gameObject.GetInstanceID().ToString(), true);

            DisableCanvasGroup();
            _canvasGroup.blocksRaycasts = true;
            
            var tween = _canvasGroup.DOFade(1.0f, _duration).SetEase(_inEase).OnComplete(EnableCanvasGroup);

            tween.SetId(gameObject.GetInstanceID().ToString());
            tween.Play();
        }

        public void FadeOut()
        {
            DOTween.Kill(gameObject.GetInstanceID().ToString(), true);

            EnableCanvasGroup();
            _canvasGroup.interactable = false;
            
            var tween = _canvasGroup.DOFade(0.0f, _duration).SetEase(_outEase).OnComplete(DisableCanvasGroup);

            tween.SetId(gameObject.GetInstanceID().ToString());
            tween.Play();
        }

        private void EnableCanvasGroup()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            _canvasGroup.alpha = 1.0f;
        }
        
        private void DisableCanvasGroup()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            _canvasGroup.alpha = 0.0f;
        }
    }
}