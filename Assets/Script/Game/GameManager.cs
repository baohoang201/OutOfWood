using UnityEngine;
using Observer;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab, GroupButton, BGNight;
    [SerializeField] private Transform obsParent;
    [SerializeField] private CanvasGroup warning;
    public Transform[] spawnPoint;
    [SerializeField] private UIManager UIManager;
    [SerializeField] private GameOverPopup gameOverPopup;
    public static GameManager instance;
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Invoke("InstiateObs", 10);
    }

    private void OnEnable()
    {
        this.RegisterListener(EventID.GameOver, (param) => GameOver());
        this.RegisterListener(EventID.InstantiateEnemy, (param) => InstiateObs());
        this.RegisterListener(EventID.CameraShake, (param) => ShakeCam());
        this.RegisterListener(EventID.Warning, (param) => Warning());
    }

    private void ShakeCam()
    {
        Camera.main.DOShakePosition(.2f, .1f, 1).Play().OnComplete(() =>
        {
            Camera.main.transform.position = new Vector3(0, 0, -10);
        });
    }

    private void Warning()
    {
        warning.DOFade(1f, .2f).Play().OnComplete(() =>
        {
            warning.DOFade(0f, 0.2f).Play();
        });
    }


    public void InstiateObs()
    {
        var randomSpawnPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];
        var obsIns = Instantiate(enemyPrefab);
        obsIns.transform.position = randomSpawnPoint.transform.position;
        obsIns.transform.SetParent(obsParent);
    }


    public void GameOver()
    {
        GroupButton.SetActive(false);
        BGNight.SetActive(false);
        UIManager.EnableGameOverPopUp(true);
        gameOverPopup.LoadText();
        UIManager.EnableMainGame(false);
        UIManager.EnableEnviroment(false);
        this.PostEvent(EventID.OnSaveHighScore);
    }


}
