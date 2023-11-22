using DG.Tweening;
using UnityEngine;

namespace IncredibleGrocery.Money
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ToastNotificationVIew : MoneyView
    {
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        private const int HidePositionY = 110;
        private const int ShowPositionY = -110;
        private const int HideAlpha = 0;
        private const int ShowAlpha = 255;
        
        private const float HideAnimationDuration = 3f;
        private const float ShowAnimationDuration = 0.3f;

        private const char Plus = '+';
        private const char Minus = '-';

        public override void Init()
        {
            base.Init();
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, HidePositionY);
        }
        
        private void OnEnable()
        {
            MoneyManager.IncomeCame += ToastNotification;
        }

        private void ToastNotification(int moneyDif)
        {
            if (moneyDif == 0)
                return;
            
            SetMoneyText(string.Format(Constants.ToastNotificationFormat, moneyDif > 0 ? Plus : Minus, 
                Mathf.Abs(moneyDif)));
            PlayAnimation();
        }

        private void PlayAnimation()
        {
            DOTween.Sequence().Append( _rectTransform.DOAnchorPosY(ShowPositionY, ShowAnimationDuration)
                    .SetEase(Ease.InOutExpo)).SetEase(Ease.InOutSine)
                    .Join(_canvasGroup.DOFade(ShowAlpha, ShowAnimationDuration))
                    .Append( _rectTransform.DOAnchorPosY(HidePositionY, HideAnimationDuration)
                    .SetEase(Ease.InOutExpo)).SetEase(Ease.InOutSine)
                    .Join(_canvasGroup.DOFade(HideAlpha, HideAnimationDuration));
        }

        private void OnDisable()
        {
            MoneyManager.BalanceChanged -= ToastNotification;
        }
    }
}
