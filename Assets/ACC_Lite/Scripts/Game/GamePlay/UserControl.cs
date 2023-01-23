using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For user multiplatform control.
/// </summary>
[RequireComponent (typeof (CarController))]
public class UserControl :MonoBehaviour
{

	CarController ControlledCar;

	public float Horizontal { get; private set; }
	public float Vertical { get; private set; }
	public bool Brake { get; private set; }

	public static MobileControlUI CurrentUIControl { get; set; }
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

                break;
        }
    }
	private void Awake ()
	{
		ControlledCar = GetComponent<CarController> ();
		CurrentUIControl = FindObjectOfType<MobileControlUI> ();
	}

	void Update ()
	{
		Vector2 input  = new Vector2();
        float speed = 1.20f;
        if (up)
        {
            input += new Vector2(0, 1);
        }
        if (down)
        {
            input -= new Vector2(0, 1);
        }

        if (left)
        {
            input -= new Vector2(1,0);
        }
        if (right)
        {
            input += new Vector2(1, 0);
        }
		/*if (CurrentUIControl != null && CurrentUIControl.ControlInUse)
		{
			//Mobile control.
			Horizontal = CurrentUIControl.GetHorizontalAxis;
			Vertical = CurrentUIControl.GetVerticalAxis;
		}
		else
		{
			//Standart input control (Keyboard or gamepad).
			Horizontal = Input.GetAxis ("Horizontal");
			Vertical = Input.GetAxis ("Vertical");
			Brake = Input.GetButton ("Jump");
		}*/
		Horizontal = input.x;
		Vertical = input.y;

		//Apply control for controlled car.
		ControlledCar.UpdateControls (Horizontal, Vertical, Brake);
	}
}
