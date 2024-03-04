using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"On Drag: {eventData.position}");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down!");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up!");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
