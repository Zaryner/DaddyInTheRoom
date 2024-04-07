using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescribeItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject item;
    [SerializeField] GameObject description;
    public static GameObject descrObj;
    [SerializeField] Transform parrent;

    private void Awake()
    {
        EventManager.OnInventoryHide.AddListener(End);
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Destroy(descrObj);
        descrObj = Instantiate(description, new Vector3(10000, 10000, description.transform.position.z), description.transform.rotation, parrent);
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Destroy(descrObj);
    }
    private void End()
    {
        Destroy(descrObj);
    }
    
}
