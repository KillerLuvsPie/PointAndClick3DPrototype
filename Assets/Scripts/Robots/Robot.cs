using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Robot", menuName = "Scriptable Object/Robot")]
public class Robot : ScriptableObject
{
    public string robotName;
    public Sprite placeholderImage;
    public GameObject robotInterface;
}