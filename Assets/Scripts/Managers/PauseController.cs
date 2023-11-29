using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static PauseController Instance;
    public GameObject pauseMenu;
    
    public void PauseButton()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RetryLevel()
    {
        ContinueButton();
        StartCoroutine(GameManager.Instance.ResetLevel());
    }

    void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(pauseMenu.activeSelf == false)
                PauseButton();
            else
                ContinueButton();
        }
    }
}
