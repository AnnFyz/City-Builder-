using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse3D : MonoBehaviour {

    public static Mouse3D Instance { get; private set; }

    [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask)) {
            transform.position = raycastHit.point;
        }
    }

    public static Vector3 GetMouseWorldPosition() => Instance.GetMouseWorldPosition_Instance();

    private Vector3 GetMouseWorldPosition_Instance() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask)) {
            //Debug.Log("GridBuildingSystem3D.IsValidGridPos" + GridBuildingSystem3D.IsValidGridPos);
            GridBuildingSystem3D.IsValidGridPos = true;
            return raycastHit.point;
        } else {
            //Debug.Log("GridBuildingSystem3D.IsValidGridPos" + GridBuildingSystem3D.IsValidGridPos);
            GridBuildingSystem3D.IsValidGridPos = false;
            return Vector3.zero;
           
        }
    }

}
