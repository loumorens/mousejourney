
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControllerOnPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnObjectsPrefab;

    [SerializeField]
    private int numberObjectToSpawn = 10;

    [SerializeField]
    private float objectHeight = 0.03f;

    private Transform plane;

    private float planeLenght;
    private float planeWidth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plane = gameObject.transform;
        planeLenght = plane.GetComponent<MeshRenderer>().bounds.size.x / 2;
        planeWidth = plane.GetComponent<MeshRenderer>().bounds.size.z / 2;
        spawnInArray();
    }



    void spawnInArray()
    {
         for (int i = 0; i < numberObjectToSpawn; i++)
        {
            GameObject objToSpawn = spawnObjectsPrefab[Random.Range(0, spawnObjectsPrefab.Length)];
            Debug.Log("SpawnControllerOnPlane::Start::Object to Spawn selected : " + objToSpawn.name);
            Spawn(objToSpawn);
        }
    }
    // Update is called once per frame
    void Spawn(GameObject objToSpawn)
    {
        float posX = Random.Range(-planeLenght, planeLenght) + plane.position.x;
        float posZ = Random.Range(-planeWidth, planeWidth) + plane.position.z;
        Instantiate(objToSpawn, new Vector3(posX,  + objectHeight, posZ), objToSpawn.transform.rotation);
        Debug.Log("SpawnControllerOnPlane::Spawn:: " + objToSpawn.name + "instanciated at posX :"+posX+" and posZ:"+posZ);
    }
}
