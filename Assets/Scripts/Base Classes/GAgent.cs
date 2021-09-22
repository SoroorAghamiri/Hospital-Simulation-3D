using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


//This defines our goals
public class subGoal
{
    public Dictionary<string, int> sGoals;
    public bool remove; //When the goal is satisfied it must be removed from the set of goals

    //s indicates the name of the goal, i indicates the importance of the goal, and r indicates
    public subGoal(string s, int i, bool r)
    {
        sGoals = new Dictionary<string, int>();
        sGoals.Add(s, i);
        remove = r;
    }
}

public class GAgent : MonoBehaviour
{

    public List<GAction> actions = new List<GAction>();//Actions given to achieve a goal

    public Dictionary<subGoal, int> goals = new Dictionary<subGoal, int>();

    public GInventory inventory = new GInventory();

    public WorldStates beliefs = new WorldStates();


    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    [HideInInspector] public subGoal currentGoal;
    Vector3 destination = Vector3.zero;
    // Start is called before the first frame update
    public void Start()
    {
        GAction[] act = this.GetComponents<GAction>();
        foreach (GAction a in act)
            actions.Add(a);
    }

    bool invoked = false;
    void CompleteAction()
    {
        currentAction.running = false;
        // Debug.Log(currentAction + "is completed");
        currentAction.PostPerform();
        invoked = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //If we have running actions
        if (currentAction != null && currentAction.running)
        {
            float distanceToTarget = Vector3.Distance(destination, this.transform.position);
            // Debug.Log(" && distanceToTarget " + distanceToTarget);
            //Check if agent has a goal and is close to it
            if (distanceToTarget < 2f)//currentAction.agent.remainingDistance < 1f)//NAvMEsh code
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return;
        }

        //If we don't have a plan
        if (planner == null || actionQueue == null)//Agent has no plan
        {
            planner = new GPlanner();
            //Sort goals from the most important to the least to plan them
            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach (KeyValuePair<subGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key.sGoals, beliefs);
                if (actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        //If there are no more actions are left to do
        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        //We still have actions in our queue that came back from the planner
        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && currentAction.targetTag != "")
                {
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                }
                //If agent has no place to go
                if (currentAction.target != null)
                {
                    currentAction.running = true;
                    destination = currentAction.target.transform.position;
                    // Debug.Log("Current action target is " + currentAction.target);
                    // Debug.Log("Current action agent is " + currentAction.agent);
                    Transform dest = currentAction.target.transform.Find("Destination");
                    if (dest != null)
                    {
                        destination = dest.position;
                    }
                    currentAction.agent.SetDestination(destination);
                    // Debug.Log("agent is moving");
                }
            }
            //This will force us to get a new plan
            else
            {
                actionQueue = null;
            }
        }
    }
}
