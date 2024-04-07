using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Target : MonoBehaviour
{

    public enum TargetType { player = 0, npc = 1, mob = 2 };

    [SerializeField] string name;
    [SerializeField] GameObject icon;
    [SerializeField] Target target;
    Entity me;
    [SerializeField] TargetType type;

    private void Awake()
    {
        me = GetComponent<Entity>();
    }

    public void TakeDamage(int d)
    {
        me.TakeDamage(d);
    }
    public TargetType GetTargetType()
    {
        return type;
    }
    public void SetTarget(Target t)
    {
        target = t;
    }
    public string GetName()
    {
        return name;
    }
    public int GetMaxHp()
    {
        return me.GetMaxHp();
    }
    public int GetHp()
    {
        return me.GetHp();
    }
    public int GetMaxMp()
    {
        return me.GetMaxMp();
    }
    public int GetMp()
    {
        return me.GetMp();
    }
    public GameObject GetIcon()
    {
        return icon;
    }
    public Target GetTarget()
    {
        return target;
    }
    public Entity GetEntity()
    {
        return me;
    }
}
