using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ButtonInput : MonoBehaviour
{
    [SerializeField] private EventTrigger _button;
    private bool _pressed;

    public bool Pressed => _pressed;
    public UnityAction OnButtonDown;
    public UnityAction OnButtonUp;

    private void ButtonDown(PointerEventData data)
    {
        _pressed = true;
        OnButtonDown?.Invoke();
    }

    private void ButtonUp(PointerEventData data)
    {
        _pressed = false;
        OnButtonUp?.Invoke();
    }

    private void Awake()
    {
        EventTrigger.Entry Down = new EventTrigger.Entry();
        Down.eventID = EventTriggerType.PointerDown;
        Down.callback.AddListener((data) => { ButtonDown((PointerEventData)data); });
        _button.triggers.Add(Down);
        EventTrigger.Entry Up = new EventTrigger.Entry();
        Up.eventID = EventTriggerType.PointerUp;
        Up.callback.AddListener((data) => { ButtonUp((PointerEventData)data); });
        _button.triggers.Add(Up);
    }
}
