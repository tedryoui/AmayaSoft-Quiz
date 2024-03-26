using System;
using Quiz.DOTween_Extras;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    public class GameplayButton : MonoBehaviour
    {
        [Header("Scene References")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        [Header("Particles")]
        [SerializeField] private ParticleSystem _particleSystem;

        [Header("Tween References")]
        [SerializeField] private TransformBounceTween _bounceTween;
        [SerializeField] private TransformJitterTween _jitterTween;

        public TransformBounceTween BounceTween => _bounceTween;
        public TransformJitterTween JitterTween => _jitterTween;
        
        public void SetClickCallback(Action<GameplayButton> callback)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => callback?.Invoke(this));
        }
        
        public void SetImage(Sprite sprite, Vector3 eulerRotation)
        {
            _image.sprite = sprite;
            _image.transform.rotation = Quaternion.Euler(eulerRotation);
        }

        public void Shine()
        {
            _particleSystem.transform.parent = null;
            _particleSystem.transform.position = (Vector2)transform.position;
            _particleSystem.transform.localScale = Vector3.one;
            _particleSystem.Play();
        }
    }
}