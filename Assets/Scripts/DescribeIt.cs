using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescribeIt : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject description;
    public static GameObject descrObj;
    [SerializeField] Transform parrent;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (descrObj != null)
        {
            Destroy(descrObj);
            descrObj = null;
        }
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
