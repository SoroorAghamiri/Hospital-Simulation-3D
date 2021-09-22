using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfineThePatient : GAction
{
    GameObject patient;
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("beds").RemoveResource();
        if (target == null)
        {
            return false;
        }

        patient = inventory.FindItemWithTag("Untagged");

        if (patient)
        {
            patient.GetComponent<GAgent>().inventory.AddItem(target);
        }
        GWorld.Instance.GetWorld().ModifyState("BusySurgery", -1);
        GWorld.Instance.GetWorld().ModifyState("FreeBed", -1);

        //Debug.Log("Research Started");
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetQueue("patientsWaitingForSurgery").RemoveResource(patient);
        inventory.RemoveItem(patient);
        // beliefs.RemoveState("SurgeryDone");
        return true;
    }
}
