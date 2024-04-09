using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Animator animator;

    private Target MeTarget;

    [SerializeField] GameObject skin;
    [SerializeField] private float skinOffset;

    private Target target;
    private TargetView targetView;
    private TargetView MeView;


    private Entity myEntity;

    [SerializeField] GameObject wentObjectPrefab;
    [SerializeField] GameObject wentInvisiblePrefab;
    private GameObject wentObject;
    private Target wentTarget;
    TargetFinder targetFinder;

    private bool facing = true;
    private bool weaponize = false;
    private float attTimer = 0;
    private bool isAttacking;

    private bool isGoingToAttackTarget = false;
    private Target attTarget;

    void Start()
    {
        targetFinder = GetComponentInChildren<TargetFinder>();
        myEntity = GetComponent<Entity>();
        animator = GetComponentInChildren<Animator>();
        target = null;
        targetView = GameObject.FindWithTag("TargetView").GetComponent<TargetView>();
        targetView.UpdateTarget(null);
        MeView = GameObject.FindWithTag("MeView").GetComponent<TargetView>();
        MeTarget = GetComponent<Target>();
        MeView.UpdateTarget(MeTarget);
    }

    void Update()
    {
        attTimer -= Time.deltaTime;
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"), 0).normalized;
        //if (Math.Abs(Input.GetAxis("Horizontal")) > 0.1f || Math.Abs(Input.GetAxis("Vertical")) > 0.1f)
            transform.Translate(moveDir * myEntity.Speed * Time.deltaTime);
        animator.SetFloat("Speed", Math.Abs(Input.GetAxis("Horizontal")) + Math.Abs(Input.GetAxis("Vertical")));
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            StopMovement();
            StopAttack();
        }
        else
        {
            if (wentObject != null)
            {
                if ((transform.position == Vector3.MoveTowards(transform.position, wentObject.transform.position, myEntity.Speed * Time.deltaTime)) || (targetFinder.StayOnTarget(wentTarget)))
                {
                    StopMovement();
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, wentObject.transform.position, myEntity.Speed * Time.deltaTime);
                    animator.SetFloat("Speed", 1);
                }
                if (isGoingToAttackTarget && Vector2.Distance(transform.position, wentObject.transform.position) <= myEntity.GetWeapon().GetRange() - 0.1f)
                {
                    StopMovement();
                    if (attTarget != null)
                        StartAttack(attTarget);
                }

            }
        }


        Flip();
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!weaponize)
            {
                animator.SetBool("Weaponize", true);
                weaponize = true;
            }
            else
            {
                animator.SetBool("Weaponize", false);
                weaponize = false;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            bool anyTarget = false;
            bool clickUI = false;
#if !UNITY_ANDROID
            if (EventSystem.current.IsPointerOverGameObject())
            {
                clickUI = true;
            }
#endif
#if UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    clickUI = true;
                }
            }
#endif
            if (!clickUI)
                CheckTargets(ref anyTarget);
            if (!anyTarget && !clickUI)
            {
                StopAttack();
                StopMovement();
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                wentObject = Instantiate(wentObjectPrefab, pos, Quaternion.identity);
            }

        }

    }

    private void Flip()
    {
        if ((facing && Input.GetAxis("Horizontal") < 0) ||
            (!facing && Input.GetAxis("Horizontal") > 0))
        {
            if (facing && Input.GetAxis("Horizontal") < 0)
                skin.transform.localPosition = new Vector3(skin.transform.localPosition.x + skinOffset, skin.transform.localPosition.y, skin.transform.localPosition.z);
            else if (!facing && Input.GetAxis("Horizontal") > 0)
                skin.transform.localPosition = new Vector3(skin.transform.localPosition.x - skinOffset, skin.transform.localPosition.y, skin.transform.localPosition.z);
            facing = !facing;
            skin.transform.localScale = new Vector3(-skin.transform.localScale.x, skin.transform.localScale.y, skin.transform.localScale.z);
        }
        if (wentObject != null)
        {
            if ((facing && (wentObject.transform.position.x - transform.position.x) < 0) ||
                (!facing && (wentObject.transform.position.x - transform.position.x) > 0))
            {
                if (facing && (wentObject.transform.position.x - transform.position.x) < 0)
                    skin.transform.localPosition = new Vector3(skin.transform.localPosition.x + skinOffset, skin.transform.localPosition.y, skin.transform.localPosition.z);
                else if (!facing && (wentObject.transform.position.x - transform.position.x) > 0)
                    skin.transform.localPosition = new Vector3(skin.transform.localPosition.x - skinOffset, skin.transform.localPosition.y, skin.transform.localPosition.z);
                facing = !facing;
                skin.transform.localScale = new Vector3(-skin.transform.localScale.x, skin.transform.localScale.y, skin.transform.localScale.z);
            }

        }
    }

    public void StopMovement()
    {
        if (wentObject != null)
        {
            Destroy(wentObject);
            wentObject = null;
        }
        wentTarget = null;
        isGoingToAttackTarget = false;
    }

    public void StartAttack(Target t)
    {
        if (isAttacking) return;
        if (!weaponize)
        {
            animator.SetBool("Weaponize", true);
            weaponize = true;
        }
        myEntity.StartAttack(t);
        isAttacking = true;
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        if(attTarget == null)
        {
            StopAttack();
            yield break;
        }
        string att = "Attack" + UnityEngine.Random.Range(1, 4);
        animator.SetTrigger(att);
        yield return new WaitForSeconds(myEntity.GetAttSpd() + myEntity.GetWeapon().GetAttSpeed());
        if (isAttacking)
            StartCoroutine(Attack());
    }
    public void StopAttack()
    {
        isAttacking = false;
        myEntity.StopAttack();
        attTarget = null;
        for (int i = 1; i <= 3; i++)
        {
            string att = "Attack" + i;
            animator.ResetTrigger(att);
        }
    }

    private void CheckTargets(ref bool anyTarget)
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.GetComponent<Target>())
            {
                if (target != hit[i].collider.gameObject.GetComponent<Target>())
                {
                    target = hit[i].collider.gameObject.GetComponent<Target>();
                    MeTarget.SetTarget(target);
                    targetView.UpdateTarget(target);
                }
                else
                {
                    switch (target.GetTargetType())
                    {
                        case Target.TargetType.npc:
                            if (!targetFinder.StayOnTarget(target))
                            {
                                StopAttack();
                                StopMovement();
                                wentTarget = target;
                                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                pos.z = 0;
                                wentObject = Instantiate(wentInvisiblePrefab, pos, Quaternion.identity);
                            }
                            break;
                        case Target.TargetType.mob:

                            if (Vector2.Distance(transform.position, target.transform.position) <= myEntity.GetWeapon().GetRange())
                            {
                                attTarget = target;
                                StartAttack(attTarget);
                            }
                            else
                            {
                                StopAttack();
                                StopMovement();
                                attTarget = target;
                                wentTarget = attTarget;
                                wentObject = Instantiate(wentInvisiblePrefab, wentTarget.transform.position, Quaternion.identity);
                                isGoingToAttackTarget = true;
                            }
                            break;

                    }

                }
                anyTarget = true;
            }
        }
    }

}
