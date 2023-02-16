using System.Collections;
using UnityEngine;
using Observer;
public class Apple : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tag.Player))
        {
            Destroy(gameObject);
            this.PostEvent(EventID.ScorePlus, 10);
        }
    }
}
