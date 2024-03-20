using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public float conversionRate = 10f;
    [SerializeField]
    private Image heart;
    private int totalHealth;
    // Start is called before the first frame update
    private void Awake()
    {
        var totalHealth = Player.health / conversionRate;
        this.totalHealth = Mathf.RoundToInt(totalHealth);
    }
    void Start()
    {
        /*print(totalHealth);
        for (int i = 0; i < 5; i++)
        {
            var instance = Instantiate(heart, new Vector3(i * 100, 0), Quaternion.identity);
            instance.transform.SetParent(transform, false);
        }*/
    }
    public void RemoveOneHeart()
    {
        var count = transform.childCount;
        Destroy(transform.GetChild(count - 1).gameObject);
    }

}
