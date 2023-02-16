using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    private float speed = 0.5f;
    private float offset;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;
        image.material.SetTextureOffset("_MainTex", new Vector2(0, 0));
        
    }
}
