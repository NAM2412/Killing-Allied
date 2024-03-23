using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform thumbStickTrans;
    [SerializeField] RectTransform backgroundTrans;
    [SerializeField] RectTransform centerTrans;

    bool wasDragging;

    public delegate void OnStickInputValueUpdate(Vector2 inputVal);
    public delegate void OnStickTaped();

    public event OnStickInputValueUpdate OnStickValueUpdated;
    public event OnStickTaped onStickTaped;
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log($"On Drag: {eventData.position}");
        Vector2 touchPos = eventData.position;
        Vector2 centerPos = backgroundTrans.position;

        Vector2 loccalOffset = Vector2.ClampMagnitude(touchPos - centerPos, backgroundTrans.sizeDelta.x/4);

        Vector2 inputVal = loccalOffset / (backgroundTrans.sizeDelta.x / 4);

        thumbStickTrans.position = centerPos + loccalOffset;

        OnStickValueUpdated?.Invoke(inputVal);

        wasDragging = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        backgroundTrans.position = eventData.position;
        thumbStickTrans.position = eventData.position;

        wasDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        backgroundTrans.position = centerTrans.position;
        thumbStickTrans.position = backgroundTrans.position;

        OnStickValueUpdated?.Invoke(Vector2.zero);

        if (!wasDragging)
        {
            onStickTaped?.Invoke();
        }
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
