using System;
using System.Collections;
using System.Collections.Generic;
using Suli.Bumble;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider))]
public class DeathPlate : MonoBehaviour, IDamageDealer
{
    [SerializeField] private float startX;
    [SerializeField] private float endX;

    public void Start()
    {
        transform.localPosition =
            new Vector3(Random.Range(startX, endX), transform.localPosition.y, transform.localPosition.z);
    }
}
