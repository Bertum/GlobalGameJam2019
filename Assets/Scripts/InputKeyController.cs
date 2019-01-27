using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyController : MonoBehaviour
{
    private const string KEY_HORIZONTAL = "Horizontal";
    private const string KEY_VERTICAL = "Vertical";

    public float moveSpeed = 10f;
    private Rigidbody2D rigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        this.rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move(GetValueHorizontal(), GetValueVertical());

        if (IsPressMovement())
        {
            float angle = Mathf.Atan2(GetValueHorizontal() * -1, GetValueVertical()) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    public bool IsKeyUpArrow()
    {
        return IsKey(KeyCode.UpArrow);
    }

    public bool IsKeyDownArrow()
    {
        return IsKey(KeyCode.DownArrow);
    }

    public bool IsKeyLeftArrow()
    {
        return IsKey(KeyCode.LeftArrow);
    }

    public bool IsKeyRightArrow()
    {
        return IsKey(KeyCode.RightArrow);
    }

    public bool IsKeySpace()
    {
        return IsKey(KeyCode.Space);
    }

    public float GetValueHorizontal()
    {
        return GetAxis(KEY_HORIZONTAL);
    }

    public float GetValueVertical()
    {
        return GetAxis(KEY_VERTICAL);
    }

    public bool IsPressMovement()
    {
        return IsKeyDownArrow() || IsKeyLeftArrow() || IsKeyRightArrow() || IsKeyUpArrow();
    }

    private bool IsKey(KeyCode keyCode)
    {
        return Input.GetKey(keyCode);
    }

    private void Move(float horizontalMovement, float verticalMovement)
    {
        this.rigidbodyComponent.velocity = new Vector2(horizontalMovement * this.moveSpeed, verticalMovement * this.moveSpeed);
    }

    private float GetAxis(string axisName)
    {
        return Input.GetAxis(axisName);
    }
}
