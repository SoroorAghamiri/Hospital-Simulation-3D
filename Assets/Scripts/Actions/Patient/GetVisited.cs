using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVisited : GAction
{
    // GameObject doctor;
    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Office");

        if (target == null)
        {
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {

        GWorld.Instance.GetQueue("patientsWaitingForDoctor").AddResource(this.gameObject);
        return true;
    }
}
