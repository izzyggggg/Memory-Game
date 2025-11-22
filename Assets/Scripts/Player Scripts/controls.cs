using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using TMPro; //space button text
public class controls : MonoBehaviour
{
    public float speed;
    bool hasBlue1;
    bool canMove = false;
    public TextMeshProUGUI pressSpaceText;
    int blueCount;
    public Text ReadyText;
    public Text InstructionsText;
    public Image GameImage;
    public GameObject pauseMenu;
    public Button pauseButton;
    public TextMeshProUGUI scoreText;

    private bool isPaused = false;
    private int playerScore = 0; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
{
    blueCount = 0;

    if (PlayerPrefs.HasKey("PlayerSpeed"))
    {
        speed = PlayerPrefs.GetFloat("PlayerSpeed");
        Debug.Log("Player speed: " + speed);
        PlayerPrefs.DeleteKey("PlayerSpeed");
    }
    else
    {
        if (speed == 0)
        {
            speed = 5;
        }
        PlayerPrefs.SetFloat("PlayerSpeed", 5);
        Debug.Log("Could not find key");
    }
        scoreText.text = "";
    pauseMenu.SetActive(false);
    //pauseButton.onClick.AddListener(TogglePause);
}


    // Update is called once per frame
    void Update()
    {
    PauseButton();
    if (canMove)
    {
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * speed * Time.deltaTime);
    }

    if (Input.GetKeyDown(KeyCode.UpArrow))
    {
        speed += 1f;
        Debug.Log("Speed: " + speed);
    }

    if (Input.GetKeyDown(KeyCode.DownArrow))
    {
        speed -= 1f;
        Debug.Log("Speed: " + speed);
    }
 
    if (Input.GetKeyDown(KeyCode.Space))
    {
        canMove = true;
        Debug.Log("Player can now move!");
        ReadyText.gameObject.SetActive(false);
            GameImage.gameObject.SetActive(false);
            InstructionsText.gameObject.SetActive(false);
        if (pressSpaceText != null)
        {
            pressSpaceText.gameObject.SetActive(false);
        }
    }
}


    void OnCollisionEnter(Collision other)
    {
        string colorName = "Unknown";
        if (other.gameObject.tag == "Blue1")
        {
            colorName = "Blue";
            // other.gameObject.tag = "None";
            // Color color = new Color(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2));
            // other.gameObject.GetComponent<Renderer>().material.color = GenerateColor.color;
            // blueCount++;
        }
        if (other.gameObject.tag == "Red1")
        {
            colorName = "Red";
            // other.gameObject.tag = "None";
            // Color color = new Color(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2));
            // other.gameObject.GetComponent<Renderer>().material.color = GenerateColor.color;
            // redCount++;
        }
        if (other.gameObject.tag == "Green1")
        {
            colorName = "Green";
            // other.gameObject.tag = "None";
            // Color color = new Color(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2));
            // other.gameObject.GetComponent<Renderer>().material.color = GenerateColor.color;
            // greenCount++;
        }
        Color collidedColor = other.gameObject.GetComponent<Renderer>().material.color;

        if (GenerateColor.colorCollisionCounts.ContainsKey(collidedColor))
        {
            GenerateColor.colorCollisionCounts[collidedColor]++;
        }
        else
        {
            GenerateColor.colorCollisionCounts[collidedColor] = 1;
        }


        // if (collidedColor == Color.red)
        //     colorName = "Red";
        // else if (collidedColor == Color.blue)
        //     colorName = "Blue";
        // else if (collidedColor == Color.green)
        //     colorName = "Green";

        Debug.Log(colorName + " collisions: " + GenerateColor.colorCollisionCounts[collidedColor]);

        if (blueCount == 2)
        {
            Debug.Log("Blue cards matched");
            hasBlue1 = false;
        }
    }   
    public void PauseButton()
    {
         if(Input.GetKeyDown(KeyCode.P) == true)
        {
            TogglePause();
            Debug.Log("Pause button was pressed\n Pause " + isPaused);
        }
    }
    public void TogglePause()
    {
    if (isPaused)
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
        scoreText.text = "";
    }
    else
        {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        scoreText.text = "Score: " + playerScore.ToString();
        isPaused = true;
    }
}

}

// In Unity, to save data to local storage means to keep a player’s progress in their game to then be loaded again next time. 
// In order to do so, the player could use a piece of code, “PlayerPrefs,” which stores information such as speed, progress, or size on the player’s device. 
// With this system, Unity saves data even after the game is closed, and player can then continue from their previous progress when they play the game.
