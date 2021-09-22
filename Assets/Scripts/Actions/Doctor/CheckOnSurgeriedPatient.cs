using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOnSurgeriedPatient : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("patientsAtRecovery").RemoveResource();
        if (target == null)
        {
            return false;
        }

        GWorld.Instance.GetWorld().ModifyState("RecoveryCall", -1);

        return true;
    }

    public override bool PostPerform()
    {

        if (target)
        {
            target.GetComponent<GAgent>().beliefs.ModifyState("isCured", 1);
            target.GetComponent<GAgent>().beliefs.RemoveState("needRecovery");
        }
        GWorld.Instance.GetWorld().ModifyState("FreeBed", 1);

        return true;
    }
}
