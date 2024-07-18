using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameSetting : MonoBehaviour
{
    public void OnSaveCheckPointClicked() {
        GameDataManager.Instance.SaveData();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void OnExitClicked() {
        Application.Quit();
    }
    public void OnResumeClicked() {
        Time.timeScale = 1;
        Debug.Log("Resume Clicked");
        this.gameObject.SetActive(false);
    }

    public void OnRestartClicked() {
        GameDataManager.Instance.SetDefaultValue();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
