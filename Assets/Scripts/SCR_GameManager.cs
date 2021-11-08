using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_GameManager : MonoBehaviour
{
    public static SCR_GameManager instance;

    [SerializeField] private TextMeshProUGUI scoreLabel;
    
    private int currentScore;
    

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void IncreasePlayerScore(int value)
    {
        currentScore += value;
        // Updates the UI
        scoreLabel.text = currentScore.ToString();
    }
    public int GetPlayerScore => currentScore;

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
