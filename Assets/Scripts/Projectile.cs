using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float damage;
    public float projectileSpeed;
    Rigidbody2D myRB;
    Transform myTransform;

	// Use this for initialization
	void Awake () {
        myRB = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();

        if (myTransform.localRotation.z > 0)
            myRB.AddForce(new Vector2(-1, 0) * projectileSpeed, ForceMode2D.Impulse);
        else
            myRB.AddForce(new Vector2(1, 0) * projectileSpeed, ForceMode2D.Impulse);

    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth thePlayerHealth = other.gameObject.GetComponent<PlayerHealth>();
            thePlayerHealth.addDamage(damage);
        }
        Destroy(gameObject);
    }
}
