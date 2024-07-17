using CarterGames.Assets.SaveManager;
using Save;
using UnityEngine;

public class GameDataManager : BaseSingleton<GameDataManager>
{
    [SerializeField] private PlayerInfoSaveObject playerInfoSaveObject;

    [Header("Basic Information Player")]
    [SerializeField] private int currentPlayerHP;
    [SerializeField] private int playerMaxHP;
    [SerializeField] private int currentStamina;
    [SerializeField] private int lastWave;
    [SerializeField] private int coin;
    [SerializeField] private Vector2 currentLocation;

    public int CurrentPlayerHP
    {
        get { return currentPlayerHP; }
        set { currentPlayerHP = value; }
    }

    public int PlayerMaxHP
    {
        get { return playerMaxHP; }
        set { playerMaxHP = value; }
    }

    public int CurrentStamina
    {
        get { return currentStamina; }
        set { currentStamina = value; }
    }

    public int LastWave
    {
        get { return lastWave; }
        set { lastWave = value; }
    }

    public int Coin
    {
        get { return coin; }
        set { coin = value; }
    }

    public Vector2 CurrentLocation
    {
        get { return currentLocation; }
        set { currentLocation = value; }
    }


    private void OnEnable() {
        LoadData();
    }
    
    private void LoadData () {
        playerInfoSaveObject.Load();
        currentPlayerHP = playerInfoSaveObject.currentHp.Value;
        playerMaxHP = playerInfoSaveObject.maxHp.Value;
        currentStamina = playerInfoSaveObject.stamina.Value;
        lastWave  = playerInfoSaveObject.wave.Value;
        currentLocation = playerInfoSaveObject.lastLocation.Value;
        coin = playerInfoSaveObject.coin.Value;
        if(playerMaxHP == 0 || currentPlayerHP <= 0) {
            SetDefaultValue();
        }
    }

    public void SaveData () {
        playerInfoSaveObject.currentHp.Value = currentPlayerHP;
        playerInfoSaveObject.maxHp.Value = playerMaxHP;
        playerInfoSaveObject.stamina.Value = currentStamina;
        playerInfoSaveObject.wave.Value = lastWave;
        playerInfoSaveObject.lastLocation.Value = currentLocation;
        playerInfoSaveObject.coin.Value = coin;
        playerInfoSaveObject.Save();
        SaveManager.Save();
    }

    public void ClearSave() {
        playerInfoSaveObject.ResetObjectSaveValues();
        SaveManager.Save();
        SetDefaultValue();
    }

    public void SetDefaultValue() {
        playerMaxHP = 10;
        currentPlayerHP=  playerMaxHP;
        currentStamina = 3;
        lastWave  = 1;
        currentLocation = new Vector2 (0,0);
    }

}
