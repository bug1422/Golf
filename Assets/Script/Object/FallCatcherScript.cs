using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FallCatcherScript : MonoBehaviour
{
    public GameObject HUD;
    // Start is called before the first frame update
    public GameObject SpawnPositionObject;
    public GameObject PlayerObject;

    private Dictionary<string, Vector3> _possibleSpawnPosition = new Dictionary<string, Vector3>();
    
    void Start()
    {
        for (int i = 0; i < SpawnPositionObject.transform.childCount; i++)
        {
            var getchild = SpawnPositionObject.transform.GetChild(i);
            var childPosition = getchild.position;
            _possibleSpawnPosition.Add(getchild.name,childPosition);  
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player")
            return; 
        var playerPosition = collision.transform.position;
        var distanceToSpawnPosition = new Dictionary<string, float>();
        foreach(var vect in _possibleSpawnPosition) 
        {
            var distance = Vector3.Distance(playerPosition, vect.Value);
            distanceToSpawnPosition.Add(vect.Key,distance);
        }
        var getShortestDistance = distanceToSpawnPosition.OrderBy(d => d.Value).First();
        var getPositionToSpanw = _possibleSpawnPosition.First(p => p.Key.Equals(getShortestDistance.Key));
        PlayerObject.transform.position = getPositionToSpanw.Value;
        Player.health -= 10;
        HUD.GetComponent<HUD>().RemoveOneHeart();
        Debug.Log(Player.health);
    }

}
