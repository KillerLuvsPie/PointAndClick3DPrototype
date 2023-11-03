using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    public static ElevatorManager Instance;
    public Animator elevatorAnim;
    public Transform waitPosition;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
