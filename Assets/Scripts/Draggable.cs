using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private float offsetX;
    private float offsetY;
    private float zPos;
    [SerializeField] GameObject dragObj;
    [SerializeField] float addToZ = 0;
    [SerializeField] Transform newParrent;
    Transform lastParrent;

    private void Awake()
    {
        EventManager.OnInventoryHide.AddListener(Comeback);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Vector3 pos = Input.mousePosition;
        offsetX = pos.x - dragObj.transform.position.x;
        offsetY = pos.y - dragObj.transform.position.y;
        zPos = dragObj.transform.position.z;
        lastParrent = dragObj.transform.parent;
        if (newParrent != null)
            dragObj.transform.SetParent(newParrent);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Input.mousePosition;
        pos.z = zPos + addToZ;
        pos.x -= offsetX;
        pos.y -= offsetY;
        dragObj.transform.position = pos;

    }
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector3 pos = dragObj.transform.position;
        pos.z = zPos;
        dragObj.transform.position = pos;
        dragObj.transform.SetParent(lastParrent);
    }
    private void Comeback()
    {

        Vector3 pos = dragObj.transform.position;
        pos.z = zPos;
        dragObj.transform.position = pos;
        if (lastParrent != null)
            dragObj.transform.SetParent(lastParrent);
    }

}
