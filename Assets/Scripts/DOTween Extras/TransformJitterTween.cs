using DG.Tweening;
using UnityEngine;

namespace Quiz.DOTween_Extras
{
    public class TransformJitterTween : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        [SerializeField] private Vector3 _jitterPower;

        public void Jitter()
        {
            DOTween.Kill(gameObject.GetInstanceID().ToString(), true);

            var tween = transform.DOPunchPosition(_jitterPower, _duration).SetEase(_ease);

            tween.SetId(gameObject.GetInstanceID().ToString());
            tween.Play();
        }
    }
}