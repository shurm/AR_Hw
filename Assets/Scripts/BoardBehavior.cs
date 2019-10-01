using UnityEngine;
using System.Collections.Generic;
using Photon;

public class BoardBehavior : Photon.MonoBehaviour {

    public GameObject SplatterPrefab;
    public GameObject imageTarget;

    private List<GameObject> splatters = new List<GameObject>();

    //fix the z issue so splatters appear in order
    private float z_decrement = 0.0001f;
    private float min_z = 10000000;

    private void Update () {
	    if (Input.GetKeyDown(KeyCode.Space)) {
            imageTarget.SetActive(!imageTarget.activeSelf);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
       
        var other = collision.collider.gameObject;
        Vector3 hit_position = other.transform.position;
        if (other.CompareTag("Ball"))
        {
            //gets the minimum z value of all the spatters that were spawned, minus a tiny amount
            min_z = Mathf.Min(hit_position.z, min_z) - z_decrement;

            //sets z coordinate to this smaller value, so the new splatter appears in front of all the older splatters 
            hit_position.z = min_z;
            
            PhotonNetwork.Destroy(other);
            Quaternion rot =  Quaternion.AngleAxis(Random.Range(0f, 360f), new Vector3(0, 0, 1)) ; //*transform.rotation;
            var splatter = Instantiate(SplatterPrefab, hit_position, rot) as GameObject;

            splatter.GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;

            splatters.Add(splatter);
            
        }

    }
}
