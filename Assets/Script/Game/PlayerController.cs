using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Observer;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform holdPoint, enviroment;
    [SerializeField] private Button btnAttack, btnPick;
    [SerializeField] private Axe axeObj;
    private Vector3 position;
    private float posX, posY;
    public FixedJoystick joystick;
    public static bool PointerDown = false;
    private Rigidbody2D rb;
    private Vector2 move;
    private float moveSpeed;
    public static PlayerController instance;
    private Animator animator;
    private bool isEmpty, isAxe;
    private int heath;

    [SerializeField] private Transform top, bot, right, left;

    void Awake()
    {
        instance = this;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        moveSpeed = 4;
        heath = 5;
        isEmpty = true;
        isAxe = false;
    }

    private void OnEnable()
    {
        btnPick.onClick.AddListener(PickUpAndDrop);
        btnAttack.onClick.AddListener(Attack);
    }


    void Start()
    {
        PointerDown = true;
    }

    void Update()
    {
        MoveJoyStick();
        ClampPos();
    }

    private void PickUpAndDrop()
    {
        if (isEmpty)
        {
            var axe = Physics2D.OverlapBox(transform.position, new Vector2(1f, 1f), 1f / 2, LayerMask.GetMask("Axe"));
            var wood = Physics2D.OverlapBox(transform.position, new Vector2(1f, 1f), 1f / 2, LayerMask.GetMask("Wood"));

            if (axe && wood == null)
            {
                isAxe = true;
                isEmpty = false;
                var localX = transform.localScale.x / Mathf.Abs(transform.localScale.x);
                print(localX);
                axe.transform.localScale = new Vector3(axe.transform.localScale.x * (-localX), 2.5f, 1);
                axe.transform.SetParent(holdPoint);
                axe.GetComponent<BoxCollider2D>().enabled = false;
                axe.transform.rotation = Quaternion.Euler(0, 0, 0);
                axe.gameObject.transform.position = holdPoint.position;
            }

            if (wood && axe == null)
            {
                isAxe = false;
                isEmpty = false;
                wood.transform.SetParent(holdPoint);
                wood.transform.rotation = Quaternion.Euler(0, 0, 0);
                wood.GetComponent<BoxCollider2D>().enabled = false;
                wood.gameObject.transform.position = holdPoint.position;

            }
        }
        else
        {
            if (isAxe)
            {
                if (axeObj.isComplete) ThrowItem(holdPoint.GetChild(0).gameObject, enviroment);
            }
            else ThrowItem(holdPoint.GetChild(0).gameObject, enviroment);
        }
    }

    private void ThrowItem(GameObject item, Transform parent)
    {
        isEmpty = true;
        item.transform.SetParent(parent);

        item.GetComponent<Rigidbody2D>().isKinematic = false;
        item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 3, ForceMode2D.Impulse);

        item.transform.DORotate(new Vector3(0, 0, Random.Range(30, 360)), .5f, RotateMode.Fast).Play().OnComplete(() =>
        {
            item.GetComponent<Rigidbody2D>().isKinematic = true;
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        });

        DOVirtual.DelayedCall(.5f, () =>
        {
            item.GetComponent<BoxCollider2D>().enabled = true;
        });

        if (isAxe)
        {
            isAxe = false;
            item.transform.localScale = new Vector3(2.5f, 2.5f, 1);
        }


    }


    private void Attack()
    {
        if (isAxe)
        {
            axeObj.Attack();
        }
    }


    private void MoveJoyStick()
    {
        if (move.x >= 0) transform.localScale = new Vector3(-1.3f, 1.3f, 1);
        else transform.localScale = new Vector3(1.3f, 1.3f, 1);
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;
        float hAxis = move.x;
        float vAxis = move.y;
        float zAxis = Mathf.Atan2(hAxis, vAxis);
        transform.eulerAngles = new Vector3(0f, 0f, -zAxis);
    }

    private void ClampPos()
    {
        posX = Mathf.Clamp(transform.position.x, left.position.x, right.position.x);
        posY = Mathf.Clamp(transform.position.y, bot.position.y, top.position.y);
        transform.position = new Vector2(posX, posY);
    }

    private void FixedUpdate()
    {
        if (PointerDown)
        {
            animator.SetBool("PlayerStatus", false);
            return;
        }

        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
        animator.SetBool("PlayerStatus", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tag.Enemy))
        {
            if (heath > 0)
            {
                heath--;
                this.PostEvent(EventID.Warning);
            } 
            else this.PostEvent(EventID.GameOver);
        }
    }




}
