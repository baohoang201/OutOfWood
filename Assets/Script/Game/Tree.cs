using UnityEngine;
using DG.Tweening;
using Observer;
public class Tree : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Wood woodPrefab;
    [SerializeField] private Apple applePrefab;
    private int heath;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D box;
    private int id;
    private int rateForApple;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        SetUpTree();
        RandomRate();
    }

    private void RandomRate() => rateForApple = Random.Range(0, 3);


    private void SetUpTree()
    {
        //Random sprite
        var index = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[index];

        //Set id
        id = index;

        //Set health
        if (index == 0) heath = 3;
        else heath = 5;
    }

    private void InstiateApple()
    {
        var apple = Instantiate(applePrefab, transform.position, Quaternion.identity);
        ItemFall(apple.gameObject);
    }

    private void InstiateWood(int numberWood)
    {
        for (int i = 0; i < numberWood; i++)
        {
            var wood = Instantiate(woodPrefab, transform.position, Quaternion.identity);
            ItemFall(wood.gameObject);
        }

    }

    private void ItemFall(GameObject item)
    {
        var rb = item.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        rb.isKinematic = false;
        rb.gravityScale = 1;

        rb.transform.DORotate(new Vector3(0, 0, Random.Range(70, 360)), .5f, RotateMode.Fast).Play().OnComplete(() =>
        {
            rb.isKinematic = true;
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        });
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tag.Axe))
        {
            if (heath > 0)
            {
                heath--;
                this.PostEvent(EventID.CameraShake);
            }
            else
            {
                if (rateForApple == 1) InstiateApple();
                box.enabled = false;
                spriteRenderer.DOFade(0f, .5f).Play().OnComplete(() =>
                {
                    InstiateWood(id + 1);
                    DOVirtual.DelayedCall(10, () =>
                    {
                        SetUpTree();
                        spriteRenderer.DOFade(1f, .5f).Play().OnComplete(() =>
                        {
                            box.enabled = true;
                        });
                    });

                });

            }
        }
    }




}
