using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : GAgent
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        subGoal s1 = new subGoal("rested", 1, false);
        goals.Add(s1, 2);

        subGoal s2 = new subGoal("patientVisited", 1, false);
        goals.Add(s2, 5);


        subGoal s3 = new subGoal("relieved", 1, false);
        goals.Add(s3, 3);

        subGoal s4 = new subGoal("isSafe", 1, false);
        goals.Add(s4, 10);

        subGoal s5 = new subGoal("recoveryChecked", 1, false);
        goals.Add(s5, 6);


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
