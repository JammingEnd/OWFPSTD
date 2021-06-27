using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenemanager : MonoBehaviour
{

    /// <summary>
    /// lots of variables
    /// </summary>
    public float score = 0;
    public Text playerScoreUI;
    public Text resourceCountUI;
    public Text HealthCountUI;
    public Text wave;
    public float resourceCount;
    public float startingResources = 1500;
    private PlayerStats playerStats;
    public Camera mainCam;
    public Camera menuCam;
    private GameObject baseBuilding = null;
    public GameObject menuUI;
    public GameObject highScoreUI;
    public Text startbutton;
    public Text scoreTextBox;
    public int waveCounter;
    public GameObject[] allUI;
    private GameObject[] spawnObj;
    private List<Spawner> spawner = new List<Spawner>();
    private bool highscoreIsActive = false;
    private bool cantStart = false;

    /// <summary>
    /// sets everything active / not active for the menu screen. stops enemies from spawning en disables player UI
    /// </summary>
    private void Start()
    {
        resourceCount = startingResources;
        waveCounter = 1;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        allUI = GameObject.FindGameObjectsWithTag("UI");
        spawnObj = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (var obj in spawnObj)
        {
            spawner.Add(obj.GetComponent<Spawner>());
        }
        highScoreUI.SetActive(false);

        menuCam.enabled = true;
        mainCam.enabled = false;
        foreach (var UI in allUI)
        {
            UI.SetActive(false);
        }
    }

    /// <summary>
    /// updates the labels and increases the wave everytime the spawnlimited has been reached. detects if your base is destroyed, if so game over
    /// </summary>
    private void Update()
    {
        playerScoreUI.text = "Score:" + " " + score.ToString();
        resourceCountUI.text = "Resources: " + resourceCount.ToString();
        HealthCountUI.text = "Health: " + playerStats.currentHp.ToString();
        wave.text = "Wave: " + waveCounter.ToString();
        foreach (var spawner in spawner)
        {
            if (spawner.currentSpawned == spawner.maxSpawned)
            {
                waveCounter++;
                spawner.currentSpawned = 0;
                spawner.currentspawnrate -= spawner.currentspawnrate * (spawner.spawnRateDecay / 20);
                spawner.maxSpawned += Random.Range(1, 4);
            }
        }
        if (baseBuilding == null)
        {
            baseBuilding = GameObject.Find("base");
        }
        if (baseBuilding != null)
        {
            if (baseBuilding.GetComponent<Buildings>().currentHP <= 0)
            {
                GameOver();
            }
        }
        if (playerStats.currentHp <= 0)
        {
            GameOver();
        }


        if(Input.GetKeyDown(KeyCode.Backspace))
        {
          if(highscoreIsActive == true)
            {
                highscoreIsActive = false;
                menuUI.SetActive(true);
                highScoreUI.SetActive(false);
            }
        }
    }

    /// <summary>
    /// if youre health <= o then score adds to the database and UI's are disabled again
    /// </summary>
    public void GameOver()
    {
        cantStart = true;
        foreach (var UI in allUI)
        {
            UI.SetActive(false);
        }
        int run = 0;
        ScoreDatabase.scores.Add("Score #" + run++.ToString() + ": " + score.ToString());
        menuUI.SetActive(true);

        menuCam.enabled = true;
        mainCam.enabled = false;

    }
    /// <summary>
    /// initiates the game, UI's are on / off 
    /// </summary>
    public void StartGame()
    {
        if(cantStart != true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            menuUI.SetActive(false);
            menuCam.enabled = false;
            mainCam.enabled = true;
            foreach (var UI in allUI)
            {
                UI.SetActive(true);
            }
            foreach (var spawner in spawner)
            {
                spawner.StartSpawning();
            }
        }
       if(cantStart == true)
        {
            startbutton.text = "Exit the application en reload it again to play";
        }
    }

    /// <summary>
    /// press exit to exit
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// calls for the static list in scoredatabase
    /// </summary>
    public void HighScoreActive()
    {
        highscoreIsActive = true;
        menuUI.SetActive(false);
        highScoreUI.SetActive(true);
        scoreTextBox.text = ScoreDatabase.scores[0].ToString() + "\n" + ScoreDatabase.scores[1].ToString() + "\n" + ScoreDatabase.scores[2].ToString() + "\n" + ScoreDatabase.scores[3].ToString() + "\n" + ScoreDatabase.scores[4].ToString() + "\n" + ScoreDatabase.scores[5].ToString() + "\n" + ScoreDatabase.scores[6].ToString() + "\n" + ScoreDatabase.scores[7].ToString() + "\n" + ScoreDatabase.scores[8].ToString() + "\n" + ScoreDatabase.scores[9].ToString();
    }
}