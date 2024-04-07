using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Item : MonoBehaviour, IAmItem
{
    [SerializeField] protected GameObject description;
    [SerializeField] protected GameObject item;
    [SerializeField] protected GameObject bag;
    [SerializeField] protected string name;
    [SerializeField] protected string descriptionText;
    [SerializeField] Transform parrent;
    GameObject desc;

    public virtual string GetDescription()
    {
        return descriptionText;
    }

    public virtual string GetName()
    {
        return name;
    }

    public GameObject GetUIDescriptionPrefab()
    {
        return description;
    }

    public GameObject GetUIPrefab()
    {
        return item;
    }

    public GameObject GetWorldPrefab()
    {
        return bag;
    }


    void Start()
    {

    }

    void Update()
    {

    }
}
