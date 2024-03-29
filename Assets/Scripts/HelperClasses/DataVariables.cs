using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataVariables
{
    //ELEVATOR STATE ENUM
    public enum ElevatorNPCState {Idle, Spawn, Move, Wait, Enter}

    //ROBOT TAG LIST
    public enum RobotButtonGroup {None, FlyingDrone, ShockDrone, Camera, GarageKeypad, Light}
}
