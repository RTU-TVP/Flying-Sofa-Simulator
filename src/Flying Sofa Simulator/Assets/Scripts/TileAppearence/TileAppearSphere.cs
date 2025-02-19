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
            DestroyAllSphereTriggers();
            FindObjectOfType<SofaMovement>().GetComponent<AudioManager>().Play("voice6");
            FindObjectOfType<LevelFinisher>().FinishLevel(30);
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

    void DestroyAllSphereTriggers()
    {
        foreach(GrowTileSphereTrigger trigger in FindObjectsOfType<GrowTileSphereTrigger>())
        {
            Destroy(trigger.gameObject);
        }
    }
    IEnumerator SphereStoper()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
        yield break;
    }
}
