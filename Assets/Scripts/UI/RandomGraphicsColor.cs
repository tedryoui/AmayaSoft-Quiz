using System;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    [RequireComponent(typeof(Graphic))]
    public class RandomGraphicsColor : MonoBehaviour
    {
        private Graphic _graphics;

        [SerializeField] private Color[] _colorVariants;

        private void Awake()
        {
            _graphics = GetComponent<Graphic>();
        }

        private void Start()
        {
            Randomize();
        }

        private void Randomize()
        {
            UnityEngine.Random.InitState(gameObject.GetInstanceID());

            var color = _colorVariants[UnityEngine.Random.Range(0, _colorVariants.Length)];
            _graphics.color = color;
            
            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        }
    }
}