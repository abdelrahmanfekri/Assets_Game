using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    int playerSpeed = 10;
    int playerScore = 0;
    int energyRed = 0, energyGreen = 0, energyBlue = 0;
    public GameObject redOrbPrefab, greenOrbPrefab, blueOrbPrefab, obstaclePrefab;
    public TMP_Text score_text, blue_energy_text, green_energy_text, red_energy_text;
    public Material redMaterial, greenMaterial, blueMaterial, blackMaterial;
    public Canvas pausingCanvas;
    public SoundManager soundManager;
    public SceneLoader sceneLoader;
    public GameObject shader;
    int maxEnergy = 5;
    int minEnergy = 0;
    int currentLane = 0;
    string currentForm = "Normal";
    bool gameOver = false;
    bool isPowerUp = false;
    bool isPowerBlueUp = false;
    int laneWidth = 5;
    int jumpHeight = 2;
    float createInterval = 1.0f;
    float lastCreatedTime = 0.0f;
    bool paused = false;
    Vector2 touchStart, touchEnd;

    void Start()
    {
        createOrbAndObst();
        lastCreatedTime = Time.time;
        pausingCanvas.enabled = false;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseTheGame();
        }
        if (!paused)
        {
            handleWindowMovement();
            handleMobileMovement();
            if (Time.time - lastCreatedTime > createInterval)
            {
                createOrbAndObst();
                lastCreatedTime = Time.time;
            }
            switchForm();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UsePower();
            }
        }
    }
    void handleWindowMovement()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            handleMoveRight();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            handleMoveLeft();
        }
    }

    void handleMobileMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEnd = touch.position;
                DetectSwipe();
            }
        }
        // if (Input.GetMouseButtonDown(0))
        // {
        //     touchStart = Input.mousePosition;
        // }
        // if (Input.GetMouseButtonUp(0))
        // {
        //     touchEnd = Input.mousePosition;
        //     DetectSwipe();
        // }
    }

    void DetectSwipe()
    {
        float swipeX = touchEnd.x - touchStart.x;
        if (Mathf.Abs(swipeX) >= 50f)
        {
            if (swipeX > 0)
            {
                handleMoveRight();
            }
            else
            {
                handleMoveLeft();
            }
        }
    }

    void handleMoveRight()
    {
        if (currentLane < 1)
        {
            currentLane += 1;
            MoveToLane(currentLane);
        }
    }
    void handleMoveLeft()
    {
        if (currentLane > -1)
        {
            currentLane -= 1;
            MoveToLane(currentLane);
        }
    }

    public void PauseTheGame()
    {
        pauseToggler(!paused);
    }
    public void pauseToggler(bool value)
    {
        pausingCanvas.enabled = value;
        paused = value;
        if (value)
        {
            Time.timeScale = 0;
            pausingCanvas.GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().Stop();
        }
        else
        {
            Time.timeScale = 1;
            pausingCanvas.GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().Play();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obst"))
        {
            ObstacleCollision();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("RedOrb"))
        {
            CollectOrb("Red");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("GreenOrb"))
        {
            CollectOrb("Green");
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.CompareTag("BlueOrb"))
        {
            CollectOrb("Blue");
            Destroy(collision.gameObject);

        }
    }

    void movePlayerForward()
    {
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * playerSpeed);
    }
    void MoveToLane(int targetLane)
    {
        transform.position = new Vector3(transform.position.x, 1, laneWidth * -1 * currentLane);
    }
    void switchForm()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SwitchToForm("Red");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SwitchToForm("Green");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SwitchToForm("Blue");
        }
    }

    public void SwitchToForm(string form)
    {
        // should change the player color and form
        if (form == "Red")
        {
            if (energyRed == maxEnergy)
            {
                currentForm = form;
                soundManager.playSwitchForm();
                changeToRed();
                decreasePlayerEnergy();
            }
            else
            {
                soundManager.playInvalidAction();
            }
        }
        if (form == "Green")
        {
            if (energyGreen == maxEnergy)
            {
                currentForm = form;
                soundManager.playSwitchForm();

                changeToGreen();
                decreasePlayerEnergy();

            }
            else
            {
                soundManager.playInvalidAction();
            }

        }
        if (form == "Blue")
        {
            if (energyBlue == maxEnergy)
            {
                currentForm = form;
                soundManager.playSwitchForm();
                changeToBlue();
                decreasePlayerEnergy();
            }
            else
            {
                soundManager.playInvalidAction();
            }
        }
        if (form == "Normal")
        {
            currentForm = form;
            soundManager.playSwitchForm();

            changeToBlack();
        }
        isPowerUp = false;
        deactivateShader();
    }

    void changeToRed()
    {
        GetComponent<Renderer>().material = redMaterial;
    }
    void changeToGreen()
    {
        GetComponent<Renderer>().material = greenMaterial;
    }
    void changeToBlue()
    {
        GetComponent<Renderer>().material = blueMaterial;
    }
    void changeToBlack()
    {
        GetComponent<Renderer>().material = blackMaterial;
    }

    void createOrbAndObst()
    {
        Vector3 position1 = new Vector3(60, 1, 0);
        Vector3 position2 = new Vector3(60, 1, 5);
        Vector3 position3 = new Vector3(60, 1, -5);
        int random1 = Random.Range(0, 5);
        int random2 = Random.Range(0, 5);
        int random3 = Random.Range(0, 5);
        if (random1 == 0)
        {
            Instantiate(redOrbPrefab, position1, Quaternion.identity);
        }
        if (random1 == 1)
        {
            Instantiate(greenOrbPrefab, position1, Quaternion.identity);
        }
        if (random1 == 2)
        {
            Instantiate(blueOrbPrefab, position1, Quaternion.identity);
        }
        if (random1 == 3)
        {
            Instantiate(obstaclePrefab, position1, Quaternion.identity);
        }
        if (random2 == 0)
        {
            Instantiate(redOrbPrefab, position2, Quaternion.identity);
        }
        if (random2 == 1)
        {
            Instantiate(greenOrbPrefab, position2, Quaternion.identity);
        }
        if (random2 == 2)
        {
            Instantiate(blueOrbPrefab, position2, Quaternion.identity);
        }
        if (random2 == 3)
        {
            Instantiate(obstaclePrefab, position2, Quaternion.identity);
        }
        if (random3 == 0)
        {
            Instantiate(redOrbPrefab, position3, Quaternion.identity);
        }
        if (random3 == 1)
        {
            Instantiate(greenOrbPrefab, position3, Quaternion.identity);
        }
        if (random3 == 2)
        {
            Instantiate(blueOrbPrefab, position3, Quaternion.identity);
        }
        if (random3 == 3 && random1 != 3 && random2 != 3)
        {
            Instantiate(obstaclePrefab, position3, Quaternion.identity);
        }
    }

    public void UsePower()
    {

        if (currentForm == "Red")
        {
            if (energyRed > minEnergy)
            {
                decreasePlayerEnergy();
                GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obst");
                foreach (GameObject obstacle in obstacles)
                {
                    Destroy(obstacle);
                }
                soundManager.playUsePower();
            }
            if (energyRed == minEnergy)
            {
                SwitchToForm("Normal");
            }
        }
        if (currentForm == "Green")
        {
            if (energyGreen > minEnergy && !isPowerUp)
            {
                decreasePlayerEnergy();
                isPowerUp = true;
                soundManager.playUsePower();
            }
            else
            {
                soundManager.playInvalidAction();
            }
            if (energyGreen == minEnergy)
            {
                SwitchToForm("Normal");
            }
        }
        if (currentForm == "Blue")
        {
            if (energyBlue > minEnergy && !isPowerBlueUp)
            {
                decreasePlayerEnergy();
                activateShader();
                soundManager.playUsePower();
            }
            else
            {
                soundManager.playInvalidAction();
            }
            if (energyBlue == minEnergy)
            {
                SwitchToForm("Normal");
            }
        }
        if (currentForm == "Normal")
        {
            soundManager.playInvalidAction();
        }
    }
    void GameOver()
    {
        gameOver = true;
        sceneLoader.LoadScene("GameOver", data: playerScore + "");
    }
    void CollectOrb(string orbType)
    {
        IncreasePlayerScore(orbType);
        IncreasePlayerEnergy(orbType);
        soundManager.playCollectOrb();
        isPowerUp = false;
    }
    void IncreasePlayerScore(string orbType)
    {
        int multiplier = 1;
        if (isPowerUp == true && currentForm == "Green")
        {
            multiplier = 5;
        }
        if (currentForm == orbType)
        {
            playerScore += 2 * multiplier;
        }
        else
        {
            playerScore += 1 * multiplier;
        }
        updateScore(playerScore);

    }

    void IncreasePlayerEnergy(string orbType)
    {
        int multiplier = 1;
        if (isPowerUp == true && currentForm == "Green")
        {
            multiplier = 2;
        }
        if (currentForm != orbType)
        {
            if (orbType == "Red")
            {
                energyRed += 1 * multiplier;
                // energy should not exceed maxEnergy
                if (energyRed > maxEnergy)
                {
                    energyRed = maxEnergy;
                }
            }
            if (orbType == "Green")
            {
                energyGreen += 1 * multiplier;
                if (energyGreen > maxEnergy)
                {
                    energyGreen = maxEnergy;
                }
            }
            if (orbType == "Blue")
            {
                energyBlue += 1 * multiplier;
                if (energyBlue > maxEnergy)
                {
                    energyBlue = maxEnergy;
                }
            }
        }
        updateBlueEnergy(energyBlue);
        updateGreenEnergy(energyGreen);
        updateRedEnergy(energyRed);
    }
    void decreasePlayerEnergy()
    {
        if (currentForm == "Red")
        {
            energyRed--;
        }
        if (currentForm == "Green")
        {
            energyGreen--;
        }
        if (currentForm == "Blue")
        {
            energyBlue--;
        }
        updateBlueEnergy(energyBlue);
        updateGreenEnergy(energyGreen);
        updateRedEnergy(energyRed);
    }
    void activateShader()
    {
        shader.SetActive(true);
        isPowerBlueUp = true;
    }
    void deactivateShader()
    {
        shader.SetActive(false);
        isPowerBlueUp = false;
    }
    void ObstacleCollision()
    {
        if (currentForm == "Normal")
        {
            GameOver();
        }
        else if (isPowerBlueUp && currentForm == "Blue")
        {
            deactivateShader();
        }
        else
        {
            SwitchToForm("Normal");
        }
        isPowerUp = false;
        isPowerBlueUp = false;
    }
    void updateScore(int score)
    {
        score_text.text = "Score: " + score;
    }

    void updateRedEnergy(int energy)
    {
        red_energy_text.text = energy + "";
    }

    void updateGreenEnergy(int energy)
    {
        green_energy_text.text = energy + "";
    }

    void updateBlueEnergy(int energy)
    {
        blue_energy_text.text = energy + "";
    }

}
