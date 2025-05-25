using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickIdleHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text introduce;
    public string tooltipContent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        introduce.text = tooltipContent;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        introduce.text = "";
    }
}