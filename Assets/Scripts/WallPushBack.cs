using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPushBack : MonoBehaviour
{
    public float force = 5;
    Rigidbody rig;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            rig = gameObject.GetComponent<Rigidbody>();
            if (rig)
            {
                rig.AddForce(collision.contacts[0].normal * force, ForceMode.Impulse);
            }
        }
    }
}
