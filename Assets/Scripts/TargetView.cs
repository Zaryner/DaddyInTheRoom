using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetView : MonoBehaviour
{
    protected Target target = null;

    [SerializeField] protected GameObject content;
    [SerializeField] protected GameObject icon;
    [SerializeField] protected TMPro.TMP_Text nameField;
    [SerializeField] protected Bar hp;
    [SerializeField] protected Bar mp;

    protected Target subtarget;
    [SerializeField] protected GameObject subtargetContent;
    [SerializeField] protected TMPro.TMP_Text subtargetName;
    [SerializeField] protected Bar subtargetHp;
    [SerializeField] protected Bar subtargetMp;

    public void UpdateTarget(Target t)
    {
        if (t != null)
        {
            target = t;

            content.SetActive(true);
            Destroy(icon);
            icon = Instantiate(target.GetIcon(), content.transform);
            if (subtargetContent != null)
            {
                if (target.GetTarget() != null)
                {
                    subtarget = target.GetTarget();
                    subtargetContent.SetActive(true);
                    ChangeSubtargetValues();
                }
                else
                {
                    subtarget = null;
                    subtargetContent.SetActive(false);
                }
            }
            ChangeValues();
        }
        else
        {
            content.SetActive(false);
        }
    }
    protected void Update()
    {
        if (target != null)
        {
            subtarget = target.GetTarget();
            if (subtargetContent != null)
            {
                if (subtarget != null)
                    subtargetContent.SetActive(true);
                else
                    subtargetContent.SetActive(false);
            }
        }
        if (target != null) ChangeValues();
        if (subtarget != null && subtargetContent != null) ChangeSubtargetValues();
    }
    protected virtual void ChangeValues()
    {
        nameField.text = target.GetName();

        hp.ChangeMaxValueTo(target.GetMaxHp());
        hp.ChangeValueTo(target.GetHp());

        mp.ChangeMaxValueTo(target.GetMaxMp());
        mp.ChangeValueTo(target.GetMp());
    }
    protected virtual void ChangeSubtargetValues()
    {
        subtargetName.text = subtarget.GetName();

        subtargetHp.ChangeMaxValueTo(subtarget.GetMaxHp());
        subtargetHp.ChangeValueTo(subtarget.GetHp());

        subtargetMp.ChangeMaxValueTo(subtarget.GetMaxMp());
        subtargetMp.ChangeValueTo(subtarget.GetMp());
    }
}
