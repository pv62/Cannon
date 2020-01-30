using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameManager.Instance.PlayFX(GameManager.Instance.cannonBallFXPool.GetPooledPbject(), transform.position);
        Collider[] objects = Physics.OverlapSphere(transform.position, GameManager.Instance.blastRadius);
        foreach(Collider o in objects)
        {
            if (o.gameObject.CompareTag("Destructible"))
            {
                GameManager.Instance.PlayFX(GameManager.Instance.destructibleFXPool.GetPooledPbject(), o.transform.position);
                o.gameObject.SetActive(false);
            }
        }
        gameObject.SetActive(false);
    }
}
