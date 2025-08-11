using DG.Tweening;
using UnityEngine;


namespace Common.UI
{
    public class FadeUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration = 0.4f;

        // Öð½¥ÏÔÊ¾
        public void FadeIn()
        {
            canvasGroup.DOFade(1f, fadeDuration)
                .OnStart(() =>
                {
                    canvasGroup.blocksRaycasts = true;
                    gameObject.SetActive(true);
                });
        }

        // Öð½¥ÏûÊ§
        public void FadeOut()
        {
            canvasGroup.DOFade(0f, fadeDuration)
                .OnComplete(() =>
                {
                    canvasGroup.blocksRaycasts = false;
                    gameObject.SetActive(false);
                });
        }
    }
}