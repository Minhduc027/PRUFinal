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
        SceneManager.LoadScene(1);
    }

    public void OnContinueBtnClicked() {
        int wave = GameDataManager.Instance.LastWave;
        SceneManager.LoadScene(wave);
    }

    public void OnExitBtnClicked() {

    }

}
