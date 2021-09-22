using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWaitingRoom : GAction
{
    public override bool PrePerform()
    {
        // Debug.Log("Going to waiting room");
        return true;
    }

    public override bool PostPerform()
    {
        int prob = Random.Range(0, 10);
        if (prob <= 4)
        {
            GWorld.Instance.GetWorld().ModifyState("WaitingForCube", 1);
            beliefs.ModifyState("needCube", 1);
        }
        else if (prob > 4)
        {
            GWorld.Instance.GetWorld().ModifyState("WaitingForDoctor", 1);
            beliefs.ModifyState("needDoctor", 1);
            // Debug.Log("Added WaitingForDoctor state");
        }
        GWorld.Instance.GetQueue("patients").AddResource(this.gameObject);//AddPatient(this.gameObject);
        beliefs.ModifyState("atHospital", 1);
        return true;
    }
}
