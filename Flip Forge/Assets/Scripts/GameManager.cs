using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance

    private int moneyCollectedCount = 0; // Variable to keep track of collected money
    private int intLives = 3; // Variable to keep track of player lives
    private static int deathCount = 0; // made this static so it doesn't get changed on each level reload

    [SerializeField] private TextMeshProUGUI moneyCollectedText;
    [SerializeField] private TextMeshProUGUI livesText;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject adPopupPanel; 

    private void OnEnable()
    {
        UnpauseGameOnRestart(); // Unpause the game when the script is enabled
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the scene loaded event
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this; // Set the singleton instance
        DontDestroyOnLoad(gameObject); // Prevent this GameObject from being destroyed when loading a new scene
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        moneyCollectedCount = 0; // Reset money collected count when a new scene is loaded
        intLives = 3; // Reset player lives when a new scene is loaded

        // Find UI elements in the new scene
        moneyCollectedText = GameObject.Find("MoneyText")?.GetComponent<TextMeshProUGUI>();
        livesText = GameObject.Find("LivesText")?.GetComponent<TextMeshProUGUI>();


        // Update UI with current values
        moneyCollectedText?.SetText(moneyCollectedCount.ToString());
        livesText?.SetText(intLives.ToString());

        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverScreen");
        adPopupPanel = GameObject.FindGameObjectWithTag("AdPopup");

        // Avoid null references by checking before setting active
        gameOverPanel?.SetActive(false);
        adPopupPanel?.SetActive(false);
    }
    public void OnMoneyCollected()
    {
        moneyCollectedCount++; // Increment the count when money is collected
        moneyCollectedText.text = moneyCollectedCount.ToString(); // Update the UI text with the new count

        AudioManager.Instance.PlayCollectingSound(); // Play the collecting sound
    }

    public void OnDamageTaken()
    {
        intLives--; // Decrement the lives count when damage is taken
        livesText.text = intLives.ToString(); // Update the UI text with the new lives count

        AudioManager.Instance.PlayDamageSound(); // Play the damage sound

        Debug.Log("Lives left: " + intLives); // Log the remaining lives for debugging

        if (intLives <= 0)
        {
            deathCount++; // Increment the death count
            Debug.Log("Death Count: " + deathCount); // Log the death count for debugging
            if (deathCount % 3 == 0)
            {
                adPopupPanel?.SetActive(true);
                PauseGameOnDeath(); // Pause the game when ad popup is shown
                Debug.Log("Ad Popup shown after " + deathCount + " deaths!"); // Log ad popup message
            }
            else { 
                gameOverPanel?.SetActive(true); // Activate the Game Over screen

                PauseGameOnDeath(); // Pause the game when player dies

                Debug.Log("Game Over!"); // Log game over message
            }
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
        }
    }

    private void PauseGameOnDeath()
    {
        Time.timeScale = 0f; // Pause the game
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }

    private void UnpauseGameOnRestart()
    {
        Time.timeScale = 1f; // Unpause the game
        adPopupPanel?.SetActive(false); // Hide the ad popup panel
    }

    //all button clicks
    public void OnPlayClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnQuitClick()
    {
        Application.Quit(); // Quit the application
    }

    public void OnRestartClick()
    {
        moneyCollectedCount = 0; // Reset money collected count
        intLives = 3; // Reset player lives

        // Update UI immediately
        if (moneyCollectedText != null) moneyCollectedText.text = "0";
        if (livesText != null) livesText.text = "3";

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the scene
        UnpauseGameOnRestart(); // Unpause the game when restarted
    }

    public void OnQuitToMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    public void OnAdWatchClicked()
    {
        // Find active ad panel in current scene
        GameObject currentAdPanel = GameObject.FindGameObjectWithTag("AdPopup");
        if (currentAdPanel != null) currentAdPanel.SetActive(false);
        UnpauseGameOnRestart(); // Unpause the game when ad is watched
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from the scene loaded event
    }
}
