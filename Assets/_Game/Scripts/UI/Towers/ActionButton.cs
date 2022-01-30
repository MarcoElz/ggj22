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
        
        public event Action<AbstractAction> onEntered;
        public event Action onExited;
        public event Action<AbstractAction> onPressed;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(PressAction);
        }

        public virtual void OnShow(AbstractTower tower)
        { }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            onEntered?.Invoke(action);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            onExited?.Invoke();
        }

        protected virtual void PressAction()
        {
            onPressed?.Invoke(action);
        }
    }
}
