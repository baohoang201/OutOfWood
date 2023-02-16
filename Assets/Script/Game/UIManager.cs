using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup gameOverPopUp, mainGame;
    [SerializeField] private GameObject enviroment;
    private float time = 0.5f;
    public static UIManager instance;
    void Awake()
    {
        instance = this;
    }
    private void MoveDown(GameObject gameObject) => gameObject.transform.DOMoveY(gameObject.transform.position.y - 1, 1f).Play();

    public void EnableGameOverPopUp(bool enable)
    {
        if (enable) gameOverPopUp.DOFade(1f, time).Play();
        else gameOverPopUp.DOFade(0f, time).Play();
        gameOverPopUp.blocksRaycasts = enable;
    }

    public void EnableMainGame(bool enable)
    {
        if (enable) mainGame.DOFade(1f, time).Play();
        else mainGame.DOFade(0f, time).Play();
        mainGame.blocksRaycasts = enable;

    }

    public void EnableEnviroment(bool enable) => enviroment.SetActive(enable);


}
