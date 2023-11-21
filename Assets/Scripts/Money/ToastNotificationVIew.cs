using DG.Tweening;
using UnityEngine;

namespace IncredibleGrocery.Money
{
    public class ToastNotificationVIew : MoneyView
    {
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
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, HidePositionY);
        }

        protected override void ChangeMoneyBalance(int moneyBalance, int moneyDif)
        {
            if (moneyDif == 0)
                return;
            
            SetMoneyBalance(string.Format(Constants.ToastNotificationFormat, moneyDif > 0 ? Plus : Minus, 
                Mathf.Abs(moneyDif)));
            PlayAnimation();
        }

        private void PlayAnimation()
        {
            var sequence = DOTween.Sequence();
            sequence.Append( _rectTransform.DOAnchorPosY(ShowPositionY, ShowAnimationDuration)
                    .SetEase(Ease.InOutExpo)).SetEase(Ease.InOutSine);
            sequence.Join(CanvasGroup.DOFade(ShowAlpha, ShowAnimationDuration));
            sequence.Append( _rectTransform.DOAnchorPosY(HidePositionY, HideAnimationDuration)
                .SetEase(Ease.InOutExpo)).SetEase(Ease.InOutSine);
            sequence.Join(CanvasGroup.DOFade(HideAlpha, HideAnimationDuration));
        }
    }
}
