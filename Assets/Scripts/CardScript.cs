using UnityEngine;

public class Card_Script : MonoBehaviour
{
    bool changeColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = GenerateColor.color;
    }
}
