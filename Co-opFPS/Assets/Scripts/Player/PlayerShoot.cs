using Mirror;
using UnityEngine;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField]private LayerMask RemotePlayer;
    private Camera mainCamera;

    private void Start() 
    {
        if(!isLocalPlayer){return;}
        mainCamera = Camera.main;
    }

    [ClientCallback]
    private void Update() 
    {
        if(!isLocalPlayer){return;}
        if(Input.GetButtonDown("Fire1"))
        {
            if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, Mathf.Infinity, RemotePlayer))
            {
                CmdPlayerShot(hit.collider.name);
            }
        }
    }

    [Command]
    void CmdPlayerShot(string playerID)
    {
        Debug.Log(playerID + " has been shot");
    }
}
