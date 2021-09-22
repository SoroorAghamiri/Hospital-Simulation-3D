using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janitor : GAgent
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //We set the treat patient goal to nonremoval because we want the nurse to treat patients over and over again
        subGoal s1 = new subGoal("puddleCleaned", 1, false);
        goals.Add(s1, 3);

        subGoal s2 = new subGoal("rested", 1, false);
        goals.Add(s2, 1);

        subGoal s3 = new subGoal("relieved", 1, false);
        goals.Add(s3, 2);
        subGoal s4 = new subGoal("isSafe", 1, false);
        goals.Add(s4, 10);

        Invoke("GetTired", Random.Range(10, 20));
        Invoke("NeedToilet", Random.Range(50, 100));

    }

    void GetTired()
    {
        beliefs.ModifyState("exhausted", 1);
        Invoke("GetTired", Random.Range(10, 20));
    }

    void NeedToilet()
    {
        beliefs.ModifyState("needToUseToilet", 1);
        Invoke("NeedToilet", Random.Range(50, 100));
    }
    private void Update()
    {
        GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
        if (weapon)
        {
            float distanceToGrenade = Vector3.Distance(this.transform.position, weapon.transform.position);

            if (distanceToGrenade < 5f)
            {

                Destroy(weapon.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
