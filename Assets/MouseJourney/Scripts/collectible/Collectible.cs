using Unity.VisualScripting;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    //Rotation speed
    public float rotationSpeed;

    //Effect to apply to the object
    public GameObject onCollectEffect;

    [SerializeField]
    private int point;  

    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.isGameActive)
        {

            if (gameObject.CompareTag("good"))
            {
                AudioManager.Instance.PlaySFX("GoodFood");
            } else if(gameObject.CompareTag("bad"))
            {
                AudioManager.Instance.PlaySFX("BadFood");
            }
            //Intanciate the particule effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);
         
            //Destroy the collectible
            Destroy(gameObject);

            //update total
            gameManager.updateTotalCollected(gameObject);
            gameManager.UpdateText();
        }
    }

}
