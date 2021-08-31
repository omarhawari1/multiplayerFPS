using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField]private List<GameObject> guns = new List<GameObject>();
    private void Update() 
    {
        if(isLocalPlayer)
        {
            foreach(GameObject gun in guns)
            {
                gun.layer = LayerMask.NameToLayer("LocalShow");
            }
            gameObject.layer = LayerMask.NameToLayer("LocalPlayer");
        }
        else if(!isLocalPlayer)
        {
            foreach(GameObject gun in guns)
            {
                gun.layer = LayerMask.NameToLayer("RemoteShow");
            }
            gameObject.layer = LayerMask.NameToLayer("RemotePlayer");
        }
        
    }
}
