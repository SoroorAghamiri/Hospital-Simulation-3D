using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClearGrenadesUp : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("weapons").RemoveResource();
        if (target == null)
        {
            return false;
        }
        inventory.AddItem(target);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetQueue("weapons").RemoveResource(target);
        inventory.RemoveItem(target);
        GWorld.Instance.GetWorld().ModifyState("FreeWeapon", -1);
        Destroy(target.gameObject);

        return true;
    }
}
