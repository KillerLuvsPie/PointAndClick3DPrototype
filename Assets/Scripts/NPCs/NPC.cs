using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Scriptable Object/NPC")]
public class NPC : ScriptableObject
{
    public string npcName;
    public Mesh mesh;
    public string message;
}