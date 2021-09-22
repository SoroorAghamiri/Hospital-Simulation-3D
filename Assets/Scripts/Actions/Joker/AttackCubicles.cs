using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCubicles : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("cubicles").RemoveResource();
        if (target == null)
        {
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetQueue("cubicles").RemoveResource(target);
        Destroy(target.gameObject);
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", -1);
        return true;
    }
}
