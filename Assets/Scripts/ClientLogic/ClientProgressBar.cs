using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace IncredibleGrocery.ClientLogic
{
    public class ClientProgressBar : MonoBehaviour
    {
        public event Action WaitingTimeEnded;
        
        [SerializeField] private Color happyColor;
        [SerializeField] private Color middleColor;
        [SerializeField] private Color sadColor;
        
        private float _currentTime;
        private Image _progressImage;
        private int _maxWaitingTime;
        
        private void Start()
        {
            _progressImage = GetComponent<Image>();
            _maxWaitingTime = Random.Range(10, 16);
            _progressImage.color = happyColor;
        }

        public void UpdateProgressBar()
        {
            _currentTime += Time.deltaTime;
            var currentProgress = (_maxWaitingTime - _currentTime) / _maxWaitingTime;
            _progressImage.fillAmount = currentProgress;
            _progressImage.color = SetColor(currentProgress);
            if (_progressImage.fillAmount <= 0)
            {
                WaitingTimeEnded?.Invoke();
            }
        }

        private Color SetColor(float fillAmount)
        {
            return fillAmount switch
            {
                < 0.33f => sadColor,
                < 0.66f and > 0.33f => middleColor,
                > 0.66f => happyColor,
                _ => sadColor
            };
        }
    }
}
