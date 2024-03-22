using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject HUD;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Save Handler").GetComponent<SaveHandler>().AddGem(transform.position);
            HUD.GetComponent<HUD>().AddScore();
            Destroy(transform.gameObject);
        }
    }
}
