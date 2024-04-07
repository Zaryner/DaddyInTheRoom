
using UnityEngine;

interface IAmItem
{
    string GetName();
    string GetDescription();
    GameObject GetWorldPrefab();
    GameObject GetUIPrefab();
    GameObject GetUIDescriptionPrefab();

}
public enum EquipmentType { weapon = 0, chestplate = 1, helm = 2, ring = 3 };
interface IAmEquipment : IAmItem
{
    EquipmentType GetEquipmentType();

}
interface IAmWeapon : IAmEquipment
{
    int GetDamage();
    float GetAttSpeed();
    void Shot();

}
