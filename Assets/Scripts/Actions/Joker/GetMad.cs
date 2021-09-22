using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMad : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("patients").RemoveResource();
        if (target == null)
        {
            target = GWorld.Instance.GetQueue("doctors").RemoveResource();
            if (target == null)
            {
                return false;
            }
        }
        Debug.Log("GetMad started");
        return true;
    }

    public override bool PostPerform()
    {
        Debug.Log("GetMad Finished");
        // GWorld.Instance.GetQueue("patients").RemoveResource(target);
        Destroy(target.gameObject, 1);
        return true;
    }
}
