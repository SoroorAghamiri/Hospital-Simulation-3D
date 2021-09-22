using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOffices : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("offices").RemoveResource();
        if (target == null)
        {
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetQueue("offices").RemoveResource(target);
        Destroy(target.gameObject);
        GWorld.Instance.GetWorld().ModifyState("FreeOffice", -1);
        return true;
    }
}
