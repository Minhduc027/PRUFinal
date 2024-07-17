using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RestartBtnClicked() {
        GameDataManager.Instance.SetDefaultValue();
        SceneManager.LoadScene(1);
    }
    public void ReturnMenuBtnClicked() {
        SceneManager.LoadScene(0);
    }
}