using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerSpawner : MonoBehaviour
{
    public GameObject jokerPrefab;

    int spawnNow;
    void Start()
    {
        Invoke("SpawnPatients", Random.Range(300, 500));
    }

    void SpawnPatients()
    {
        if (GameObject.Find("Joker(Clone)") == null)
            Instantiate(jokerPrefab, this.transform.position, Quaternion.identity);
        Invoke("SpawnPatients", Random.Range(3000, 5000));
    }

}
