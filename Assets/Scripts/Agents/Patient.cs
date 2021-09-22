using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : GAgent
{
    new void Start()
    {
        base.Start();
        subGoal s1 = new subGoal("waitingForNurse", 1, true);
        goals.Add(s1, 3);
        subGoal s2 = new subGoal("isTreated", 1, true);
        goals.Add(s2, 5);
        subGoal s3 = new subGoal("isHome", 1, true);
        goals.Add(s3, 1);
        subGoal s4 = new subGoal("relieved", 1, false);
        goals.Add(s4, 2);
        subGoal s5 = new subGoal("isVisited", 1, true);
        goals.Add(s5, 6);
        subGoal s6 = new subGoal("isSafe", 1, false);
        goals.Add(s6, 11);
        subGoal s7 = new subGoal("isWaitingForDoctor", 1, true);
        goals.Add(s7, 7);
        subGoal s8 = new subGoal("isWaitingForSurgery", 1, true);
        goals.Add(s8, 8);
        subGoal s9 = new subGoal("goToRecovery", 1, true);
        goals.Add(s9, 9);
        subGoal s10 = new subGoal("Recovered", 1, true);
        goals.Add(s10, 10);
        // Invoke("NeedToilet", Random.Range(5, 10));

    }


    void NeedToilet()
    {
        beliefs.ModifyState("needToUseToilet", 1);
        Invoke("NeedToilet", Random.Range(5, 10));
    }

    private void Update()
    {
        GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
        if (weapon)
        {
            float distanceToGrenade = Vector3.Distance(this.transform.position, weapon.transform.position);

            if (distanceToGrenade < 5f)
            {
                GWorld.Instance.GetQueue("patients").RemoveResource(this.gameObject);
                Destroy(weapon.gameObject);
                Destroy(this.gameObject);
            }
        }
    }

}
