using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TranslateToMouse : MonoBehaviour
{
    [SerializeField] float offsetX;
    [SerializeField] float offsetY;
    [SerializeField] bool changeXPosRelativeToMouse;
    Canvas canvas;
    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
    }
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = transform.position.z;
        pos.x -= offsetX * canvas.scaleFactor;
        if (changeXPosRelativeToMouse)
        {
            if (Input.mousePosition.x < Screen.width / 2f)
            {
                pos.x += 2 * offsetX *  canvas.scaleFactor; ;
            }
        }
        pos.y -= offsetY *  canvas.scaleFactor; ;
        transform.position = pos;
    }



}
