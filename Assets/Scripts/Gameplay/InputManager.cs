﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    #region Variables

    //private Vector3 mousePosition, lastWorldPosition;
    #endregion

    #region Delegates
    public delegate void PrimaryButtonDown();
    public delegate void PrimaryButtonUp();
    public delegate void SecondaryButtonDown();
    public delegate void SecondaryButtonUp();

    public static event PrimaryButtonDown OnPrimaryButtonDown;
    public static event PrimaryButtonDown OnPrimaryButtonUp;
    public static event SecondaryButtonDown OnSecondaryButtonDown;
    public static event SecondaryButtonDown OnSecondaryButtonUp;
    #endregion

    public static Vector3 MousePosition
    {
        get
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            return mousePosition;
        }
    }

    #region Public functions

    #endregion

    #region Built-in functions

    #endregion

    #region Setup
    void Start()
    {
    }
    #endregion

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && OnPrimaryButtonUp != null)
        {
            OnPrimaryButtonUp();
        }
        else if (Input.GetMouseButtonDown(0) && OnPrimaryButtonDown != null)
        {
            OnPrimaryButtonDown();
        }

        if (Input.GetMouseButtonUp(1) && OnSecondaryButtonUp != null)
        {
            OnSecondaryButtonUp();
        }
        else if (Input.GetMouseButtonDown(1) && OnSecondaryButtonDown != null)
        {
            OnSecondaryButtonDown();
        }
    }
}
