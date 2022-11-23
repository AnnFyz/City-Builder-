using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRaycast : MonoBehaviour
{
    public bool IsThisObjWasSelected = false;
    BlockPrefab prefabBlock;
    MyGridBuildingSystem localGrid;
    Ray ray;
    RaycastHit hit;
    BlockPrefab block;
    private void Awake()
    {
        prefabBlock = GetComponent<BlockPrefab>();
        localGrid = GetComponent<MyGridBuildingSystem>();
    }
    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //the collider could be children of the unit, so we make sure to check in the parent
            block = hit.collider.GetComponentInParent<BlockPrefab>();
            if (prefabBlock == block)
            {
                prefabBlock.IsThisBlockWasSelected = true;
                BuildingManager.grid = localGrid.grid; //HOW TO CONVERT properly
                Debug.Log("GLOBAL GRID WAS UPDATED");
            }
            else
            {
                prefabBlock.IsThisBlockWasSelected = false;
            }
        }

        else
        {
            prefabBlock.IsThisBlockWasSelected = false;
        }
    }
}

