using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueezerTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("sofa"))
        {
            //ubiystva raschlenyonka
            Destroy(other.gameObject);
        }
    }
}
