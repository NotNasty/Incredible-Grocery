using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace IncredibleGrocery.ClientLogic
{
    public class ClientProgressBar : MonoBehaviour
    {
        public event Action WaitingTimeEnded;
        
        [SerializeField] private Gradient colorGradient;
        
        private float _currentTime;
        private Image _progressImage;
        private int _maxWaitingTime;
        
        private void Start()
        {
            _progressImage = GetComponent<Image>();
            _maxWaitingTime = Random.Range(15, 21);
            _progressImage.color = SetColor(1);
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
            return colorGradient.Evaluate(fillAmount);
        }
    }
}
