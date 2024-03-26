using System.Linq;
using System.Threading.Tasks;
using Quiz.DOTween_Extras;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Quiz
{
    public class Quiz : MonoBehaviour
    {
        [Header("Builder Presets")]
        [SerializeField] private BuilderDataVariant[] _presets;

        [Header("Scene References")]
        [SerializeField] private Builder builder;
        [SerializeField] private FormatText _title;
        
        [Header("Tween References")]
        [SerializeField] private TextMeshProUGUIFadeInTween _titleFadeInTween;
        [SerializeField] private CanvasGroupTransition _restartCanvasGroup;

        private int _afterRightClickedDelay = 1250;
        
        private void Start()
        {
            StartNewGame();
        }

        public void StartNewGame()
        {
            builder.SetData(_presets.Select(x => x.Variant).ToArray());
            builder.Build();
            
            _titleFadeInTween.FadeIn();
            foreach (var element in builder.ActualElements)
                element.BounceTween.Bounce();
        }

        public void OnBuilded(Builder builder)
        {
            _title.SetText(builder.RightCard.sprite.name);
        }
        
        public async void OnRightClicked(GameplayButton gameplayButton)
        {
            gameplayButton.Shine();

            await Task.Delay(_afterRightClickedDelay);
            
            builder.Build();
        }

        public void OnWrongClicked(GameplayButton gameplayButton)
        {
            gameplayButton.JitterTween.Jitter();
        }

        public void OnDataRunOut()
        {
            _restartCanvasGroup.FadeIn();
        }
    }
}