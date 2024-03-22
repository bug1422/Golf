using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public DateTime created = DateTime.Now.Date;
    public Vector2 playerPosition {  get; set; }
    public int health { get; set; }
    public List<Vector2> gems { get; set; }
    public bool isKeyOpened {  get; set; }
    public SaveData()
    {

    }
    public SaveData(Vector2 playerPosition, int health, List<Vector2> gems, bool isKeyOpened)
    {
        this.playerPosition = playerPosition;
        this.health = health;
        this.gems = gems;
        this.isKeyOpened = isKeyOpened;
    }
}
