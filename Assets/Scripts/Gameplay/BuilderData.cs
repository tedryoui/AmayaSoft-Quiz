using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Quiz
{
    [Serializable]
    public class Card
    {
        public Sprite sprite;

        public Vector3 rotation;
    }
    
    [CreateAssetMenu(menuName = "Quiz/Builder Data", fileName = "Quiz Builder Data", order = 0)]
    public class BuilderData : ScriptableObject
    {
        [SerializeField] private Vector2Int _sizes;

        [SerializeField] private Card[] _cardsBundle;

        public int Width => _sizes.x;
        public int Height => _sizes.y;

        public Card[] CardsBundle => _cardsBundle;
    }
}
