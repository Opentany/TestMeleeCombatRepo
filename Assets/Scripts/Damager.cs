using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float force = 10f;
    public float forceOffset = 0.1f;
    public ParticleSystem bloodSpill;
    public event Action<float> TakingDamageEvent;

    private void OnCollisionEnter(Collision collision)
    {
        var contact = collision.GetContact(0);
        if (contact.otherCollider.gameObject.tag == "Sword")
        {
            var stats = contact.otherCollider.gameObject.GetComponent<AttackStats>();
            if (stats && TakingDamageEvent!=null)
            {
                TakingDamageEvent(stats.damage);
            }
            bloodSpill.transform.position = contact.point;
            bloodSpill.transform.forward = contact.normal;
            bloodSpill.Play();
            MeshDeformer deformer =contact.thisCollider.GetComponent<MeshDeformer>();
            if (deformer)
            {
                Vector3 point = contact.point;
                point += contact.normal * forceOffset * -1f ;
                deformer.AddDeformingForce(point, force);
            }
        }
    }
}
