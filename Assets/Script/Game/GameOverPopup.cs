using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPopup : MonoBehaviour
{
    [SerializeField] private Text txtScore, txtHighScore;

    public void LoadText()
    {
        txtScore.text = ScoreManager.instance.score.ToString();
        var highScore = PlayerPrefs.GetInt("highScore");
        txtHighScore.text = highScore.ToString();
    }
    public void PlayButtonOnClick() => SceneManager.LoadScene(0);

}

