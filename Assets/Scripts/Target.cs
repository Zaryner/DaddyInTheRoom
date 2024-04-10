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

    public void TakeDamage(int d,Target source)
    {
        me.TakeDamage(d);
        if (me.GetHp() <= 0 && type == TargetType.mob)
        {
            source.AddExp((int)(GetExp()*0.25));

            source.target = null;
            if(source.type == TargetType.player)
            {
                GameObject.FindWithTag("TargetView").GetComponent<TargetView>().UpdateTarget(null);
            }
            Destroy(this.gameObject);
        }
    }

    public void UnderAttack(Target enemy)
    {
        if(type == TargetType.mob)
        {
            var enemy_script = GetComponent<Enemy>();
            if(enemy_script != null)
            {
                enemy_script.StartAttack(enemy);
            }
        }
    }
    public void AddExp(int exp)
    {
        if (type == TargetType.player)
        {
            me.AddExp(exp);
            if (me.GetExp() >= me.GetMaxExp())
            {

            }
        }
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
    public int GetMaxExp()
    {
        return me.GetMaxExp();
    }
    public int GetExp()
    {
        return me.GetExp();
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
