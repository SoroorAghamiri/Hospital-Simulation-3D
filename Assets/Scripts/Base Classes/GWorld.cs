using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ResourceQueue
{
    public Queue<GameObject> que = new Queue<GameObject>();
    public string tag;
    public string modState;

    public ResourceQueue(string t, string ms, WorldStates w)
    {
        tag = t;
        modState = ms;
        if (tag != "")
        {
            GameObject[] resources = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject go in resources)
            {
                que.Enqueue(go);
            }
        }
        if (modState != "")
        {
            w.ModifyState(modState, que.Count);
        }
    }
    public int ResourceCount()
    {
        return que.Count;
    }
    public void AddResource(GameObject r)
    {
        que.Enqueue(r);
    }
    public void RemoveResource(GameObject r)
    {
        que = new Queue<GameObject>(que.Where(p => p != r));
    }
    public GameObject RemoveResource()
    {
        if (que.Count == 0)
            return null;
        return que.Dequeue();
    }
}
//About sealed classes:Once a class is defined as a sealed class, this class cannot be inherited. 
public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    private static ResourceQueue patients;
    private static ResourceQueue patientsWaitingForDoctor;
    private static ResourceQueue patientsWaitingForSurgery;
    private static ResourceQueue patientsAtRecovery;
    private static ResourceQueue doctors;
    private static ResourceQueue jokers;
    private static ResourceQueue cubicles;
    private static ResourceQueue offices;
    private static ResourceQueue surgeries;
    private static ResourceQueue toilets;
    private static ResourceQueue puddles;
    private static ResourceQueue weapons;
    private static ResourceQueue beds;
    private static Dictionary<string, ResourceQueue> resources = new Dictionary<string, ResourceQueue>();

    static GWorld()
    {
        world = new WorldStates();

        patients = new ResourceQueue("", "", world);
        resources.Add("patients", patients);

        patientsWaitingForDoctor = new ResourceQueue("", "", world);
        resources.Add("patientsWaitingForDoctor", patientsWaitingForDoctor);

        patientsWaitingForSurgery = new ResourceQueue("", "", world);
        resources.Add("patientsWaitingForSurgery", patientsWaitingForSurgery);

        patientsAtRecovery = new ResourceQueue("", "", world);
        resources.Add("patientsAtRecovery", patientsAtRecovery);

        doctors = new ResourceQueue("Doctor", "", world);
        resources.Add("doctors", doctors);

        jokers = new ResourceQueue("Joker", "", world);
        resources.Add("jokers", jokers);

        cubicles = new ResourceQueue("Cubicle", "FreeCubicle", world);
        resources.Add("cubicles", cubicles);

        offices = new ResourceQueue("Office", "FreeOffice", world);
        resources.Add("offices", offices);

        surgeries = new ResourceQueue("Surgery", "FreeSurgery", world);
        resources.Add("surgeries", surgeries);

        toilets = new ResourceQueue("Toilet", "FreeToilet", world);
        resources.Add("toilets", toilets);

        puddles = new ResourceQueue("Puddle", "FreePuddle", world);
        resources.Add("puddles", puddles);

        weapons = new ResourceQueue("Weapon", "FreeWeapon", world);
        resources.Add("weapons", weapons);

        beds = new ResourceQueue("Bed", "FreeBed", world);
        resources.Add("beds", beds);
        //Added just for testing
        Time.timeScale = 5;
    }

    private GWorld() { }
    public ResourceQueue GetQueue(string type)
    {
        return resources[type];
    }
    public static GWorld Instance
    {
        get
        {
            return instance;
        }
    }

    public WorldStates GetWorld()
    {
        return world;
    }


}
