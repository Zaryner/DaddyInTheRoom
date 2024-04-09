using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float chase_radiuse;
    [SerializeField] float agre_radius;
    [SerializeField] float walk_radius;
    [SerializeField] Vector3 walk_point;
    [SerializeField] float walk_cooldown;
    [SerializeField] float walk_time;
    Entity my_entity;
    float walk_timer;
    float rest_timer;
    bool walk;
    void Start()
    {
        my_entity = GetComponent<Entity>();
        walk_point = transform.position;
        walk = true;
        rest_timer = 0;
        walk_timer = 0;
    }

    void Update()
    {
      
        
    }
}
