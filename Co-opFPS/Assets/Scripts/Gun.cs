using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]private float damage;
    [SerializeField]private LayerMask playerLayer;

    private Camera FPScam;

    private void Start() 
    {
        FPScam = Camera.main;
    }

    private void Update() 
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out RaycastHit hit, Mathf.Infinity, playerLayer))
        {
            Debug.Log("hit player");
        }

    }
}
