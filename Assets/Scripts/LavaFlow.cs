using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFlow : MonoBehaviour {

    public ParticleSystem particles;

    private bool activated = false;

    public void ToggleActivated()
    {
        activated = !activated;
    }

    void Update()
    {
        if (activated)
        {
            particles.Play();
        }
    }
}
