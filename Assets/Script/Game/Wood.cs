using UnityEngine;
using Observer;
public class Wood : MonoBehaviour
{

    [SerializeField] private Sprite woodFire;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tag.Fire))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            spriteRenderer.sprite = woodFire;
            this.PostEvent(EventID.UpdateArea);
            Destroy(gameObject, 4);
        }
    }
}
