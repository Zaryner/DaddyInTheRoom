using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    private List<Target> stayOn = new List<Target>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DialogueArea>() && collision.GetComponentInParent<Target>())
        {
            stayOn.Add(collision.GetComponentInParent<Target>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DialogueArea>() && collision.GetComponentInParent<Target>())
        {
            stayOn.Remove(collision.GetComponentInParent<Target>());
        }
    }
    public bool StayOnTarget(Target target)
    {
        for (int i = 0; i < stayOn.Count; i++)
        {
            if (target != null && target.GetName() == stayOn[i].GetName())
            {
                return true;
            }
        }
        return false;
    }
}
