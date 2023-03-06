using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;
    public GameObject car3;
    public GameObject car4;
    public GameObject car5;
    public GameObject car6;
    public GameObject car7;
    void Start()
    {
        EventManager.OnPlayerJoin += PlayerJoin;
        EventManager.OnPlayerLeave += PlayerLeave;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerJoin -= PlayerJoin;
        EventManager.OnPlayerLeave -= PlayerLeave;
    }
    void PlayerJoin(string player,string properties)
    {
        
        Debug.Log("PlayerJoin: "+properties);
        GameObject instance = null; 
        if(properties == "1"){
            instance = Instantiate(car1);
        }
        if(properties == "2"){
            instance = Instantiate(car2);
        }
        if(properties == "3"){
            instance = Instantiate(car3);
        }
        if(properties == "4"){
            instance = Instantiate(car4);
        }
         if(properties == "5"){
            instance = Instantiate(car5);
        }
         if(properties == "6"){
            instance = Instantiate(car6);
        }
         if(properties == "7"){
            instance = Instantiate(car7);
        }
        
        instance.transform.name = player;
        instance.transform.parent = transform;
    }
    void PlayerLeave(string player)
    {
        foreach (Transform t in transform)
        {
            if (t.name == player)
            {
                Destroy(t.gameObject);
                return;
            }
        }

    }
}
