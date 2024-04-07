using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int hp;
    [SerializeField] private int maxMp;
    [SerializeField] private int mp;
    [SerializeField] private float attSpd;
    [SerializeField] private int damage;
    private Entity target;
    private bool isAttacking;
    [SerializeField] private Weapon weapon = null;



    void Update()
    {

    }

    public void TakeDamage(int d)
    {
        hp -= d;
        if (hp < 0) hp = 0;
    }

    public void StartAttack()
    {
        if (isAttacking) return;
        isAttacking = true;
        StartCoroutine(Attack());
    }
    public void StartAttack(Target t)
    {
        SetTarget(t);
        if (isAttacking) return;
        isAttacking = true;
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        int totalDamage = damage;
        if (weapon != null) totalDamage += weapon.GetDamage();
        target.TakeDamage(totalDamage);
        float totalAttSpd = attSpd;
        if (weapon != null) totalAttSpd += weapon.GetAttSpeed();

        yield return new WaitForSeconds(totalAttSpd);

        if (isAttacking)
            StartCoroutine(Attack());
    }
    public void StopAttack()
    {
        isAttacking = false;
    }
    public void SetTarget(Entity e)
    {
        target = e;
    }
    public void SetTarget(Target t)
    {
        target = t.GetEntity();
    }
    public int GetHp() { return hp; }
    public int GetMp() { return mp; }
    public int GetMaxHp() { return maxHp; }
    public int GetMaxMp() { return maxMp; }
    public float GetAttSpd()
    {
        return attSpd;
    }
    public Weapon GetWeapon() { return weapon; }
}