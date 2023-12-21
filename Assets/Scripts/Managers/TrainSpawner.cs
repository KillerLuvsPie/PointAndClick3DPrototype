using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    public GameObject Train;
    private Vector3 spawnposition = new Vector3 (-90f, 9f, -120f);
    private float timer = 0;
    private float interval = 8;
    // Start is called before the first frame update
    void Start()
    {
        Object.Instantiate(Train, spawnposition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval)
        {
            Object.Instantiate(Train, spawnposition, Quaternion.identity);

            timer = 0;
        }

    }
}
