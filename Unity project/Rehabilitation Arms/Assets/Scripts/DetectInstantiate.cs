using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing.Text;
using Middleware;

public class DetectInstantiate : MonoBehaviour
{
  
    private Vector3 offset = new Vector3(0, -0.5f, 1);
    private ImuDataConnector middleware;
    public GameObject ProjectilePrefab;
    public int rockTimerMax = 200;
    private int rockTimer = 0;
    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    private const int MAX_BOXES = 5;
    public TextMeshProUGUI highScoreText;
    

    //public SphereCollider col;
    // Start is called before the first frame update
    void Start()
    {
        rockTimer = 0;
        isGameActive = true;
        middleware = GameObject.Find("Ground").GetComponent<Startup>().Middleware;
        middleware.NotifyStart();
        print("Detection is alive");
        //UpdateHighScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        rockTimer--;
        int numBoxes = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (numBoxes > MAX_BOXES)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        //CheckHighScore();
        Time.timeScale = 0;
        gameOverText.gameObject.SetActive(true);
        middleware.NotifyEnd();
        isGameActive = false;
        gameOverText.text = "Gameover!!! Your score is " + scoreText.text;
    }
    private void OnTriggerEnter(Collider other)
    {
        middleware.NotifyEvent();

        if (other.gameObject.CompareTag("Banana"))
        {
            Destroy(other.gameObject);
            if (hasPowerup)
            {
                UpdateScore(2);
            }
            else
            {
                UpdateScore(1);
            }
        }
        else if (other.gameObject.CompareTag("Spawn Rock") && rockTimer <= 0)
            {
                rockTimer = rockTimerMax;
                Instantiate(ProjectilePrefab, transform.position + offset, ProjectilePrefab.transform.rotation);
            }
            else if (other.gameObject.CompareTag("Powerup"))
            {
                hasPowerup = true;
                powerupIndicator.gameObject.SetActive(true);
                Destroy(other.gameObject);
                StartCoroutine(PowerupCountdwonRoutine());
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                GameOver();
            }
        }
    //void CheckHighScore()
    //{
    //    if (score > PlayerPrefs.GetInt("HighScoreValue", 0))
    //    {
    //        PlayerPrefs.SetInt("HighScoreValue", score);
    //    }
    //}

    //void UpdateHighScoreText()
    //{
    //    highScoreText.text = $"HighScore: {PlayerPrefs.GetInt("HighScoreValue")}";
    //}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameOver();
            
        }
    }
    IEnumerator PowerupCountdwonRoutine()
    {
        yield return new WaitForSeconds(10);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "" + score;
    }
}


