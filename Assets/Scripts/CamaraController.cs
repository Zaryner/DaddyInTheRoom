using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamaraController : MonoBehaviour
{
    Camera cam;
    [SerializeField] float scrollSpeed;
    [SerializeField] float maxScroll;
    [SerializeField] float minScroll;

    [SerializeField] float speed;
    [SerializeField] float moveDistance;

    [SerializeField] GameObject target;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if ((scrollSpeed * Input.mouseScrollDelta.y * Time.deltaTime < 0 && cam.orthographicSize > minScroll) ||
                    (scrollSpeed * Input.mouseScrollDelta.y * Time.deltaTime > 0 && cam.orthographicSize < maxScroll))
                cam.orthographicSize += scrollSpeed * Input.mouseScrollDelta.y * Time.deltaTime;
        }
        Vector3 pos = target.transform.position;
        pos.z = transform.position.z;
        if (Vector3.Distance(pos, transform.position) >= moveDistance)
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            cam.orthographicSize = 5;
        }
    }
}
