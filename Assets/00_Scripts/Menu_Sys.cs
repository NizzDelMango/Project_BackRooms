using UnityEngine;

public class Menu_Sys : MonoBehaviour
{
    private bool isGamePaused = false;

    public MonoBehaviour firstPersonControllerScript;

    // public GameObject pauseMenuUI; 

    void Start()
    {
        if (firstPersonControllerScript == null)
        {
            firstPersonControllerScript = GetComponent<FirstPersonController>();
        }

        ResumeGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;

        if (firstPersonControllerScript != null)
        {
            firstPersonControllerScript.enabled = false;
        }

        // if (pauseMenuUI != null)
        // {
        //     pauseMenuUI.SetActive(true);
        // }
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;

        if (firstPersonControllerScript != null)
        {
            firstPersonControllerScript.enabled = true;
        }

        // if (pauseMenuUI != null)
        // {
        //     pauseMenuUI.SetActive(false);
        // }
    }
}