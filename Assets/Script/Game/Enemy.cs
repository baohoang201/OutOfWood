using UnityEngine;
using DG.Tweening;
using Observer;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform axe;
    private float distance;
    private float moveSpeed;
    private float timeRate, nextTime;
    private Animator animator;

    private int heath;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        moveSpeed = 2;
        nextTime = Time.time;
        timeRate = 2;
        heath = 5;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, PlayerController.instance.transform.position);
        if (distance > 1) Move();
        else
        {
            if (Time.time > nextTime)
            {
                AttackPlayer();
                nextTime = timeRate + Time.time;
            }
        }
    }

    private void Move()
    {
        animator.SetBool(Tag.EnemyStatus, true);
        if (transform.position.x < PlayerController.instance.transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);
        transform.position = Vector2.MoveTowards(transform.position, PlayerController.instance.transform.position, moveSpeed * Time.deltaTime);
    }

    private void AttackPlayer()
    {
        animator.SetBool(Tag.EnemyStatus, false);
        axe.GetComponent<BoxCollider2D>().enabled = true;
        axe.DORotate(new Vector3(0, 0, 120 * transform.localScale.x), .2f, RotateMode.Fast).Play().OnComplete(() =>
        {
            axe.DORotate(new Vector3(0, 0, 0), .2f, RotateMode.Fast).Play().OnComplete(() =>
            {
                axe.GetComponent<BoxCollider2D>().enabled = false;
            });
        });
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tag.Axe))
        {
            if (heath > 0) heath--;
            else
            {
                this.PostEvent(EventID.InstantiateEnemy);
                Destroy(gameObject);
            }

        }
    }
}
