using System;
using UnityEngine;

namespace Quiz
{
    [Serializable]
    public class BuilderDataVariant
    {
        [SerializeField] private BuilderData[] _variants;

        public BuilderData Variant
        {
            get
            {
                var index = UnityEngine.Random.Range(0, _variants.Length);
                return _variants[index];
            }
        }
    }
}