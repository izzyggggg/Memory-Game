using UnityEngine;
using UnityEngine.UI;
public class ControllerScript : MonoBehaviour
{
    public GameObject MyCanvas;
    public Transform Panel;
    public GameObject NameContainer;
    public GameObject ScoreContainer;
    public Transform NameArray;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MyCanvas = GameObject.Find("Canvas");
        Panel = MyCanvas.transform.Find("Panel").GetComponent<Transform>();
        NameContainer = Panel.Find("NameContainer").GetComponent<GameObject>();
        ScoreContainer = Panel.transform.Find("ScoreContainer").GetComponent<GameObject>();
        //NameArray = Panel.GetComponentsInChildren<>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
