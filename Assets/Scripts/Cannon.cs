using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform platform, barrel, firePoint, fxPoint;
    [SerializeField] [Range(10, 80)] float angle;

    private void Start()
    {
        GameManager.Instance.onFire += Fire;
    }

    private void Update()
    {
        AimCannon();
    }

    private void Fire()
    {
        Vector3 velocity = CalculateShotVelocity(GameManager.Instance.aimingCursor.transform.position, angle);

        GameObject obj = GameManager.Instance.cannonBallPool.GetPooledPbject();
        if (obj == null) { return; }
        obj.transform.position = firePoint.position;
        obj.GetComponent<Rigidbody>().velocity = velocity;
        obj.SetActive(true);

        GameManager.Instance.PlayFX(GameManager.Instance.launchFXPool.GetPooledPbject(), fxPoint.position);
    }

    private Vector3 CalculateShotVelocity(Vector3 destination, float angle)
    {
        Vector3 direction = destination - firePoint.position;
        float height = direction.y;
        direction.y = 0f;
        float distance = direction.magnitude;
        float a = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(a);
        distance += height / Mathf.Tan(a);

        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        if (float.IsNaN(velocity)) { return Vector3.zero; }
        return velocity * direction.normalized;
    }

    private void AimCannon()
    {
        Vector3 direction = GameManager.Instance.aimingCursor.transform.position - platform.position;
        direction.y = 0f;
        platform.rotation = Quaternion.LookRotation(direction);

        float angleChange = Input.GetAxis("Vertical") * GameManager.Instance.aimSpeed * Time.deltaTime;
        angle = Mathf.Clamp(angle + angleChange, 10f, 80f);
        barrel.localRotation = Quaternion.AngleAxis(-angle, Vector3.right);
    }
}