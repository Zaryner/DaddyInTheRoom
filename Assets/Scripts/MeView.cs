using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeView : TargetView
{
    [SerializeField] protected Bar exp;
    protected new void Update()
    {
        base.Update();
    }
    protected override void ChangeValues()
    {
        base.ChangeValues();
        exp.ChangeMaxValueTo(target.GetMaxExp());
        exp.ChangeValueTo(target.GetExp());

    }

}
