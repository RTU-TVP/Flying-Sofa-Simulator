using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAppearSphere : MonoBehaviour
{
    bool startGrowing = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GrowTileSphereTrigger>(out GrowTileSphereTrigger trigger))
        {
            startGrowing = true;
            StartCoroutine(SphereStoper());
            Destroy(trigger.gameObject);
        }
        if((other.TryGetComponent<TileAppear>(out TileAppear tile)) && (tile.GetComponent<MeshRenderer>().enabled == false))
        {
            tile.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    private void FixedUpdate()
    {
        if(startGrowing)
        {
            transform.parent = null;
            transform.localScale = transform.localScale + new Vector3(1.5f, 1.5f, 1.5f);
        }
    }


    IEnumerator SphereStoper()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
        yield break;
    }
}
