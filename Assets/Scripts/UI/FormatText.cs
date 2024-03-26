using TMPro;
using UnityEngine;

namespace Quiz
{
    public class FormatText : MonoBehaviour
    {
        [SerializeField] private string _format;
        [SerializeField] private TextMeshProUGUI _text;
        
        public void SetText(string value)
        {
            _text.SetText(string.Format(_format, value));
        }
    }
}