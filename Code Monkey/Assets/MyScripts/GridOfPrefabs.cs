using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridOfPrefabs : MonoBehaviour
{
    [SerializeField] GameObject blockPrefab; // better Transform?
    public static GridOfPrefabs Instance { get; private set; }
    private MyGridXZ<PrefabGridObject> grid;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        grid = new MyGridXZ<PrefabGridObject>(10, 10, 15f, Vector3.zero, (MyGridXZ<PrefabGridObject> g, int x, int y) => new PrefabGridObject(g, x, y));
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
               Instantiate(blockPrefab, grid.GetWorldPosition(x, y) + new Vector3(7.5f, -5f, 7.5f), Quaternion.identity);
            }
        }
    }

    private void Update()
    {
        Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();

        if (Input.GetMouseButtonDown(0))
        {
            PrefabGridObject prefabGridObject = grid.GetGridObject(mousePosition);

            if (prefabGridObject != null)
            {
                prefabGridObject.ChangeValue(5);
               
            }
        }

    }
}
    public class PrefabGridObject 
    {

    private const int MIN = 0;
    private const int MAX = 100;

    private MyGridXZ<PrefabGridObject> grid;
    private int x;
    private int y;
    private int value;

    private GameObject blockPrefab;
    public PrefabGridObject(MyGridXZ<PrefabGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void ChangeValue(int addValue) //OVERRIDE THIS FUNC
    {
        value += addValue;
        value = Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x, y);
        Debug.Log("Value " + value);
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
