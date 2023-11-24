using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public IEnumerator ResetLevel()
    {
        UIManager.Instance.blackScreenAnim.SetBool("FadeOut", true);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => UIManager.Instance.blackScreenAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
