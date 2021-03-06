﻿using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject BulletPrefab;
	public Transform BulletSpawn;

    public float BulletSpeed = 10.0f;
    public float BulletLifeTime = 2.0f;

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	// Update is called once per frame
	void Update () 
	{

		if (!isLocalPlayer) return;

		var x = Input.GetAxis("Horizontal") * Time.deltaTime* 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0,x,0);
		transform.Translate(0,0,z);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire();
		}
	}

    [Command]
	void CmdFire()
	{
		var bullet = (GameObject) Instantiate(
			BulletPrefab,
			BulletSpawn.position,
			BulletSpawn.rotation);
		
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * BulletSpeed;
        NetworkServer.Spawn(bullet);
		Destroy(bullet, BulletLifeTime);		
	}    
}
