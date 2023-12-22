using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static PauseController Instance;
    public GameObject pauseMenu;
    
    //FUNCTIONS
    public void PauseButton()
    {
        pauseMenu.SetActive(true);
        SoundManager.Instance.PauseAllSounds();
        Time.timeScale = 0;
    }

    public void ContinueButton()
    {
        pauseMenu.SetActive(false);
        SoundManager.Instance.UnpauseAllSounds();
        Time.timeScale = 1;
    }

    public void RetryLevel()
    {
        ContinueButton();
        StartCoroutine(GameManager.Instance.ResetLevel());
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
        if(Input.GetMouseButtonDown(1))
        {
            if(pauseMenu.activeSelf == false)
                PauseButton();
            else
                ContinueButton();
        }
    }
}
