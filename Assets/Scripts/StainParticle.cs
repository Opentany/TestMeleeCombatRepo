using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainParticle : MonoBehaviour
{
    public ParticleSystem particlesSystem;
    public GameObject stainQuad;
    public StainsController stainsController;
    private List<ParticleCollisionEvent> particleCollisionEvents = new List<ParticleCollisionEvent>();

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particlesSystem, other, particleCollisionEvents);

        int count = particleCollisionEvents.Count;

        for (int i = 0; i < count; i++)
        {
            //wyłączyłem osiadanie plam na zawodnikach oraz na mieczach ponieważ generowało to dziwne błędy. 
            //Gdy ustawiałem obiekt kolizji jako rodzica to zdarzało się plamy trafiające na ziemie były dziećmi gracza i gdy się przmieszczał latały z nim
            //Wiem, że najlepszym rozwiązaniem tego problemu jest zrobienie tego na shaderach, niestety mam w nie jeszcze za małego skilla.
            if (other.tag != "Player" && other.tag != "Sword")
            {
                GameObject stain = Instantiate(stainQuad, particleCollisionEvents[i].intersection, Quaternion.identity) as GameObject;
                stain.transform.forward = particleCollisionEvents[i].normal * -1f;
                stainsController.AddStain(stain);
            }
            
        }
    }
}
