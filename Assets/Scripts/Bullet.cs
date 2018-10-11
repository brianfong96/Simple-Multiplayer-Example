using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
