using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectActive : MonoBehaviour 
{
    public Image myImg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myImg.color == Color.white)
        {
            GameManager.Instance.result.Add(gameObject);
            GameManager.Instance.RandomColor();
            myImg.color = GameManager.Instance.currentColor;

        }
    }
   

   

    // Start is called before the first frame update
    void Start()
    {
        myImg = gameObject.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
