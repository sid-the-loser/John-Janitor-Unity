using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LookingAtObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> children = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable Object"))
        {
            children.Clear();
            foreach (Transform child in other.transform)
            {
                children.Add(child.gameObject);
            }
            foreach (var child in children)
            {
                var shader = child.GetComponent<Renderer>().material.shader;
                if (shader != null)
                {
                    child.GetComponent<Renderer>().material.SetFloat("_ActiveRim", 1);
                }
            }
        }
    }
}
