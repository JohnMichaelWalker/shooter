using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextButton : Button
{

    Text text;

    protected override void Awake()
    {
        text = GetComponentInChildren<Text>();
        text.color = colors.normalColor;
        base.Awake();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        text.color = colors.highlightedColor;
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        text.color = colors.normalColor;
        base.OnPointerExit(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        text.color = colors.pressedColor;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        text.color = colors.normalColor;
        base.OnPointerUp(eventData);
    }
}