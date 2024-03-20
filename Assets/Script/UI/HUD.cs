using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    [SerializeField]
    public float conversionRate = 10f;
    [SerializeField]
    private Image heart;
    [SerializeField]
    private GameObject healthParent;
    [SerializeField]
    private TextMeshProUGUI Score;
    public GameObject winMenu;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        var totalHealth = Player.health / conversionRate;
        print(totalHealth);
        for (int i = 0; i < totalHealth; i++)
        {
            var instance = Instantiate(heart, new Vector3(i * 100, 0), Quaternion.identity, healthParent.transform);
        }
    }
    public void RemoveOneHeart()
    {
        var count = transform.childCount;
        Destroy(transform.GetChild(count - 1).gameObject);
    }

    public void AddScore()
    {
        score += 1;
        Score.text = score.ToString();
        if(score == 13)
        {
            Time.timeScale = 0;
            winMenu.SetActive(true);
        }
    }
}
