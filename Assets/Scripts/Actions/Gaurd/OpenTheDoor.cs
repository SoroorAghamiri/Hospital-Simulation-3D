using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpenTheDoor : GAction
{
    public NavMeshSurface surface;
    public GameObject[] emergancyDoors;
    public override bool PrePerform()
    {
        int joker = GWorld.Instance.GetQueue("jokers").ResourceCount();
        if (joker != 0)
        {
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        for (int i = 0; i < emergancyDoors.Length; i++)
        {
            if (emergancyDoors[i].active)
                emergancyDoors[i].SetActive(false);
        }
        GWorld.Instance.GetWorld().ModifyState("SecurityComing", -1);

        surface.BuildNavMesh();

        return true;
    }
}
