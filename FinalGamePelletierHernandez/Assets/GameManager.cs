using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    //Array of potions, red = 0, green = 1, blue = 2
    public int[] potions = new int[3] { 0, 0, 0 };

    public int coins = 0;

    //UI Elements
    public GameObject gameOverPanel;
    public GameObject youWinPanel;
    public TMP_Text redPotionsText;
    public TMP_Text bluePotionsText;
    public TMP_Text greenPotionsText;
    public TMP_Text coinsText;
    public TMP_Text healthText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        //Hides UI 
        gameOverPanel.SetActive(false);
        youWinPanel.SetActive(false);

        //Hides Curser
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void HandleGameOver()
    {
        gameOverPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CheckLevelWin()
    {
        if(coins == 10)
        {
            youWinPanel.SetActive(true);
        }
    }
    
    public void Restart()
    {
        //Reloads Current Scene
        SceneManager.LoadScene("LevelOne", LoadSceneMode.Single);
    }

    public void GoToNextLevel()
    {
        Debug.Log("Next level");
    }

    public void EndGame()
    {
        //Quit Game
        Application.Quit();

        Debug.Log("Quitting Game");
    }

    public void UpdateData(int hp)
    {
        redPotionsText.text = "Red Potions: " + potions[0];
        greenPotionsText.text = "Green Potions: " + potions[1];
        bluePotionsText.text = "Blue Potions: " + potions[2];
        coinsText.text = "Coins: " + coins;
        healthText.text = "Health: " + hp;
    }

}
