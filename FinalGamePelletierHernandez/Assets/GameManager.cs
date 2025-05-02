using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    //Array of potions, red = 0, green = 1, blue = 2
    public int[] potions = new int[3] { 0, 0, 0 };

    public int coins = 0;

    float startCountdownValue = 10f;

    //UI Elements
    public GameObject mainMenuPanel;
    public GameObject gameOverPanel;
    public GameObject youWinPanel;
    public GameObject pickupTextGroup;
    public GameObject invincibleCountdownParent;
    public TMP_Text redPotionsText;
    public TMP_Text bluePotionsText;
    public TMP_Text greenPotionsText;
    public TMP_Text coinsText;
    public TMP_Text healthText;
    public TMP_Text invincibleCountdownText;


    public void UpdateData(int hp)
    { 
        redPotionsText.text = "Red Potions: " + potions[0];
        greenPotionsText.text = "Green Potions: " + potions[1];
        bluePotionsText.text = "Blue Potions: " + potions[2];
        coinsText.text = "Coins: " + coins;
        healthText.text = "Health: " + hp;
    }
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
        //Hides In Game UI 
        gameOverPanel.SetActive(false);
        youWinPanel.SetActive(false);
        pickupTextGroup.SetActive(false);
    }

    public void HandleGameOver()
    {
        gameOverPanel.SetActive(true);

        //Reactivates Cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CheckLevelWin()
    {
        if(coins == 10)
        {
            youWinPanel.SetActive(true);

            coins = 0;

            //Reactivates Cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    public void Restart()
    {
        //Hides In Game UI 
        gameOverPanel.SetActive(false);
        youWinPanel.SetActive(false);
        pickupTextGroup.SetActive(false);

        //makes Main Menu visable again
        mainMenuPanel.SetActive(true);

        //Reactivates Cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        //Activates all of the in game UI except for invincibility countdown
        pickupTextGroup.SetActive(true);
        invincibleCountdownParent.SetActive(false);

        //Disables Main Menu UI
        mainMenuPanel.SetActive(false);

        //Hides Curser
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("LevelOne", LoadSceneMode.Single);
    }

    public void GoToNextLevel()
    {
        //Hides Curser
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Hides you win panel again
        youWinPanel.SetActive(false);

        //Get next scene index from build profile
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        //Checks to see if there are any more levels and if it is the last one, sends you back to the main menu
        if(SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Restart();
        }
    }

    public void EndGame()
    {
        //Quit Game
        Application.Quit();

        Debug.Log("Quitting Game");
    }


    public void StartInvincibility()
    {
        invincibleCountdownParent.SetActive(true);
        StartCoroutine(InvincibleCountdown(startCountdownValue));
    }

    IEnumerator InvincibleCountdown(float countdownValue)
    {
        startCountdownValue = countdownValue;
        while(countdownValue > 0)
        {
            invincibleCountdownText.text = "Invincibility Left: " + countdownValue + " seconds";
            yield return new WaitForSeconds(1f);
            countdownValue--;
        }
        if(countdownValue == 0)
        {
            invincibleCountdownParent.SetActive(false);
        }
    }

}
