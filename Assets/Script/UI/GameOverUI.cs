using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RestartBtnClicked() {
        GameDataManager.Instance.SetDefaultValue();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void ReturnMenuBtnClicked() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}