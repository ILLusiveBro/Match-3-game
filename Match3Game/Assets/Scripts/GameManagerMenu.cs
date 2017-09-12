using UnityEngine;

public class GameManagerMenu : MonoBehaviour {

    public GameObject settingsUI;
    public GameObject mainMenuUI;
    bool settingOpened = false;
    bool soundMuted = false;
    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ResetHS()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }

    public void ChangeScreen()
    {
        if(settingOpened)
        {
            settingsUI.SetActive(false);
            mainMenuUI.SetActive(true);
            settingOpened = false;
        }
        else
        {
            settingsUI.SetActive(true);
            mainMenuUI.SetActive(false);
            settingOpened = true;
        }
    }

    public void MuteSound()
    {
        if(soundMuted)
        {
            FindObjectOfType<soundListener>().SoundCountrol();
            soundMuted = false;
        }
        else
        {
            FindObjectOfType<soundListener>().SoundCountrol();
            soundMuted = true;
        }

    }
}
