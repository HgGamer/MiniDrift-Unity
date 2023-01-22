using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject cubeInstance;
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
    void PlayerJoin(string player)
    {
        //Debug.Log("player joined __" + "asd");
        Debug.Log("player joined __" );

        var instance = Instantiate(cubeInstance);
        instance.transform.name = player;
        instance.transform.parent = transform;

        Debug.Log("player joined __" + "dsaasd");

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
