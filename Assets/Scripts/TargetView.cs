using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetView : MonoBehaviour
{
    private Target target = null;

    [SerializeField] private GameObject content;
    [SerializeField] private GameObject icon;
    [SerializeField] private TMPro.TMP_Text nameField;
    [SerializeField] private Bar hp;
    [SerializeField] private Bar mp;

    private Target subtarget;
    [SerializeField] private GameObject subtargetContent;
    [SerializeField] private TMPro.TMP_Text subtargetName;
    [SerializeField] private Bar subtargetHp;
    [SerializeField] private Bar subtargetMp;

    public void UpdateTarget(Target t)
    {
        if (t != null)
        {
            target = t;

            content.SetActive(true);
            Destroy(icon);
            icon = Instantiate(target.GetIcon(), content.transform);
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
            ChangeValues();
        }
        else
        {
            content.SetActive(false);
        }
    }
    private void Update()
    {
        if (target != null)
        {
            subtarget = target.GetTarget();
            if (subtarget != null)
                subtargetContent.SetActive(true);
            else
                subtargetContent.SetActive(false);
        }
        if (target != null) ChangeValues();
        if (subtarget != null) ChangeSubtargetValues();
    }
    private void ChangeValues()
    {
        nameField.text = target.GetName();

        hp.ChangeMaxValueTo(target.GetMaxHp());
        hp.ChangeValueTo(target.GetHp());

        mp.ChangeMaxValueTo(target.GetMaxMp());
        mp.ChangeValueTo(target.GetMp());
    }
    private void ChangeSubtargetValues()
    {
        subtargetName.text = subtarget.GetName();

        subtargetHp.ChangeMaxValueTo(subtarget.GetMaxHp());
        subtargetHp.ChangeValueTo(subtarget.GetHp());

        subtargetMp.ChangeMaxValueTo(subtarget.GetMaxMp());
        subtargetMp.ChangeValueTo(subtarget.GetMp());
    }
}
