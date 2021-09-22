using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseJoker : GAction
{
    public NavMeshSurface surface;
    public GameObject[] emergancyDoors;
    public override bool PrePerform()
    {
        GameObject joker = GWorld.Instance.GetQueue("jokers").RemoveResource();
        if (joker == null)
        {
            return false;
        }
        GWorld.Instance.GetWorld().ModifyState("SecurityComing", 1);
        return true;
    }

    public override bool PostPerform()
    {
        for (int i = 0; i < emergancyDoors.Length; i++)
        {
            if (!emergancyDoors[i].active)
                emergancyDoors[i].SetActive(true);
        }
        surface.BuildNavMesh();
        return true;
    }
}
