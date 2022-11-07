using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOfPrefabs : MonoBehaviour
{
    [SerializeField] GameObject blockPrefab; // better Transform?
    public static GridOfPrefabs Instance { get; private set; }
    private GridXZ<GridPrefab> grid;

    private void Awake()
    {
        Instance = this;

        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 10f;
        grid = new GridXZ<GridPrefab>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (GridXZ<GridPrefab> g, int x, int y) => new GridPrefab(g, x, y));
    }

    public class GridPrefab
    {
        private GridXZ<GameObject> grid;
        private int x;
        private int y;

        public GridPrefab(GridXZ<GameObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }
    }
}
