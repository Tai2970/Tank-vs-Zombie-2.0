using UnityEngine;
using UnityEngine.EventSystems;

// Enables arrow hover effects for UI buttons
public class ButtonHoverFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Arrow Objects")]
    public GameObject leftArrow;    // Arrow shown on the left of the button
    public GameObject rightArrow;   // Arrow shown on the right of the button

    // Called when the mouse enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
    }

    // Called when the mouse exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }
}
