using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : MonoBehaviour
{
    public bool IsDestroyingWall()
    {
        return Input.GetKeyDown(KeyCode.D);
    }
}
