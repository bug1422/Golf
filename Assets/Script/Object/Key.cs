using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private GameObject Door;
    private Vector3 initialPos;
    void Start()
    {
        initialPos = transform.position;
        StartCoroutine(Floating());
    }
    private IEnumerator Floating()
    {
        while (true)
        {
            transform.position = initialPos + new Vector3(0, Mathf.Sin(Time.time) * 0.5f, 0);
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("Save Handler").GetComponent<SaveHandler>().KeyUnlock();
        Destroy(Door);
        Destroy(this.gameObject);
    }
}
