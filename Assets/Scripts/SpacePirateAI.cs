using UnityEngine;
using System.Collections;

public class SpacePirateAI : MonoBehaviour {

    public GameObject playerStats;
    public Protagonist script;

    public float pirateMaxHealth;
    private float health;
    public float fireRate;
    private bool facingLeft = true;

    public Transform shootLocation;
    public GameObject plasma;

    private float nextFire = 0f;

    private Animator anim;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        health = pirateMaxHealth;
        script = playerStats.GetComponent<Protagonist>(); // get script

	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            anim.SetTrigger("Shooting");
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.GetComponent<Transform>().position);
        if (health == 0 && script.ishooking)
        {
            Die();
        }

    }

    void shootPlasma()
    {
        if (facingLeft)
            Instantiate(plasma, shootLocation.position, Quaternion.Euler(new Vector3(0, 0, 180f)));
        else
            Instantiate(plasma, shootLocation.position, Quaternion.Euler(new Vector3(0, 0, 0)));
    }

    public void TakeDamage(float value)
    {
        //decrement health
        health -= value;
        Debug.Log("Enemy Health is now " + health.ToString());

        //Check for death
        if (health <= 0)
        {
            Die();
        }

    }

    //Destroys player object and triggers and scene stuff
    private void Die()
    {
        //destroy gameobject
        Destroy(this.gameObject);

        //trigger anything we need to end the game, find game manager and lose game
    }
}
