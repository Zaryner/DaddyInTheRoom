using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate long GetLongDelegate();
public delegate void VoidDelegate();

public class Inventory : MonoBehaviour
{
    private long money;
    [SerializeField] List<Item> items;
    [SerializeField] int capacity;

    private event VoidDelegate onMoneyChange;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            AddMoney(2);
        }
    }

    public void OnMoneyChangeAddListener(VoidDelegate f)
    {
        onMoneyChange += f;
    }
    public void OnMoneyChangeRemoveListener(VoidDelegate f)
    {
        onMoneyChange -= f;
    }
    public long GetMoney()
    {
        return money;
    }
    public void AddMoney(long m)
    {
        money += m;
        onMoneyChange();
    }
}
