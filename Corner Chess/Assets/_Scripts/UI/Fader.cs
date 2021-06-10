using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private Animation fadeAnimatinon;

    private void Awake()
    {
        fadeAnimatinon = GetComponent<Animation>();
    }

    private void Start()
    {
        fadeAnimatinon.Play("FadeIn");
    }
}
