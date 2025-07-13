using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[DefaultExecutionOrder(999)]
public class GameManager : MonoBehaviour
{

    public bool isGameActive;

    private int nbGoodFoodToCollectToWinTheLevel;

    private int nbBadFoodToCollectToLostOneLife;

    private int goodFoodTotal;
    private int badFoodTotal;

    private int nbLifeMax = 3;
    private int lifeLost = 3;

    [SerializeField]
    private GameObject[] planeWithCollectible;

    public CountCollectible counter;

    //UI top info
    public TextMeshProUGUI totalGood;
    public TextMeshProUGUI totalBad;
    public TextMeshProUGUI life;

    //UI restart
    public Button restartButton;
    // UI Got To Main Menu
    public Button goToMainMenu;
    //UI life lost
    public TextMeshProUGUI lifeLostText;
    //UI nextLevelCanvas
    public Canvas nextLevelCanvas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameActive = true;
        counter = new CountCollectible();
        initiateDatasFoodCollectible();
        UpdateTextLife(lifeLost);
        UpdateTextTotalGood(0);
        UpdateTextTotalBad(0);
        restartButton.gameObject.SetActive(false);
        goToMainMenu.gameObject.SetActive(false);
        lifeLostText.gameObject.SetActive(false);
        nextLevelCanvas.gameObject.SetActive(false);
    }


    //Initialize number collectible to collect
    private void updateGoodFoodTotal(GameObject obj)
    {
        goodFoodTotal += obj.GetComponent<SpawnControllerOnPlane>().numberGoodFoodSpawned;
        Debug.Log("sceneController::updateGoodFoodTotal::goodFoodTotal = " + goodFoodTotal);
    }

    private void updateBadFoodTotal(GameObject obj)
    {
        badFoodTotal += obj.GetComponent<SpawnControllerOnPlane>().numberBadFoodSpawned;
        Debug.Log("sceneController::updateBadFoodTotal::badFoodTotal = " + badFoodTotal);
    }

    private void InitiateNbGoodFoodToCollectToWinTheLevel()
    {
        nbGoodFoodToCollectToWinTheLevel = Mathf.RoundToInt(goodFoodTotal / 2);
        Debug.Log("sceneController::updateNInitiateNbGoodFoodToCollectToWinTheLevelbGoodFoodToCollectToWinTheLevel::nbGoodFoodToCollectToWinTheLevel = " + nbGoodFoodToCollectToWinTheLevel);

    }

    private void InitiateNbBadFoodToCollectToLostOneLife()
    {
        nbBadFoodToCollectToLostOneLife = Mathf.RoundToInt(badFoodTotal / 4);
        Debug.Log("sceneController::InitiateNbBadFoodToCollectToLostOneLife::nbBadFoodToCollectToLostOneLife = " + nbBadFoodToCollectToLostOneLife);
    }

    private void initiateDatasFoodCollectible()
    {
        for (int i = 0; i < planeWithCollectible.Length; i++)
        {
            Debug.Log("sceneController::initiateDatasFoodCollectible::planeWithCollectible[i] = " + planeWithCollectible[i].name);
            updateGoodFoodTotal(planeWithCollectible[i]);
            updateBadFoodTotal(planeWithCollectible[i]);
        }

        InitiateNbGoodFoodToCollectToWinTheLevel();
        InitiateNbBadFoodToCollectToLostOneLife();
    }

    //UI
    public void UpdateTextTotalGood(int number)
    {
        totalGood.text = "Total good : " + number + " / " + nbGoodFoodToCollectToWinTheLevel;
    }

    public void UpdateTextTotalBad(int number)
    {
        totalBad.text = "Total bad : " + number + " / " + nbBadFoodToCollectToLostOneLife;
    }

    public void UpdateTextLife(int number)
    {
        life.text = "Life : " + number + " / " + nbLifeMax;
    }

    public void UpdateText()
    {
        UpdateTextTotalGood(counter.goodFoodCollected);
        UpdateTextTotalBad(counter.badFoodCollected);
        UpdateTextLife(lifeLost);
    }

    //CountCollectible
    public void updateTotalCollected(GameObject gameObject)
    {
        counter.updateTotalCollected(gameObject);
        if (counter.goodFoodCollected == nbGoodFoodToCollectToWinTheLevel)
        {
            Debug.Log("Level win. Go to next level.");
            nextLevelCanvas.gameObject.SetActive(true);
            goToMainMenu.gameObject.SetActive(true);
        }
        if (counter.badFoodCollected == nbBadFoodToCollectToLostOneLife)
        {
            if (lifeLost > 0)
            {
                int nblife = nbLifeMax - lifeLost;
                Debug.Log("One life lost. You have : " + nblife);
                lifeLost--;
                nbBadFoodToCollectToLostOneLife = nbBadFoodToCollectToLostOneLife / 2;
                counter.updateBadFoodCollected(0);
                StartCoroutine(DisplayLifeLost());
            }
            else
            {
                Debug.Log("Level Lost. Restart Level or go to main menu.");
                isGameActive = false;
                restartButton.gameObject.SetActive(true);
                goToMainMenu.gameObject.SetActive(true);
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator DisplayLifeLost()
    {
        lifeLostText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        lifeLostText.gameObject.SetActive(false);
    }
}
