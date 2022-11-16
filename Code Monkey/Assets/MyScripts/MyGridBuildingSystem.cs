using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGridBuildingSystem : MonoBehaviour
{
    private MyGridXZ<MyGridObject> grid;
    [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList = null;
    private PlacedObjectTypeSO placedObjectTypeSO;
    private PlacedObjectTypeSO.Dir dir;
    [SerializeField] int gridWidth = 3;
    [SerializeField] int gridHeight = 3;
    [SerializeField] float cellSize = 5f;
    BlockPrefab blockPrefab;
    public Vector3 origin;
    // startGrid and cuurentGrid
    private void Awake()
    {
        origin = transform.position;
        blockPrefab = GetComponent<BlockPrefab>(); //IS THAT RIGHT?
        grid = new MyGridXZ<MyGridObject>(gridWidth, gridHeight, cellSize, origin - BlockPrefab.offset, (MyGridXZ<MyGridObject> g, int x, int y) => new MyGridObject(g, x, y));
        placedObjectTypeSO = null;// placedObjectTypeSOList[0];
        blockPrefab.OnHeightChanged += UpdateGrid;
    }

    public void UpdateGrid(int newHeight)
    {
        // delete all obj on thr grid
        //for (int x = 0; x < gridWidth; x++)
        //{
        //    for (int y = 0; y < gridHeight; y++)
        //    {
        //        grid
        //    }
        //}
        grid = new MyGridXZ<MyGridObject>(gridWidth, gridHeight, cellSize, new Vector3(origin.x - BlockPrefab.offset.x, (-newHeight * BlockPrefab.offset.y) + BlockPrefab.offset.y, origin.z - BlockPrefab.offset.z), (MyGridXZ<MyGridObject> g, int x, int y) => new MyGridObject(g, x, y));
    }
    public class MyGridObject
    {

        private MyGridXZ<MyGridObject> grid;
        private int x;
        private int y;
        public PlacedObject_Done placedObject;

        public MyGridObject(MyGridXZ<MyGridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            placedObject = null;
        }

        public override string ToString()
        {
            return x + ", " + y + "\n" + placedObject;
        }

        public void SetPlacedObject(PlacedObject_Done placedObject)
        {
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x, y); // 
        }

        public void ClearPlacedObject()
        {
            placedObject = null;
            grid.TriggerGridObjectChanged(x, y);
        }

        public PlacedObject_Done GetPlacedObject()
        {
            return placedObject;
        }

        public bool CanBuild()
        {
            return placedObject == null;
        }

    }

}
