using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joker : GAgent
{

    new void Start()
    {
        base.Start();

        int randGoal = Random.Range(0, 10);


        subGoal s2 = new subGoal("runAway", 1, true);
        goals.Add(s2, 6);


        subGoal s4 = new subGoal("gotIntoHospital", 1, false);
        goals.Add(s4, 2);

        subGoal s3 = new subGoal("getMad", 1, false);
        goals.Add(s3, 7);

        if (randGoal <= 5)
        {
            subGoal s1 = new subGoal("removeCubes", 1, false);
            goals.Add(s1, 5);
        }
        else if (randGoal > 5)
        {

            subGoal s1 = new subGoal("removeOffices", 1, false);
            goals.Add(s1, 5);
        }
    }

    private void Update()
    {
        GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
        if (weapon)
        {
            float distanceToGrenade = Vector3.Distance(this.transform.position, weapon.transform.position);
            // Debug.Log("distanceToGrenade " + distanceToGrenade);
            if (distanceToGrenade < 5f)
            {
                // Debug.Log("Bumped into a grenade");
                Destroy(weapon.gameObject);
                GWorld.Instance.GetWorld().ModifyState("HospitalIsSafe", 1);
                GWorld.Instance.GetWorld().ModifyState("JokerAlert", -1);
                Destroy(this.gameObject);
            }
        }
        if (currentGoal != null)
        {
            if (currentGoal.sGoals.ContainsKey("runAway") && currentAction.actionName == "RunAway" && currentAction.running)
            {
                // Debug.Log("About to change the goal");
                StartCoroutine(waitToGetMad());
            }
        }
    }

    IEnumerator waitToGetMad()
    {
        // Debug.Log("Entered ienumerator");
        yield return new WaitForSeconds(50);
        beliefs.ModifyState("RunningFailed", 1);
        // goals.Remove(currentGoal);
        if (currentAction.actionName == "RunAway")
            currentAction.running = false;
    }
}
