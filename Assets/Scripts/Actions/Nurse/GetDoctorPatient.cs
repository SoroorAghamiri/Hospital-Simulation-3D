using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDoctorPatient : GAction
{
    GameObject resource;
    // GameObject doctor;
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("patients").RemoveResource();
        if (target == null)
        {
            return false;
        }

        // doctor = GWorld.Instance.GetQueue("doctors").RemoveResource();
        // if (doctor == null)
        // {
        //     GWorld.Instance.GetQueue("patients").AddResource(target);
        //     target = null;
        //     return false;
        // }
        // inventory.AddItem(doctor);

        resource = GWorld.Instance.GetQueue("offices").RemoveResource();
        if (resource != null)
        {
            inventory.AddItem(resource);
        }
        else
        {
            GWorld.Instance.GetQueue("patients").AddResource(target);
            target = null;
            return false;
        }

        GWorld.Instance.GetWorld().ModifyState("PatientWaitingForDoctor", 1);

        GWorld.Instance.GetWorld().ModifyState("FreeOffice", -1);

        return true;
    }

    public override bool PostPerform()
    {

        GWorld.Instance.GetWorld().ModifyState("WaitingForDoctor", -1);
        beliefs.ModifyState("takeToDoctor", 1);

        if (target)
        {
            target.GetComponent<GAgent>().inventory.AddItem(resource);
            // doctor.GetComponent<GAgent>().inventory.AddItem(resource);
        }
        return true;
    }
}
