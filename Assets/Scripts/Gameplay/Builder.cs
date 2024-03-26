using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Quiz
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] private CenteredGridLayout _gridLayout;

        [Header("Callbacks")]
        [SerializeField, Space(5)] private UnityEvent<Builder> builded;
        [SerializeField, Space(5)] private UnityEvent<GameplayButton> rightClicked;
        [SerializeField, Space(5)] private UnityEvent<GameplayButton> wrongClicked;
        [SerializeField, Space(5)] private UnityEvent dataRunOut;
        
        private Card _rightCard;
        private GameplayButton _rightCardButton;
        private GameplayButton[] _actualElements;
        private HashSet<Card> _usedCards;
        private Queue<BuilderData> _datesQueue;

        private BuilderData CurrentData => _datesQueue.Peek();

        public Card RightCard => _rightCard;
        public GameplayButton[] ActualElements => _actualElements;

        private void Awake()
        {
            _usedCards = new HashSet<Card>();
        }

        public void SetData(BuilderData[] data)
        {
            _datesQueue = new Queue<BuilderData>(data);
        }

        public void Build()
        {
            if (_datesQueue.Count == 0)
            {
                OnDataRunOut();
                return;
            }
            
            var rightButtonIndex = UnityEngine.Random.Range(0, CurrentData.Width * CurrentData.Height);
            var cardsInScene = new HashSet<Card>();
            
            _actualElements = _gridLayout.Build(CurrentData.Width, CurrentData.Height).Select(x => x.GetComponent<GameplayButton>()).ToArray();

            for (var i = 0; i < _actualElements.Length; i++)
            {
                var card = GetRandomCardWithUsedException(cardsInScene);
                var quizButton = _actualElements[i];

                if (card == null)
                {
                    return;
                }
                
                if (i == rightButtonIndex)
                {
                    _rightCard = card;
                    _rightCardButton = quizButton;
                }
                
                FillQuizButton(quizButton, card);
                cardsInScene.Add(card);
            }
            
            OnBuilded();
            
            _usedCards.Add(_rightCard);
            
            _datesQueue.Dequeue();
        }

        private void FillQuizButton(GameplayButton gameplayButton, Card card)
        {
            gameplayButton.SetImage(card.sprite, card.rotation);
            gameplayButton.SetClickCallback(gameplayButton.Equals(_rightCardButton) ? OnRightClicked : OnWrongClicked);
        }

        private Card GetRandomCardWithUsedException(HashSet<Card> cardsInScene)
        {
            var cards = CurrentData.CardsBundle.Except(_usedCards).Except(cardsInScene).ToArray();
            
            if (cards.Length == 0)
            {
#if UNITY_EDITOR
                if (EditorApplication.isPlaying)
                {
                    EditorApplication.isPlaying = false;
                    return null;
                }
#else
                if (Application.isPlaying) {
                    Application.Quit();
                    return null;
                }
#endif
            }
            
            var index = UnityEngine.Random.Range(0, cards.Length);
            var card = cards[index];

            return card;
        }

        private void OnBuilded()
        {
            builded?.Invoke(this);
        }

        private void OnRightClicked(GameplayButton gameplayButton)
        {
            rightClicked?.Invoke(gameplayButton);
        }

        private void OnWrongClicked(GameplayButton gameplayButton)
        {
            wrongClicked.Invoke(gameplayButton);
        }

        private void OnDataRunOut()
        {
            dataRunOut?.Invoke();
        }
    }
}