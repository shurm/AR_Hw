using UnityEngine;
using System.Collections.Generic;
using Photon;

public class BoardBehavior : Photon.MonoBehaviour {

    public GameObject SplatterPrefab;
    public GameObject imageTarget;

    private List<GameObject> splatters = new List<GameObject>();

    private float z_decrement = 0.00001f;
    private float currrent_z_decrement = 0;
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
            PhotonNetwork.Destroy(other);
            Quaternion rot =  Quaternion.AngleAxis(Random.Range(0f, 360f), new Vector3(0, 0, 1)) ; //*transform.rotation;
            var splatter = Instantiate(SplatterPrefab, hit_position+Vector3.forward*currrent_z_decrement, rot) as GameObject;

            splatter.GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;

            splatters.Add(splatter);
            currrent_z_decrement -= z_decrement;
        }

    }
}
