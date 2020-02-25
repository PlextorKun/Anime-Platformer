using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public float fullHealth;

    private float currentHealth;
    private Animator anim;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        currentHealth = fullHealth;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addDamage(float damage)
    {
        if (damage <= 0)
            return;
        currentHealth -= damage;
        if (currentHealth <= 0)
            makeDead();
    }

    public void makeDead()
    {
        anim.SetBool("Dead", true);
        Destroy(GetComponent<Protagonist>());
        anim.SetBool("Ground", true);
        Destroy(gameObject, 5f);
    }
}
