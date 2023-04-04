using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeController : MonoBehaviour
{
   
    RectTransform rt;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        EventManager.OnPlayerInput += OnPlayerInput;
    }
    private void OnDisable()
    {
        EventManager.OnPlayerInput -= OnPlayerInput;
    }

    Vector3 firstclick = Vector3.zero;
    Vector3 secondclick = Vector3.zero;
    bool registerNextClick = false;
    void OnPlayerInput(string input,string name)
    {

        if (name != transform.name)
        {
            Debug.Log("WrongName:"+  name+"|"+transform.name);
            return;
        }
        switch (input)
        {
            case "buttonup_down":
            case "buttonup_up":
                break;
            case "pointerbutton_down":
               
                
                break;
            case "pointerbutton_up":
                registerNextClick = true;
              
                break;
            case "buttondown_down":
            case "buttondown_up":
            case "buttonleft_down":
            case "buttonleft_up":
            case "buttonright_down":
            case "buttonright_up":
                break;
            default:
                if (input.Contains("null")|| input.Contains("undefined"))
                {
                    return;
                }
              
                Vector3 data = new Vector3(float.Parse(input.Split(',')[0]), float.Parse(input.Split(',')[1]), float.Parse(input.Split(',')[2]))/ 360.0f;
                
                
                if (firstclick == Vector3.zero)
                {
                   
                    if (registerNextClick)
                    {
                        Debug.Log("firstclick");
                        firstclick = data;
                        registerNextClick = false;
                    }

                    return;
                }
                if (secondclick == Vector3.zero)
                {
                    if (registerNextClick)
                    {
                        Debug.Log("secondclick");
                        secondclick = data;
                        registerNextClick = false;
                    }
                    return;
                }

                float x = (1 - ((data.x - secondclick.x) / (firstclick.x - secondclick.x))) * 1920;
                float y = (1 - ((data.y - secondclick.y) / (firstclick.y - secondclick.y))) * -1080;
                
                GetWorldPosition(new Vector2(x, y));
                break;
        }
    }
    GameObject fakelightInstance;
    public GameObject fakelight;
    void GetWorldPosition(Vector2 pos)
    {
        if (fakelightInstance == null)
        {
            fakelightInstance = Instantiate(fakelight);
        }
        rt.anchoredPosition = pos;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(pos+new Vector2(0, 1080)), out hit, Mathf.Infinity)) {
            fakelightInstance.transform.position = hit.point;
        };
        
    }
}
