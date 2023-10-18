using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public Robot robot;
    private SpriteRenderer placeholderImage;
    public GameObject robotInterface;
    
    void Start()
    {
        placeholderImage = GetComponent<SpriteRenderer>();
        placeholderImage.sprite = robot.placeholderImage;
        robotInterface = robot.robotInterface;
    }
}
