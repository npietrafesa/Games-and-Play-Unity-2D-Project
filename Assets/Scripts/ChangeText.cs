using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text myText;

    private void Start()
    {
        myText = GetComponentInChildren<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        myText.color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myText.color = Color.white;
    }
}