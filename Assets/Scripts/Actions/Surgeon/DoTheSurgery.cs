using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTheSurgery : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("patientsWaitingForSurgery").RemoveResource();
        if (target == null)
        {
            return false;
        }
        inventory.AddItem(target);
        return true;
    }

    public override bool PostPerform()
    {
        if (target)
        {
            target.GetComponent<GAgent>().beliefs.ModifyState("needRecovery", 1);
        }
        GWorld.Instance.GetWorld().ModifyState("CallingForSurgeon", -1);

        return true;
    }
}
