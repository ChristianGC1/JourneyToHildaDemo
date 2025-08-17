using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public GameObject jumpPad;
    public AudioSource sound;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered trigger");
        if (other.gameObject.CompareTag("PickUp"))
        {
            Debug.Log("entered if");
            other.gameObject.SetActive(false);

            jumpPad.GetComponent<Renderer>().material.color = Color.blue;
            jumpPad.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
