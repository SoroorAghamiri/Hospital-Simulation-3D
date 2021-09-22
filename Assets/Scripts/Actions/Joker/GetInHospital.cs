using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInHospital : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("atHospital", 1);
        GWorld.Instance.GetWorld().ModifyState("JokerAlert", 1);

        return true;
    }
}
