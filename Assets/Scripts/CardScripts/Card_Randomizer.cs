using UnityEngine;

public class Card_Randomizer : MonoBehaviour
{
    /// <summary>
    /// Need to make code that will allow the game to swith the places of the cards, but only once the colors have ben generated.
    /// Will also need to make a copy of the card array, copy the actual cards from the GenerateColor script, switch the colors, and place them back in the generate color script.
    /// </summary>
    public GameObject ColorGenScript;
    public CardScript[] cards;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlaceCards()
    {
      if(ColorGenScript != null)
        {
            GenerateColor GenColor = ColorGenScript.GetComponent<GenerateColor>();
            if (GenColor.globalNumberOfColorsGenerated == 6)
            {
                
            }
        }  
    }
}
