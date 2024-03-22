using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameObject player;
    private GameObject gems;
    private GameObject key;
    private GameObject door;
    private GameObject HUD;

    private SaveHandler _saveHandlerScript;

    private bool _isEnded = false;
    private SaveData _dataToSave;
    private int health;
    private void Awake()
    {
        health = Player.health;
		player = GameObject.Find("Player");
		gems = GameObject.Find("Gems");
        key = GameObject.Find("Key");
        door = GameObject.Find("Door");
        HUD = GameObject.Find("HUD");
        _saveHandlerScript = SaveHandler.Instance;
        _dataToSave = new SaveData();
        Instance = gameObject.GetComponent<GameManager>();
        SceneManager.sceneLoaded += (Scene, SceneMode) =>
        {
            if (Scene.name == "MainScene")
            {
                var tryGetGameManagementScript = GameObject.Find("GameManagement");
                if (tryGetGameManagementScript is null)
                    throw new UnityException("error cannot find game management of round ");
                var gameManagerScript = tryGetGameManagementScript.GetComponent<GameManager>();
                if (SaveHandler.IsLoadGame)
                {
                    if (SaveHandler.IsLoaded == false)
                    {
                        print("load");
                        _saveHandlerScript.OnLoadButton();
                        gameManagerScript.SetData(_saveHandlerScript.Data);
                        SaveHandler.IsLoaded = true;
                    }
                }
            }
        };
    }
    private void OnDestroy()
    {
        SaveHandler.IsLoadGame = false;
        SaveHandler.IsLoaded = false;
    }
    private IEnumerator()
    private void Update()
    {
        SaveHandler.Instance.SceneTempData.playerPosition =  player.transform.position;
    }
    public void SetData(SaveData data)
    {
        player.transform.position = data.playerPosition;
        Player.health = data.health;
        var healthbar = GameObject.Find("Health").transform;
        var total = data.health / HealthBar.conversionRate;
        print(total);
        for(int i = total-1; i >= 0; i--)
        {
            Destroy(healthbar.GetChild(i).gameObject);
        }
        for (int i = 0; i < gems.transform.childCount; i++)
        {
            var gem = gems.transform.GetChild(i);
            if(data.gems.Contains(gem.position))
            {
                GameObject.Destroy(gem.gameObject);
                HUD.GetComponent<HUD>().AddScore();
            }
        }
        if (data.isKeyOpened)
        {
            GameObject.Destroy(key);
            GameObject.Destroy(door);
        }
    }
    public void Reset()
    {
        SaveHandler.Instance.SceneTempData = new SaveData()
        {
            playerPosition = player.transform.position,
            health = health,
            gems = new List<Vector2>(),
            isKeyOpened = false
        };
    }
}
