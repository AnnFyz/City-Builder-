using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRaycas : MonoBehaviour
{
    public bool IsThisObjWasSelected = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4)) { GetMouseWorldPosition(); }
        //GetMouseWorldPosition();
    }
    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
        {

            IsThisObjWasSelected = true;
            return raycastHit.point;
        }
        else
        {
            IsThisObjWasSelected = false;
            return Vector3.zero;

        }
    }
}
