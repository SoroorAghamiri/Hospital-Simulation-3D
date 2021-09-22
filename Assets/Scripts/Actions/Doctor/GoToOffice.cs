using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToOffice : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("patientsWaitingForDoctor").RemoveResource();
        if (target == null)
        {
            return false;
        }

        // inventory.AddItem(target);
        GWorld.Instance.GetWorld().ModifyState("PatientWaitingForDoctor", -1);
        //Debug.Log("Research Started");
        return true;
    }

    public override bool PostPerform()
    {
        int hOrS = Random.Range(0, 10);
        GWorld.Instance.GetWorld().ModifyState("PatientVisited", 1);
        if (hOrS < 5)
        {
            if (target)
            {
                target.GetComponent<GAgent>().beliefs.ModifyState("needSurgery", 1);
                target.GetComponent<GAgent>().beliefs.RemoveState("needDoctor");
            }
        }
        else
        {
            if (target)
            {
                target.GetComponent<GAgent>().beliefs.ModifyState("isCured", 1);
                target.GetComponent<GAgent>().beliefs.RemoveState("needDoctor");
            }
        }
        GWorld.Instance.GetWorld().ModifyState("FreeOffice", 1);
        GWorld.Instance.GetQueue("doctors").AddResource(this.gameObject);
        GWorld.Instance.GetQueue("patientsWaitingForDoctor").RemoveResource(target);



        return true;
    }
}
