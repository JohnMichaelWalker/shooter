using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class FullyReactiveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private TextMeshProUGUI txt;
    private Button btn;

    public Color normalColor;
    public Color disabledColor;
    public Color pressedColor;
    public Color highlightedColor;

    void Start()
    {
        txt = GetComponentInChildren<TextMeshProUGUI>();
        btn = gameObject.GetComponent<Button>();
    }

    private ButtonStatus lastButtonStatus = ButtonStatus.Normal;
    private bool isHighlightDesired = false;
    private bool isPressedDesired = false;

    void Update()
    {
        ButtonStatus desiredButtonStatus = ButtonStatus.Normal;
        if (!btn.interactable)
            desiredButtonStatus = ButtonStatus.Disabled;
        else
        {
            if (isHighlightDesired)
                desiredButtonStatus = ButtonStatus.Highlighted;
            if (isPressedDesired)
                desiredButtonStatus = ButtonStatus.Pressed;
        }

        if (desiredButtonStatus != this.lastButtonStatus)
        {
            this.lastButtonStatus = desiredButtonStatus;
            switch (this.lastButtonStatus)
            {
                case ButtonStatus.Normal:
                    txt.color = normalColor;
                    break;
                case ButtonStatus.Disabled:
                    txt.color = disabledColor;
                    break;
                case ButtonStatus.Pressed:
                    txt.color = pressedColor;
                    break;
                case ButtonStatus.Highlighted:
                    txt.color = highlightedColor;
                    break;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHighlightDesired = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressedDesired = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressedDesired = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHighlightDesired = false;
    }

    public enum ButtonStatus
    {
        Normal,
        Disabled,
        Highlighted,
        Pressed
    }
}