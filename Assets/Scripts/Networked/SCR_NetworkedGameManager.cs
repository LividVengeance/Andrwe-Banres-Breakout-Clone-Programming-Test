using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_NetworkedGameManager : NetworkBehaviour
{
    public static SCR_NetworkedGameManager instance;
    
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SyncVar] private int currentScore;
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    
    // Update is called once per frame
    void Update() => scoreLabel.text = currentScore.ToString();

    public void IncreasePlayerScore(int value)
    {
        currentScore += value;
        
        // Updates the UI
        scoreLabel.text = currentScore.ToString();
    }

    public void ResetLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
