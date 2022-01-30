using System;
using _Game.GameActions;
using _Game.Towers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Game.UI.Towers
{
    [RequireComponent(typeof(Button))]
    public class ActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected AbstractAction action = default;

        protected Button button;
        
        public Action<AbstractAction> onEntered;
        public Action onExited;
        public Action<AbstractAction> onPressed;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(PressAction);
        }

        public virtual void OnShow(AbstractTower tower)
        { }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onEntered?.Invoke(action);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onExited?.Invoke();
        }

        protected virtual void PressAction()
        {
            onPressed?.Invoke(action);
        }
    }
}
