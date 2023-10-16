using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject sideMenu;
    private RectTransform rectT;
    public Button[] commandButtons;
    public IEnumerator SideMenuSlideIn()
    {
        while(rectT.anchoredPosition.x > -250)
        {
            rectT.anchoredPosition += new Vector2(-2,0);
            yield return new WaitForEndOfFrame();
        }
        for(int i = 0; i < commandButtons.Length; i++)
        {
            commandButtons[i].interactable = true;
        }
    }

    public IEnumerator SideMenuSlideOut()
    {
        for(int i = 0; i < commandButtons.Length; i++)
        {
            commandButtons[i].interactable = false;
        }
        while(rectT.anchoredPosition.x < 250)
        {
            rectT.anchoredPosition += new Vector2(2,0);
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
