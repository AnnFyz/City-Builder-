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
        grid = new MyGridXZ<PrefabGridObject>(10, 10, 15f, Vector3.zero, (MyGridXZ<PrefabGridObject> g, int x, int y) => new PrefabGridObject(g, x, y, blockPrefab));
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
        Vector3 mousePosition = GetMouseWorldPosition();
        //Debug.Log("Mouse Position" + mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            PrefabGridObject prefabGridObject = grid.GetGridObject(mousePosition);
            
            if (prefabGridObject != null)
            {
                prefabGridObject.ChangeValue(50);
               
            }
        }

    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;

        }
    }
}
    public class PrefabGridObject 
    {

    private const int MIN = 0;
    private const int MAX = 255;

    private MyGridXZ<PrefabGridObject> grid;
    private int x;
    private int y;
    private int value;

    private GameObject blockPrefab;
    public PrefabGridObject(MyGridXZ<PrefabGridObject> grid, int x, int y, GameObject blockPrefab)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.blockPrefab = blockPrefab;
    }

    public void ChangeValue(int addValue) 
    {
        value += addValue;
        value = Mathf.Clamp(value, MIN, MAX);
        SelectObj();
        grid.TriggerGridObjectChanged(x, y);
        //Debug.Log("Value " + value);
    }
       
    public void SelectObj()
    {
        Color oldColor = blockPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterial.color;
        Color newColor = new Color (GetValueNormalized(), GetValueNormalized(), oldColor.b);
        blockPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterial.SetColor("_Color", newColor);
        Debug.Log("Color was changed " + newColor);
        //blockPrefab.transform.localScale = new Vector3(blockPrefab.transform.localScale.x, blockPrefab.transform.localScale.y + value, blockPrefab.transform.localScale.z);

    }

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}

public class BlockPrefab : MonoBehaviour
{
    //public void SelectObj() //TryGetComponent
    //{
    //  
    //    
    //}
}
