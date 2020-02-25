using UnityEngine;
using System.Collections;

public class Protagonist : MonoBehaviour {

    [HideInInspector] public bool facingLeft = true;
    [HideInInspector] public bool jump = true;

    float hookTimer = 0;
    public float endAnimationTiming;
    public bool ishooking = false;
    Vector2 currDirection;

    public float moveForce = 365f;
    public float maxSpeed = 10f;
    public float jumpForce = 700f;

    //bool hitEnemy = false;

    public Transform groundCheck;
    private bool grounded = false;
    private float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    private float verticalValue;

    private Animator anim;

    private Rigidbody2D rb2d;
    

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	

	// Update is called once per frame
    void Update()
    {
        if (grounded && Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Ground", false);
            rb2d.AddForce(new Vector2(0, jumpForce));
        }

        if (hookTimer <= 0)
        {
            anim.SetBool("Hook", false);
            ishooking = false;
        }

        if (grounded && Input.GetButtonDown("FireHook") && hookTimer <= 0)
        {
            //activate hooking
            ishooking = true;
            hookTimer = 0.4f;
            anim.SetBool("Hook", true);
        } else {
            hookTimer -= Time.deltaTime;
        }


        verticalValue = Input.GetAxis("Vertical");
        if (grounded && (verticalValue < 0) && (anim.GetFloat("Speed") == 0))
        {
            anim.SetFloat("DownValue", Mathf.Abs(verticalValue));
        }
        else
            anim.SetFloat("DownValue", 0);
        
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void FixedUpdate () {
        if (!ishooking)
        {
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
            anim.SetBool("Ground", grounded);

            anim.SetFloat("vSpeed", rb2d.velocity.y);

            float move = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(move));

            rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);

            if (move < 0 && !facingLeft)
                Flip();
            else if (move > 0 && facingLeft)
                Flip();
        }
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("HOOK!");
        //Debug.Log(collision.GetComponent<Transform>().position);
        if (ishooking) {
            anim.SetFloat("vSpeed", 0);
            anim.SetFloat("Speed", 0);
            //transform.position = collision.GetComponent<Transform>().position;
            StartCoroutine(Fly(collision.gameObject));
        }
        
    }
    IEnumerator Fly(GameObject target)
    {
        Debug.Log("FLY YOU FOOL LMAO");

        float elapsedTime = 0.0f;
        Vector3 lastSeen = target.GetComponent<Transform>().position;
        float totalDis = Vector3.Distance(transform.position, lastSeen);
        float fractionalDist = 0.0f;
        while (transform.position != lastSeen && ishooking)
        {
            transform.position = Vector3.Lerp(transform.position, lastSeen, fractionalDist);
            elapsedTime += Time.deltaTime;
            fractionalDist = elapsedTime / totalDis;
            yield return null;
        }
        ishooking = false;
        //Debug.Log("FLY YOU FOOL LMAO");
    }

    //void OnTriggerExit2D(Collider2D collision)
    //{  
    //}


}
