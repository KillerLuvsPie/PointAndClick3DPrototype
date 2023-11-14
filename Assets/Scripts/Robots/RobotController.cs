using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public Robot robot;
    public GameObject robotInterface;
    public RobotController robotTarget;
    public DataVariables.RobotButtonGroup buttonGroup = DataVariables.RobotButtonGroup.None;

    void Start()
    {
        robotInterface = robot.robotInterface;
        if(buttonGroup == DataVariables.RobotButtonGroup.None)
            Debug.Log(name + " has no button group assigned");
    }
    void Update()
    {
        
    }
}
