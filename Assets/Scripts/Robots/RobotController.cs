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
    public bool isActive = false;

    void Start()
    {
        robotInterface = robot.robotInterface;
    }
}
