using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSurgeryWR : GAction
{
    GameObject officeResource;
    GameObject surgeryResource;
    public override bool PrePerform()
    {
        officeResource = inventory.FindItemWithTag("Office");
        if (officeResource)
        {
            inventory.RemoveItem(officeResource);
        }
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.RemoveState("needSurgery");
        GWorld.Instance.GetWorld().ModifyState("CallingForSurgeon", 1);
        return true;
    }
}
