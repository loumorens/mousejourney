using UnityEngine;

public class CountCollectible : MonoBehaviour
{
    public int goodFoodCollected {get; private set;}
    
    public int badFoodCollected {get; private set;}


    public void updateTotalCollected(GameObject obj){        
        if(obj.CompareTag("good")){
            goodFoodCollected += 1;
        } else if(obj.CompareTag("bad")){
            badFoodCollected += 1;
        } 
        Debug.Log("CountCollectible::updateTotalCollected::goodFoodCollected = " + goodFoodCollected +" ; badFoodCollected = " + badFoodCollected);
        
    }

    public void updateBadFoodCollected(int i){
        badFoodCollected = i;
    }
}
