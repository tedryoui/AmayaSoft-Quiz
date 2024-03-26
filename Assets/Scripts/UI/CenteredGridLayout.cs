using System;
using System.Collections.Generic;
using Quiz.Extensions;
using UnityEngine;

namespace Quiz
{
    public class CenteredGridLayout : MonoBehaviour
    {
        [Header("Object References")]
        [SerializeField] private RectTransform _prefab;

        [SerializeField] private Transform _holder;

        [Header("Grid Defaults")]
        [SerializeField] private Vector2 _paddings;

        [SerializeField] private float _spaceBetween;

        private Vector2Int _sizes;

        private Vector2 Sizes => new(
                x: FrameSizes.x * _sizes.x + _paddings.x * 2 + _spaceBetween * (_sizes.x - 1),
                y: FrameSizes.y * _sizes.y + _paddings.y * 2 + _spaceBetween * (_sizes.y - 1)
            );

        private Vector2 BasePosition
        {
            get
            {
                float x, y;

                if (_sizes.x % 2 == 0)
                    x = (_sizes.x) / 2.0f * FrameSizes.x - FrameSizes.x / 2.0f + _spaceBetween *
                        (_sizes.x - 1) / 2.0f;
                else 
                    x = (_sizes.x - 1) / 2.0f * FrameSizes.x + _spaceBetween * (_sizes.x - 1) / 2.0f;

                if (_sizes.y % 2 == 0)
                    y = (_sizes.y) / 2.0f * FrameSizes.y - FrameSizes.y / 2.0f + _spaceBetween * (_sizes.y - 1) / 2.0f;
                else
                    y = (_sizes.y - 1) / 2.0f * FrameSizes.y + _spaceBetween * (_sizes.y - 1) / 2.0f;

                return new Vector2(-x, -y);
            }
        }
        
        private Vector2 FrameSizes
        {
            get
            {
                var rect = _prefab.rect;
                
                return new Vector2(rect.width, rect.height);
            }
        }

        private void Awake()
        {
            Build(0, 0);
        }

        public IEnumerable<GameObject> Build(int width, int height)
        {
            _holder.ClearChildren();

            _sizes = new Vector2Int(width, height);
            
            for (int x = 0; x < _sizes.x; x++)
                for (int y = 0; y < _sizes.y; y++)
                    yield return CreateElement(x, y);
            
            UpdateGridSizes();
        }

        private void UpdateGridSizes()
        {
            (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Sizes.x);
            (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Sizes.y);
        }
        
        private GameObject CreateElement(int x, int y)
        {
            var basePosition = BasePosition;
            var offset = new Vector2(FrameSizes.x * x + _spaceBetween * x, FrameSizes.y * y + _spaceBetween * y);

            var rectTransform = Instantiate(_prefab, _holder);
            rectTransform.transform.localPosition = basePosition + offset;

            return rectTransform.gameObject;
        }
    }
}