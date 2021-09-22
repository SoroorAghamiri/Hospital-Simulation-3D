using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToDoctor : GAction
{
    // GameObject doctor;
    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Office");
        if (target == null)
        {
            return false;
        }
        // doctor = inventory.FindItemWithTag("Doctor");
        return true;
    }

    //Only nusrse has to release the cubicle and add it to world state
    public override bool PostPerform()
    {
        // GWorld.Instance.GetWorld().ModifyState("TreatingPatient", 1);
        // GWorld.Instance.GetQueue("offices").AddResource(target); //AddCubicle(target);

        inventory.RemoveItem(target);
        // inventory.RemoveItem(doctor);
        beliefs.ModifyState("takeToDoctor", -1);
        return true;
    }
}
