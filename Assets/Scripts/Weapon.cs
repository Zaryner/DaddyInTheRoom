
using UnityEngine;

[System.Serializable]
public class Weapon : Item, IAmWeapon
{
    [SerializeField] protected float AttSpeed;
    [SerializeField] protected int damage;
    [SerializeField] protected float range;
    protected EquipmentType type=EquipmentType.weapon;


    void Start()
    {

    }

    void Update()
    {

    }
    public virtual void Shot()
    {

    }
    public virtual int GetDamage()
    {
        return damage;
    }

    override public string GetDescription()
    {
        return descriptionText;
    }

    override public string GetName()
    {
        return name;
    }

    public virtual float GetAttSpeed()
    {
        return AttSpeed;
    }

    public virtual EquipmentType GetEquipmentType()
    {
        return type;
    }
    public virtual float GetRange()
    {
        return range;
    }

    GameObject IAmItem.GetWorldPrefab()
    {
        throw new System.NotImplementedException();
    }

    GameObject IAmItem.GetUIPrefab()
    {
        throw new System.NotImplementedException();
    }

    GameObject IAmItem.GetUIDescriptionPrefab()
    {
        throw new System.NotImplementedException();
    }
}
