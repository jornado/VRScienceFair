using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoker : MonoBehaviour {

    public ParticleSystem smoker;
    
    public void Toggle()
    {
        if (smoker.isEmitting)
        {
            smoker.Stop();
        } else
        {
            smoker.Play();
        }
    }
}
