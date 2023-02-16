using UnityEngine;
using UnityEngine.UI;
using Observer;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private Text tmpScore;
    public int score;

    void Awake()
    {
        instance = this;
        score = -1;
        UpdateScore(1);
    }

    private void OnEnable()
    {
        this.RegisterListener(EventID.ScorePlus, (param) => UpdateScore((int)param));
        this.RegisterListener(EventID.OnSaveHighScore, (param) => SaveHighScore());

    }


    public void SaveHighScore()
    {
        var high = PlayerPrefs.GetInt("highScore");
        if (score > high) PlayerPrefs.SetInt("highScore", score);
    }

    public void UpdateScore(int scorePlus)
    {
        score += scorePlus;
        tmpScore.text = score.ToString();

    }




}
