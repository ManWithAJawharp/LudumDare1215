﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buildingPrefab;

    [SerializeField]
    private float levelSpeed = 0.1f;
    [SerializeField]
    private float buildingSpawnInterval = 10f;

    private float timer = 0f;

    private List<GameObject> buildings;

    //////////////////////////
    //  Built-in Functions  //
    //////////////////////////

    #region Built-in Functions

    void Awake ()
    {
        State.OnGlobalStateChanged += State_OnGlobalStateChanged;
    }

    void Start ()
    {
        SpawnBuilding();
    }

    void Update ()
    {
        if (State.Current == State.GlobalState.Game)
        {
            //  Move level to the left constantly.
            transform.position -= Vector3.right * levelSpeed;

            if (timer >= buildingSpawnInterval)
            {
                SpawnBuilding();

                timer = 0;
            }

            timer += Time.deltaTime;
        }
    }

    #endregion

    //////////////////////////
    //  Delegate Functions  //
    //////////////////////////

    #region Delegate Functions

    private void State_OnGlobalStateChanged(State.GlobalState prevGlobalState, State.GlobalState newGlobalState)
    {

    }

    #endregion

    /////////////////////////
    //  Private Functions  //
    /////////////////////////

    private void SpawnBuilding ()
    {
        GameObject building = Instantiate(buildingPrefab, new Vector3(20, Random.Range(-5, -15), 0), Quaternion.identity) as GameObject;

        building.transform.parent = transform;
    }
}
