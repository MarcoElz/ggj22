using System;
using DG.Tweening;
using MEC;
using UnityEngine;

namespace _Game.UI.Utils
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FaderUI : MonoBehaviour
    {
        [Header("Fader Settings")]
        [SerializeField] protected float showDuration = 0.25f;
        [SerializeField] protected float hideDuration = 0.25f;
        [SerializeField] private bool hideOnAwake = true;
        
        protected CanvasGroup canvasGroup;
        protected bool isShowing;

        private bool originalBlockRaycast;
        private bool originalInteractable;
        
        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            originalBlockRaycast = canvasGroup.blocksRaycasts;
            originalInteractable = canvasGroup.interactable;
            
            if(hideOnAwake)
                InstantHide();
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

        public void Toggle()
        {
            if(isShowing)
                Hide();
            else
                Show();
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
            canvasGroup.interactable = originalInteractable;
            canvasGroup.blocksRaycasts = originalBlockRaycast;
            isShowing = true;
        }

        protected virtual void OnHide()
        {
            canvasGroup.DOKill();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            isShowing = false;
        }
    }
}