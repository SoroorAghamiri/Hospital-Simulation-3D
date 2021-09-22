using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToRecovery : GAction
{
    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Bed");
        if (target == null)
        {
            return false;
        }
        GWorld.Instance.GetWorld().ModifyState("FreeSurgery", 1);

        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetQueue("patientsAtRecovery").AddResource(this.gameObject);
        GWorld.Instance.GetWorld().ModifyState("RecoveryCall", 1);
        return true;
    }
}
