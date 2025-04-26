using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSprite : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();

    void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,sprites.Count)]; 
    }

}
