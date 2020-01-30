using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private static GameManager s_instance;
    public static GameManager Instance { get { return s_instance; } }

    public event Action onFire;

    [Header("Cannon")]
    public float blastRadius = 10f;
    public float aimSpeed = 10f;
    public AimingCursor aimingCursor;

    [Header("Object Pools")]
    public Pooler cannonBallPool;
    public Pooler cannonBallFXPool;
    public Pooler launchFXPool;
    public Pooler destructibleFXPool;
    public Pooler destructiblePool;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            s_instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                onFire();
            }
        }
    }

    public void PlayFX(GameObject obj, Vector3 position)
    { 
        if (obj == null) { return; }
        obj.transform.position = position;
        obj.SetActive(true);
    }

    public void SpawnDestructibles()
    {
        foreach(GameObject obj in destructiblePool.GetPooledObjectsList())
        {
            if (obj == null) { return; }
            
            obj.transform.position = GenerateNewPosition(obj.transform.localScale);
            obj.SetActive(true);
        }
    }

    Vector3 GenerateNewPosition(Vector3 scale)
    {
        float randomX = UnityEngine.Random.Range(-40f, 40f);
        float randomZ = UnityEngine.Random.Range(30f, 150f);
        Vector3 position = new Vector3(randomX, scale.y / 2, randomZ);
        if (Physics.OverlapBox(position, scale / 2, Quaternion.identity).Length > 1)
        {
            return GenerateNewPosition(scale);
        }
        return position;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}