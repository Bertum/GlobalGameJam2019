using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    private static readonly string JOYSTICK_LEFT_ONE_HORIZONTAL = "JLH";//Joystick izquierdo - horizontal
    private static readonly string JOYSTICK_LEFT_ONE_VERTICAL = "JLV";//Joystick izquierdo - vertical
    private static readonly string JOYSTICK_A = "AB";//Joystick A button
    private static readonly string JOYSTICK_B = "BB";//Joystick B button
    private static readonly string JOYSTICK_X = "XB";//Joystick X button
    private static readonly string JOYSTICK_Y = "YB";//Joystick Y button

    public float maxHorizontalSpeed = 10f;
    public float maxVerticalSpeed = 10f;
    public int numberPlayer = 1;
    
    private Rigidbody2D rigidbodyComponent;

    private void Start()
    {

    }

    private void Awake()
    {
        this.rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move(GetValueJoystickLeftHorizontal(), GetValueJoystickLeftVertical());
    }

    private void Move(float horizontalMovement, float verticalMovement)
    {
        this.rigidbodyComponent.velocity = new Vector2(horizontalMovement * this.maxHorizontalSpeed, verticalMovement * this.maxVerticalSpeed);
    }

    private float GetAxis(string axisName)
    {
        return Input.GetAxis(axisName + GetNumberPlayer());
    }

    private bool GetButtonDown(string buttonName)
    {
        return Input.GetButtonDown(buttonName + GetNumberPlayer());
    }

    private string GetNumberPlayer()
    {
        return this.tag + this.numberPlayer.ToString();
    }

    public bool IsJoystickLeftHorizontalLeft()
    {
        return GetAxis(JOYSTICK_LEFT_ONE_HORIZONTAL) < 0f;
    }

    public bool IsJoystickLeftHorizontalRight()
    {
        return GetAxis(JOYSTICK_LEFT_ONE_HORIZONTAL) > 0f;
    }

    public bool IsJoystickLeftVerticalTop()
    {
        return GetAxis(JOYSTICK_LEFT_ONE_VERTICAL) < 0f;
    }

    public bool IsJoystickLeftVerticalDown()
    {
        return GetAxis(JOYSTICK_LEFT_ONE_VERTICAL) > 0f;
    }

    public float GetValueJoystickLeftHorizontal()
    {
        return GetAxis(JOYSTICK_LEFT_ONE_HORIZONTAL);
    }

    public float GetValueJoystickLeftVertical()
    {
        return GetAxis(JOYSTICK_LEFT_ONE_VERTICAL);
    }

    public bool IsPressButtonA()
    {
        return GetButtonDown(JOYSTICK_A);
    }

    public bool IsPressButtonB()
    {
        return GetButtonDown(JOYSTICK_B);
    }

    public bool IsPressButtonX()
    {
        return GetButtonDown(JOYSTICK_X);
    }

    public bool IsPressButtonY()
    {
        return GetButtonDown(JOYSTICK_Y);
    }
}
