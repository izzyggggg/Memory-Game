using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GenerateColor : MonoBehaviour
{
    /// <summary>
    /// Need to go to CardScript class to change how the colors are generated. The script should look at a public,
    /// static array, that holds the generated colors, to see and check on what colors have been generated so far. 
    /// There should be a quota for 3 of each color, and the script should have a goal to only get the next available color.
    /// Need to shuffle the locations of the cards to make them appear to be random.
    /// </summary>
        int AmountOfReds = 0;
    int AmountOfGreens = 0;
    int AmountofBlues = 0;
    public int CardTimer;
    public int CardMax;
    public static Color color;
    public GameObject Card;
    int CardCount;
    public Transform[] Placeholders;//placeholder for game objects
    public GameObject Placeholder;//container for placeholders
    private int Index;
    public static List<Color> colors = new List<Color>();
    public List<GameObject> colorCards = new List<GameObject>();
    public static List<GameObject> Cards = new List<GameObject>();
    public static List<GameObject> ListsOfCreatedColors = new List<GameObject>();
    public static int RedCount;
    public static int BlueCount;
    public static int GreenCount;
    //public static int YellowCount;
    public static Text redTextCount;
    public static Text blueTextCount;
    public static Text greenTextCount;
    public int numberOfColorsGenerated = 0;
    public int globalNumberOfColorsGenerated = 0;
    public static bool doneCheckingColors = false;
    public List<CardScript> CardsGenerated;
    public static List<Color> ColorsGenerated;
    public Text ReadyText;
    public Image GameImage;
    public static Dictionary<Color, int> colorCollisionCounts = new Dictionary<Color, int>();
    bool CorrectAmountOfColors;
    //public static Text yellowTextCount;
    void Start()
    {
        CorrectAmountOfColors = true;
        CardTimer = 0;
        color = new Color(UnityEngine.Random.Range(0, 2), UnityEngine.Random.Range(0, 2), UnityEngine.Random.Range(0, 2));
        CardCount = 0;
        Placeholder = GameObject.Find("Placeholders");
        Placeholders = Placeholder.GetComponentsInChildren<Transform>();
        Index = 1;
        RedCount = 0;
        BlueCount = 0;
        GreenCount = 0;
        //YellowCount = 0;
        redTextCount = GameObject.Find("Canvas").transform.Find("RedCount").GetComponent<Text>();
        blueTextCount = GameObject.Find("Canvas").transform.Find("BlueCount").GetComponent<Text>();
        //yellowTextCount = GameObject.Find("Canvas").transform.Find("YellowCount").GetComponent<Text>();
        greenTextCount = GameObject.Find("Canvas").transform.Find("GreenCount").GetComponent<Text>();
        CardMax = 6;
        redTextCount.text = "0";
        blueTextCount.text = "0";
        //yellowTextCount.text = "0";
        greenTextCount.text = "0";
        CardsGenerated = new List<CardScript>();
        ColorsGenerated = new List<Color>();

        if (!colorCollisionCounts.ContainsKey(Color.red))
        colorCollisionCounts[Color.red] = 0;
        if (!colorCollisionCounts.ContainsKey(Color.blue))
        colorCollisionCounts[Color.blue] = 0;
        if (!colorCollisionCounts.ContainsKey(Color.green))
        colorCollisionCounts[Color.green] = 0;

    }
    void Update()
    {
        if (CardCount < 6)
        {
            try
            {
                // Instantiate(Card, Placeholders[Index]);
                GameObject O = Instantiate(Card);
                O.transform.position = Placeholders[Index].position;
                colorCards.Add(O);
                Cards.Add(O);
                ListsOfCreatedColors.Add(O);
                Index++;
                CardCount++;
                Debug.Log("CardTimer: " + CardTimer);
            }
            catch (Exception Ex)
            {
            }
        }
        if (CardCount == 6) //check to see if all colors are done generating
        {
            for (int i = 0; i < CardCount; i++)
            {
                CardScript cs = colorCards[i].GetComponent<CardScript>();

                if (cs.doneGeneratingColors == true && CardsGenerated.Count < 6&&cs.CardColorIsVerified==true)
                {
                    cs.CardColorIsVerified = false;
                    numberOfColorsGenerated++;
                    globalNumberOfColorsGenerated++;
                    CardTimer++;
                    CardsGenerated.Add(cs);
                    // //Debug.Log("Number Of Colors Generatedt: " + numberOfColorsGenerated);
                    //colorCards[i].GetComponent<CardScript>().doneGeneratingColors = false;
                }
            }
            ////Debug.Log("CardCount = 6");
        }

        if (numberOfColorsGenerated == 6)
        {
            // //Debug.Log("Cards with correct colors: " + numberOfColorsGenerated);
            CheckColors(Cards, ref AmountOfReds, ref AmountOfGreens, ref AmountofBlues, ref CorrectAmountOfColors, ref numberOfColorsGenerated);
        }
        CountCards();

        
    }
    public void CountCards()
    {
        if (ReadyText != null&&GameImage!=null)
        {
            if (CardTimer == CardMax)
            {
                ReadyText.text = "Game Start!";
                GameImage.gameObject.SetActive(false);
            }
            else
            {
                ReadyText.text = CardTimer.ToString();
            }
       }
    }
    public static void addToRedCount(int value)
    {
        int CurrentValue = int.Parse(redTextCount.text);
        if (value == 0)
        {
            CurrentValue = 0;
        }
        else
        {
            CurrentValue += value;
        }
        redTextCount.text = CurrentValue.ToString();
        //Debug.Log("Added a value to red. Current Red value: " + CurrentValue);
    }
    public static void addToBlueCount(int value)
    {
        int CurrentValue = int.Parse(blueTextCount.text);
         if (value == 0)
        {
            CurrentValue = 0;
        }
        else
        {
            CurrentValue += value;
        }
        blueTextCount.text = CurrentValue.ToString();
        //Debug.Log("Added a value to blue. Current blue value: "+CurrentValue);
    }
    public static void addToGreenCount(int value)
    {
        int CurrentValue = int.Parse(greenTextCount.text);
         if (value == 0)
        {
            CurrentValue = 0;
        }
        else
        {
            CurrentValue += value;
        }
        greenTextCount.text = CurrentValue.ToString();
        //Debug.Log("Added a value to green. Current green value: " + CurrentValue);
    }
    // public static void addToYellowCount(int value)
    // {
    //     int CurrentValue = int.Parse(yellowTextCount.text);
    //     CurrentValue += value;
    //     yellowTextCount.text = CurrentValue.ToString();
    // }
    public static bool CheckColorDistribution(List<GameObject> Cards, ref int AmountOfReds, ref int AmountOfGreens, ref int AmountofBlues, ref bool result)
    {
        int MaxAmountOfRed = 2;
        int MaxAmountOfBlue = 2;
        int MaxAmountOfGreen = 2;
        
        for (int j = 0; j < Cards.Count; j++)
            {
                if(result == true)
                {
                    break;
                }
                GameObject Card = Cards[j];
                if (Card.gameObject.tag == "Red1")
                {
                    AmountOfReds++;
                }
                else if (Card.gameObject.tag == "Blue1")
                {
                    AmountofBlues++;
                }
                else if (Card.gameObject.tag == "Green1")
                {
                    AmountOfGreens++;
                }
            }
            if (AmountOfReds > MaxAmountOfRed)
            {
                Debug.Log("Amount of reds is > 2.\n Value: " + AmountOfReds);
            }
            if (AmountofBlues > MaxAmountOfBlue)
            {
                Debug.Log("Amount of blues is > 2.\n Value: " + AmountofBlues);
            }
            if (AmountOfGreens > MaxAmountOfGreen)
            {
                Debug.Log("Amount of greens is > 2.\n Value: " + AmountOfGreens);
            }
            if (AmountOfReds != 2 || AmountofBlues != 2 || AmountOfGreens != 2) 
            {
                result = false;
            }
        return result;
    }
    public void CheckColors(List<GameObject> Cards, ref int AmountOfReds, ref int AmountOfGreens, ref int AmountofBlues, ref bool result, ref int numberOfColorsGenerated)
    {
        int max = 6;
        bool distributionResult;
        for (int i = 0; i < max; i++)
        {
            try
            {
                GameObject Card = Cards[i];
                if (Card.gameObject.tag == "Red1")
                {
                    //Debug.Log("color is red");
                    addToRedCount(1);
                    Cards.RemoveAt(i);
                }
                else if (Card.gameObject.tag == "Blue1")
                {
                    //Debug.Log("color is blue");
                    addToBlueCount(1);
                    Cards.RemoveAt(i);
                }
                else if (Card.gameObject.tag == "Green1")
                {
                    //Debug.Log("color is green");
                    addToGreenCount(1);
                    Cards.RemoveAt(i);
                }
            }
            catch (Exception e)
            {

            }



        }
        distributionResult = CheckColorDistribution(Cards, ref AmountOfReds, ref AmountOfGreens, ref AmountofBlues, ref result);
        if (distributionResult == false)
        {
            //ResetColorGen();
            //addToRedCount(0);
            //addToBlueCount(0);

            //addToGreenCount(0);
        }
        for (int i = 0; i < colors.Count; i++)
        {
        }
    }
    public void ResetColorGen()
    {

            for (int i = 0; i < CardCount; i++)
            {
                CardScript cs = colorCards[i].GetComponent<CardScript>();
                cs.doneGeneratingColors = false;
                cs.CardColorIsVerified = false;
                CardTimer = 0;
                numberOfColorsGenerated = 0;
                //CardsGenerated = new List<CardScript>();
            }
    }
    public void CheckNumberOfGenColors()
    {
        int count = 0;
        for (int i = 0; i < colorCards.Count; i++)
        {
            if (colorCards[i].GetComponent<CardScript>().cardColorGenerated == true)
            {
                count++;
            }
        }
        if (count == 6)
        {

        }
        //gameInformation.text = "Start!";
    }
    public static Color CreateColor()
    {
        int AmountOfReds = 0;
        int AmountOfBlues = 0;
        int AmountOfGreens = 0;
        for (int i = 0; i < ListsOfCreatedColors.Count; i++)
        {
            CardScript cs = ListsOfCreatedColors[i].GetComponent<CardScript>();
            Color c = cs.globalGeneratedColor;
            if (c == Color.red)
                AmountOfReds++;
            else if (c == Color.blue)
                AmountOfBlues++;
            else if (c == Color.green)
                AmountOfGreens++;
        }
        if (AmountOfReds < 2)
        {
            color = Color.red;
        }
        else if (AmountOfBlues < 2)
        {
            color = Color.blue;
        }
        else if(AmountOfGreens < 2)
        {
            color = Color.green;
        }
        return color;
    }

//overload  version
public static Color CreateColor(string colorName)
{
    colorName = colorName.ToLower(); 

    if (colorName == "red")
        return Color.red;
    else if (colorName == "blue")
        return Color.blue;
    else if (colorName == "green")
        return Color.green;
    else
    {
        Debug.LogWarning("Unknown color name.");
        return CreateColor(); //goes back to regular version
    }
}

    // public static Color CreateColor()
    // {
    //     color = new Color(UnityEngine.Random.Range(0, 2), UnityEngine.Random.Range(0, 2), UnityEngine.Random.Range(0, 2));
    //     return color;
    // }
}

















// using UnityEngine;

// public class GenerateColor : MonoBehaviour
// {
//     public static Color color;
//     void Start()
//     {

//     }

//     void Update()
//     {
//         color = new Color(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2));
//     }
// }
