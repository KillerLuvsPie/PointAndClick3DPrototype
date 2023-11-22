using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform actorWrapper;
    public LineRenderer line;
    public Animator storeShuttersAnim;

    //FUNCTIONS
    public void OpenStore()
    {
        StartCoroutine(OpenStoreCoroutine());
    }
    public IEnumerator OpenStoreCoroutine()
    {
        storeShuttersAnim.SetTrigger("Raise");
        yield return new WaitForSeconds(1);
        StartCoroutine(UIManager.Instance.SideMenuSlideOut());
    }

    //UNITY FUNCTIONS
    void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Update()
    {
        
    }
}
