using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConnectionController : MonoBehaviour
{
    public void DeactivateLine()
    {
        GameManager.Instance.line.GameObject().SetActive(false);
    }

    public void SetLine(Vector3 startPos, Vector3 endPos)
    {
        Vector3[] positions = new Vector3[2]{startPos, endPos};
        GameManager.Instance.line.positionCount = positions.Length;
        GameManager.Instance.line.SetPositions(positions);
        GameManager.Instance.line.GameObject().SetActive(true);
    }
}
