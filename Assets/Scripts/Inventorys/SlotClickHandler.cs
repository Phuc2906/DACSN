using UnityEngine;
using UnityEngine.EventSystems;

public class SlotClickHandler : MonoBehaviour, IPointerClickHandler
{
    public WeaponHotbar hotbar; 
    public int index;          

    public void OnPointerClick(PointerEventData eventData)
    {
        if (hotbar != null) hotbar.SelectSlot(index);
    }
}
