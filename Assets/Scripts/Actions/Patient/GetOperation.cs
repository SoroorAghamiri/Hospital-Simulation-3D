using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOperation : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("surgeries").RemoveResource();
        Debug.Log("patient target for operation is " + target);
        if (target == null)
        {
            return false;
        }
        GWorld.Instance.GetWorld().ModifyState("FreeSurgery", -1);

        return true;
    }

    public override bool PostPerform()
    {
        // beliefs.RemoveState("doTheSurgery");

        GWorld.Instance.GetQueue("patientsWaitingForSurgery").AddResource(this.gameObject);

        // inventory.RemoveItem(target);
        return true;
    }
}
