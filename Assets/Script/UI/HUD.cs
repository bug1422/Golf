using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Score;
    public GameObject winMenu;
    private int score = 0;
    // Start is called before the first frame update
    public void RemoveOneHeart()
    {

    }

    public void AddScore()
    {
        score += 1;
        Score.text = score.ToString();
        if(score == 3)
        {
            Time.timeScale = 0;
            Player.isAlive = false;
            winMenu.SetActive(true);
        }
    }
}
