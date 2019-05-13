using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextComponent : MonoBehaviour
{
    // Start is called before the first frame update

    
    void Start()
    {
        GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
