using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingCursor : MonoBehaviour
{
    void Update()
    {
        Aim();
        Cursor.lockState = CursorLockMode.Confined;
        if (transform.position.z < 20f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 20f);
        }
    }

    void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Target")))
        {
            transform.position = hit.point;
        }
    }

    //void AimWithKeyboard()
    //{
    //    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //    transform.position += move * GameManager.Instance.aimSpeed * Time.deltaTime;
    //}
}
