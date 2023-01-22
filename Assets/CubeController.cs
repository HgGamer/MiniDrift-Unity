using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    bool up = false;
    bool down = false;
    bool left = false;
    bool right = false;
    void Start()
    {
        EventManager.OnPlayerInput += OnPlayerInput;
    }
    private void OnDisable()
    {
        EventManager.OnPlayerInput -= OnPlayerInput;
    }
    // Update is called once per frame
    void Update()
    {
        float speed = 1.20f;
        if (up)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (down)
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }

        if (left)
        {
            transform.position -= new Vector3(speed * Time.deltaTime,0, 0);
        }
        if (right)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }
    void OnPlayerInput(string input,string name)
    {
        if(name != transform.name)
        {
            return;
        }
        switch (input)
        {
            case "buttonup_down":
                up = true;
                break;
            case "buttonup_up":
                up = false;
                break;
            case "buttondown_down":
                down = true;
                break;
            case "buttondown_up":
                down = false;
                break;

            case "buttonleft_down":
                left = true;
                break;
            case "buttonleft_up":
                left = false;
                break;
            case "buttonright_down":
                right = true;
                break;
            case "buttonright_up":
                right = false;
                break;
            default:
                Debug.Log(input);
                Vector3 rot = new Vector3(float.Parse(input.Split(',')[0]), float.Parse(input.Split(',')[1]), float.Parse(input.Split(',')[2]));
                transform.eulerAngles = rot;
                break;
        }
    }
}
