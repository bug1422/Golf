using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveHandler : MonoBehaviour
{
    private static GameObject GameObjectInstance { get; set; }
    public static SaveHandler Instance { get; private set; }
    public static bool IsLoadGame = false;
    public static bool IsLoaded = false;
    public bool isGameFinished = false;
    public SaveData Data;
    public SaveData SceneTempData;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(GameObjectInstance is null)
        {
            GameObjectInstance = this.gameObject;
            Instance = GameObjectInstance.GetComponent<SaveHandler>();
            SceneTempData = new SaveData() {
                playerPosition = GameObject.Find("Player").transform.position,
                health = Player.health,
                gems = new List<Vector2>(),
                isKeyOpened = false,
            };
        }
        else GameObject.Destroy(gameObject);
    }

    // Update is called once per frame
    void OnApplicationQuit()
    {
        var result = DataService.SaveData(SaveMenu.path, SceneTempData);
    }
    public void OnSave()
    {
        var result = DataService.SaveData(SaveMenu.path, SceneTempData);
    }
    public bool OnLoadButton()
    {
        var readData = DataService.LoadData<SaveData>(SaveMenu.path);
        if (readData is not null)
        {
            IsLoadGame = true;
            Data = readData;
            return true;
        }
        return false;
    }
    public void AddGem(Vector2 pos)
    {
        SceneTempData.gems.Add(pos);
    }
    public void KeyUnlock()
    {
        SceneTempData.isKeyOpened = true;
    }
    public void ChangeHealth()
    {
        SceneTempData.health = Player.health;
    }
}
