using UnityEngine;
using DG.Tweening;

public class Axe : MonoBehaviour
{
    public bool isComplete;
    private BoxCollider2D box;
    private void Awake()
    {
        isComplete = true;
        box = GetComponent<BoxCollider2D>();
    }

    public void Attack()
    {
        if (isComplete)
        {
            isComplete = false;
            box.enabled = true;
            transform.DORotate(new Vector3(0, 0, 60 * PlayerController.instance.transform.localScale.x), .2f, RotateMode.Fast).Play().OnComplete(() =>
            {
                transform.DORotate(new Vector3(0, 0, 0), .2f, RotateMode.Fast).Play().OnComplete(() =>
                {
                     isComplete = true;
                     box.enabled = false;
                });


            });
        }

    }

}
