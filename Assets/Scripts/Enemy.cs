using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Enemy : MonoBehaviour
{
    [SerializeField] float chase_radiuse;
    [SerializeField] float agre_radius;
    [SerializeField] float walk_radius;
    [SerializeField] Vector3 home_point;
    [SerializeField] Vector3 walk_point;
    [SerializeField] float walk_cooldown;
    [SerializeField] float one_walk_radius;
    Entity my_entity;
    Target my_target;
    [SerializeField] float rest_timer;
    [SerializeField] bool walk;

    [SerializeField] GameObject skin;
    [SerializeField] private float skinOffset;
    [SerializeField] private bool facing;
    private bool isAttacking;

    private Animator animator;
    void Start()
    {
        my_entity = GetComponent<Entity>();
        my_target = GetComponent<Target>();
        CulculateWalkPoint();
        home_point = transform.position;
        walk = true;
        rest_timer = 0;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Flip();
        if (!walk)
        {
            rest_timer += Time.deltaTime;
            if (rest_timer >= walk_cooldown)
            {
                walk = true;
                
                CulculateWalkPoint();
            }
        }
        else
        {
            if (Vector3.Distance(transform.position,walk_point)<=0.01f)
            {
            
                walk = false;
                rest_timer = 0;
                animator.SetFloat("Speed", 0);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, walk_point, my_entity.Speed * Time.deltaTime);
                animator.SetFloat("Speed", 1);
            }
            //if (isGoingToAttackTarget && Vector2.Distance(transform.position, wentObject.transform.position) <= myEntity.GetWeapon().GetRange() - 0.1f)
            //{
            //    StopMovement();
            //    if (attTarget != null)
            //        StartAttack(attTarget);
            //}


        }
    }
    void CulculateWalkPoint()
    {
        var x_walk = transform.position.x + one_walk_radius * Random.Range(-1.0f, 1.0f);
        if (x_walk > home_point.x + walk_radius) x_walk = home_point.x + walk_radius;
        if (x_walk < home_point.x - walk_radius) x_walk = home_point.x - walk_radius;

        var y_walk = transform.position.y + one_walk_radius * Random.Range(-1.0f, 1.0f);
        if (y_walk > home_point.y + walk_radius) y_walk = home_point.y + walk_radius;
        if (y_walk < home_point.y - walk_radius) y_walk = home_point.y - walk_radius;

        walk_point = new Vector3(x_walk, y_walk, transform.position.z);
    }
    private void Flip()
    {
            if ((facing && (walk_point.x - transform.position.x) < 0) ||
                (!facing && (walk_point.x - transform.position.x) > 0))
            {
                if (facing && (walk_point.x - transform.position.x) < 0)
                    skin.transform.localPosition = new Vector3(skin.transform.localPosition.x + skinOffset, skin.transform.localPosition.y, skin.transform.localPosition.z);
                else if (!facing && (walk_point.x - transform.position.x) > 0)
                    skin.transform.localPosition = new Vector3(skin.transform.localPosition.x - skinOffset, skin.transform.localPosition.y, skin.transform.localPosition.z);
                facing = !facing;
                skin.transform.localScale = new Vector3(-skin.transform.localScale.x, skin.transform.localScale.y, skin.transform.localScale.z);
            }

        }
    public void StartAttack(Target t)
    {
        if (isAttacking) return;

        my_target.SetTarget(t);
        my_entity.StartAttack(t);
        isAttacking = true;
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        if (my_target.GetTarget() == null)
        {
            StopAttack();
            yield break;
        }
        //if (Vector2.Distance(transform.position, my_target.GetTarget().transform.position) > my_entity.GetWeapon().GetRange())
        //{
        //    isAttacking = false;
        //    my_entity.StopAttack();
        //    for (int i = 1; i <= 3; i++)
        //    {
        //        string s = "Attack" + i;
        //        animator.ResetTrigger(s);
        //    }

        //    wentTarget = attTarget;
        //    wentObject = Instantiate(wentInvisiblePrefab, wentTarget.transform.position, Quaternion.identity);
        //    isGoingToAttackTarget = true;

        //    yield break;
        //}
        string att = "Attack" + UnityEngine.Random.Range(1, 4);
        animator.SetTrigger(att);
        yield return new WaitForSeconds(my_entity.GetAttSpd() + my_entity.GetWeapon().GetAttSpeed());
        if (isAttacking)
            StartCoroutine(Attack());
    }
    public void StopAttack()
    {
        isAttacking = false;
        my_entity.StopAttack();
        my_target = null;
        for (int i = 1; i <= 3; i++)
        {
            string att = "Attack" + i;
            animator.ResetTrigger(att);
        }
    }
}
