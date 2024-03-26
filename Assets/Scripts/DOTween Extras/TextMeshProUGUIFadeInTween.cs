using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Quiz.DOTween_Extras
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProUGUIFadeInTween : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void FadeIn()
        {
            DOTween.Kill(gameObject.GetInstanceID().ToString(), true);
            
            ResetTextAlpha();
            
            var tween = _text.DOFade(1.0f, _duration).SetEase(_ease);

            tween.SetId(gameObject.GetInstanceID().ToString());
            tween.Play();
        }

        private void ResetTextAlpha()
        {
            var color = _text.color;
            color.a = 0.0f;

            _text.color = color;
        }
    }
}