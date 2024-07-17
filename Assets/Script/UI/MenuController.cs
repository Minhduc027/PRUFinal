using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject continueBtn;
    private void Awake() {
        int wave = GameDataManager.Instance.LastWave;
        if (wave == 0) {
            continueBtn.SetActive(false);
        } else {
            continueBtn.SetActive(true);
        }
    }
    public void OnPlayBtnClicked() {
        SceneManager.LoadSceneAsync(1);
    }

    public void OnContinueBtnClicked() {
        int wave = GameDataManager.Instance.LastWave;
        SceneManager.LoadSceneAsync(wave);
    }

    public void OnExitBtnClicked() {

    }

}
