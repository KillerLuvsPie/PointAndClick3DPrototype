using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject sideMenu;
    private RectTransform rectT;
    public IEnumerator SideMenuSlideIn()
    {
        while(rectT.anchoredPosition.x > -250)
        {
            rectT.anchoredPosition += new Vector2(-1,0);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator SideMenuSlideOut()
    {
        while(rectT.anchoredPosition.x < 250)
        {
            rectT.anchoredPosition += new Vector2(1,0);
            yield return new WaitForEndOfFrame();
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        //SET SINGLETON
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        rectT = sideMenu.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
