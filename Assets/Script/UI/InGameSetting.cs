using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameSetting : MonoBehaviour
{
    public void OnSaveCheckPointClicked() {
        GameDataManager.Instance.SaveData();
        SceneManager.LoadScene(0);
    }

    public void OnExitClicked() {
        Debug.Log("Exit");
    }
    public void OnResumeClicked() {
        Time.timeScale = 1;
        Debug.Log("Resume Clicked");
        this.gameObject.SetActive(false);
    }

    public void OnRestartClicked() {
        GameDataManager.Instance.SetDefaultValue();
        SceneManager.LoadSceneAsync(1);
    }
}
