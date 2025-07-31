using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[DefaultExecutionOrder(999)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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
        //Awake();
        isGameActive = true;
        counter = GetComponent<CountCollectible>();
        initiateDatasFoodCollectible();
        try
        {
            UpdateTextLife(lifeLost);
            UpdateTextTotalGood(0);
            UpdateTextTotalBad(0);
            restartButton.gameObject.SetActive(false);
            goToMainMenu.gameObject.SetActive(false);
            lifeLostText.gameObject.SetActive(false);
            nextLevelCanvas.gameObject.SetActive(false);
        }
        catch
        {
            Debug.Log("GameManager::Start::catch");
        }
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
         if(totalGood != null)
        {
            totalGood.text = "Total good : " + number + " / " + nbGoodFoodToCollectToWinTheLevel;
        }
        else
        {
            Debug.Log("GameManager::UpdateTextTotalGood::totalGood is null");
        }
    }

    public void UpdateTextTotalBad(int number)
    {
        if(totalBad != null)
        {
            totalBad.text = "Total bad : " + number + " / " + nbBadFoodToCollectToLostOneLife;
        }
        else
        {
            Debug.Log("GameManager::UpdateTextTotalBad::totalBad is null");
        }
    }

    public void UpdateTextLife(int number)
    {
        Debug.Log("GameManager::UpdateTextLife::number:: " + number + " ::nbLifeMax:: " + nbLifeMax);
        
        if (life != null)
        {
            life.text = "Life : " + number + " / " + nbLifeMax;
        }
        else
        {
            Debug.Log("GameManager::UpdateTextLife::life is null");
        }
    }

    public void UpdateText()
    {
        UpdateTextTotalGood(counter.goodFoodCollected);
        UpdateTextTotalBad(counter.badFoodCollected);
    }

    //CountCollectible
    public void updateTotalCollected(GameObject gameObject)
    {
        counter.updateTotalCollected(gameObject);
        if (counter.goodFoodCollected == nbGoodFoodToCollectToWinTheLevel)
        {
            Debug.Log("Level win. Go to next level.");
            AudioManager.Instance.PlaySFX("LevelWin");
            nextLevelCanvas.gameObject.SetActive(true);
            goToMainMenu.gameObject.SetActive(true);
        }
        if (counter.badFoodCollected == nbBadFoodToCollectToLostOneLife)
        {
            if (lifeLost > 0)
            {
                int nblife = nbLifeMax - lifeLost;
                Debug.Log("One life lost. You have : " + nblife);
                AudioManager.Instance.PlaySFX("LiveLost");
                lifeLost--;
                UpdateTextLife(lifeLost);

                nbBadFoodToCollectToLostOneLife = nbBadFoodToCollectToLostOneLife / 2;
                counter.updateBadFoodCollected(0);
                StartCoroutine(DisplayLifeLost());
            }
            else
            {
                LevelLost();
            }
        }
    }

    private IEnumerator DisplayLifeLost()
    {
        lifeLostText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        lifeLostText.gameObject.SetActive(false);
    }

    public void LevelLost()
    {
        Debug.Log("Level Lost. Restart Level or go to main menu.");
        AudioManager.Instance.PlaySFX("LevelLost");

        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        goToMainMenu.gameObject.SetActive(true);
    }
}
