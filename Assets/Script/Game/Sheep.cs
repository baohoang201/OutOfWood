using UnityEngine;

public class Sheep : MonoBehaviour
{
    private float distance, distanceMove;
    private Vector2 direction, directionMove;
    private Animator animator;
    private int randomNumeber, randomMove;
    private float fireRate;
    private float nextFire;
    void Start()
    {
        animator = GetComponent<Animator>();
        fireRate = 3f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, PlayerController.instance.transform.position);
        direction = PlayerController.instance.transform.position - transform.position;

        if (distance < 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, -direction, 1.5f * Time.deltaTime);
            animator.SetBool("sheepStatus", true);
        }
        else
        {
            if (Time.time > nextFire)
            {
                randomNumeber = Random.Range(0, 2);
                randomMove = Random.Range(0, GameManager.instance.spawnPoint.Length);
                nextFire = Time.time + fireRate;
            }
            if (randomNumeber == 1) MoveRandom();
            else animator.SetBool("sheepStatus", false);
            return;
        }
    }

    private void MoveRandom()
    {
        distanceMove = Vector2.Distance(transform.position, GameManager.instance.spawnPoint[randomMove].position);


        if (distanceMove > 1.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, GameManager.instance.spawnPoint[randomMove].position, 0.5f * Time.deltaTime);
            if (transform.position.x < GameManager.instance.spawnPoint[randomMove].position.x) transform.localScale = new Vector3(1.2f, 1.2f, 1);
            else transform.localScale = new Vector3(-1.2f, 1.2f, 1);

            animator.SetBool("sheepStatus", true);
        }
        else return;
    }


}
