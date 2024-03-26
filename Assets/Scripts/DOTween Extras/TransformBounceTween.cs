using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz.DOTween_Extras
{
    [RequireComponent(typeof(Image))]
    public class TransformBounceTween : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        [SerializeField] private Vector2 _delayFromTo;

        public void Bounce()
        {
            DOTween.Kill(gameObject.GetInstanceID().ToString());

            ResetImageScale();

            var tween = transform.DOScale(1.0f, _duration).SetEase(_ease).SetDelay(UnityEngine.Random.Range(_delayFromTo.x, _delayFromTo.y));

            tween.SetId(gameObject.GetInstanceID().ToString());
            tween.Play();
        }

        private void ResetImageScale()
        {
            transform.localScale = Vector3.zero;
        }
    }
}