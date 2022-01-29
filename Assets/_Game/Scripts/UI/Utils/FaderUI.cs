using System;
using DG.Tweening;
using MEC;
using UnityEngine;

namespace _Game.UI.Utils
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FaderUI : MonoBehaviour
    {
        [SerializeField] protected float showDuration = 0.25f;
        [SerializeField] protected float hideDuration = 0.25f;
        
        protected CanvasGroup canvasGroup;
        
        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ShowDelay(float delay) => Timing.CallDelayed(delay, Show);
        public void HideDelay(float delay) => Timing.CallDelayed(delay, Hide);

        public void InstantShow()
        {
            OnShow();
            canvasGroup.alpha = 1f;
        }

        public void InstantHide()
        {
            OnHide();
            canvasGroup.alpha = 0f;
        }
        
        public void Show()
        {
            OnShow();
            canvasGroup.DOFade(1f, showDuration);
        }

        public void Hide()
        {
            OnHide();
            canvasGroup.DOFade(0f, hideDuration);
        }

        protected virtual void OnShow()
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        protected virtual void OnHide()
        {
            canvasGroup.DOKill();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}