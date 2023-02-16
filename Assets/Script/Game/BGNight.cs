using UnityEngine;
using Observer;
public class BGNight : MonoBehaviour
{

    void Update()
    {
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, 0.1f * Time.deltaTime);
        if(transform.localScale.x < 1.1f) this.PostEvent(EventID.GameOver);
    }


    private void OnEnable()
    {
        this.RegisterListener(EventID.UpdateArea, (param) => UpdateArea());
    }

    private void UpdateArea()
    {
        transform.localScale = new Vector2(transform.localScale.x + 0.4f, transform.localScale.y + 0.4f);
    }
}
