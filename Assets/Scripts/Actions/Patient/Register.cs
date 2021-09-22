using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : GAction
{
    public override bool PrePerform()
    {
        // Debug.Log("register started");
        return true;
    }

    public override bool PostPerform()
    {
        // Debug.Log("register finished");
        // beliefs.ModifyState("atHospital", 1);
        return true;
    }
}
