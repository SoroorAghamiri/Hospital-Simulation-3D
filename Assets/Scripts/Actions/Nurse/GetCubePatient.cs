using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCubePatient : GAction
{
    GameObject resource;
    public override bool PrePerform()
    {
        //Get the patient
        target = GWorld.Instance.GetQueue("patients").RemoveResource();
        if (target == null)
        {
            return false;
        }

        resource = GWorld.Instance.GetQueue("cubicles").RemoveResource();
        if (resource != null)
        {
            inventory.AddItem(resource);
        }
        //If the resource was not available, free the patient and go for another plan
        else
        {
            GWorld.Instance.GetQueue("patients").AddResource(target);
            target = null;
            return false;
        }

        //Busy one cubicle

        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", -1);

        return true;
    }

    public override bool PostPerform()
    {

        GWorld.Instance.GetWorld().ModifyState("WaitingForCube", -1);
        beliefs.ModifyState("takeToCube", 1);
        //Add the resource to the patient too
        if (target)
        {
            target.GetComponent<GAgent>().inventory.AddItem(resource);
        }
        return true;
    }
}
