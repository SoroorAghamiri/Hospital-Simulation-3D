using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().RemoveState("JokerAlert");
        Destroy(this.gameObject);
        return true;
    }

}
