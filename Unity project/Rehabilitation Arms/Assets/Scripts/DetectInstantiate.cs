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
    public bool hasPowerup;
    public GameObject powerupIndicator;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public GameObject boxPrefab;
    private const int MAX_BOXES = 5;

    //public SphereCollider col;
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        middleware = GameObject.Find("Ground").GetComponent<Startup>().Middleware;
        print("Detection is alive");
    }

    // Update is called once per frame
    void Update()
    {
        int numBoxes = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (numBoxes > MAX_BOXES)
        {
            GameOver();
            Time.timeScale = 0;
        }
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
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
        else if (other.gameObject.CompareTag("Spawn Rock"))
            {

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
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameOver();
            Time.timeScale = 0;

        }
    }


    IEnumerator PowerupCountdwonRoutine()
    {
        yield return new WaitForSeconds(12);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);

    }
    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "" + score;
    }
}


