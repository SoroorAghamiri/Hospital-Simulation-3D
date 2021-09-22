using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToToilet : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("toilets").RemoveResource();
        if (target == null)
        {
            return false;
        }
        inventory.AddItem(target);
        GWorld.Instance.GetWorld().ModifyState("FreeToilet", -1);
        // Debug.Log("Going to toilet Started");

        return true;
    }

    public override bool PostPerform()
    {

        GWorld.Instance.GetQueue("toilets").AddResource(target);
        inventory.RemoveItem(target);
        beliefs.RemoveState("needToUseToilet");
        GWorld.Instance.GetWorld().ModifyState("FreeToilet", 1);

        // Debug.Log("Going to toilet Finished");


        return true;
    }
}
