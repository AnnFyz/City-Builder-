using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockPrefab : MonoBehaviour
{
    public static Vector3 offset = new Vector3(7.5f, -5f, 7.5f);
    public int newHeight;
    public event Action <int> OnHeightChanged; // maybe it is tooooo global
   
    public static BlockPrefab Create(Vector3 worldPosition, GameObject blockPrefab)
    {
        GameObject placedBlockPrefabObj = Instantiate(blockPrefab, worldPosition + offset, Quaternion.identity);
        BlockPrefab placedBlockPrefab = placedBlockPrefabObj.GetComponent<BlockPrefab>();
        return placedBlockPrefab;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void ChangeHeight(int addedHeight)
    {
        transform.localScale += new Vector3(0, addedHeight, 0);
        newHeight = Mathf.FloorToInt(transform.localScale.y);
        Debug.Log("New Height" + newHeight);
        OnHeightChanged?.Invoke(newHeight);
    }

}
