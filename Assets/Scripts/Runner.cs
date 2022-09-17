using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{

    
    public float Jump_force =2.0f;
    public float Playerspeed=1;
    public Animator PlayerAnimator;
    public Vector3 jumpp;
    public bool isgrounded = true;
    public Transform currentposition;
    public Transform target;

    public List<Collider> Ragdollparts = new List<Collider>();
    public List<Rigidbody>bodies = new List<Rigidbody>();

    //private PauseControl pausecontrol;



    public Rigidbody playerrigidbody;


    private void Awake() 
    {
        turnoffragdoll();
        setragdollparts();
        
       
        //StartCoroutine(Starttocheck());

        
    }

    //temp

    private IEnumerator Starttocheck()
    {
        yield return new WaitForSeconds(5f);
        playerrigidbody.AddForce(200f * Vector3.up);
        yield return new WaitForSeconds(0.5f);
        turnonnragdoll();
    }

    

    private void turnoffragdoll()
    {

        Rigidbody[] rigidbody = this.gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody b in rigidbody)
        {
            if(b.gameObject != this.gameObject)
            {
        
                bodies.Add(b);
            }
        
           
        }
        foreach (Rigidbody b in bodies)
            {
            b.isKinematic = true;
            b.detectCollisions = false;
            }

    }

    


    //
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    } 

    

    public void turnonnragdoll()
    {
        playerrigidbody.useGravity =false;
        playerrigidbody.velocity = Vector3.zero;
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        PlayerAnimator.enabled = false;
        PlayerAnimator.avatar = null;
        StartCoroutine(Death());
        


        foreach (Collider c in Ragdollparts)
        {
            c.isTrigger = false;
            c.attachedRigidbody.velocity = Vector3.zero;
        }
        foreach (Rigidbody b in bodies)
            {
            b.isKinematic = false;
            b.detectCollisions = true;
            }

    }
    private void setragdollparts()
    {
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
        
        foreach (Collider c in colliders)
        {
            if(c.gameObject != this.gameObject)
            {
                c.isTrigger=true; 
                Ragdollparts.Add(c);
            }
        }

    }
   
    // Start is called before the first frame update
    void Start()
    {
        //pausecontrol= GetComponent<PauseControl>();
        
        
    }

    // Update is called once per frame
    void Update()

    {
        if(PlayerAnimator.enabled == true)
    {
        if(isgrounded)
     {
         run();
       
     }
       
        
    }
    }

    
 
 //consider when character is jumping .. it will exit collision.
 private void OnCollisionExit(Collision collision)
 {
     if(collision.gameObject.tag == "Ground")
     {
         isgrounded = false;
     }
 }





    void run()
    {
        //Time.timeScale = 0f;
        
        //target.position = transform.position+(2,0,0);
        //print(transform.position);
        
        PlayerAnimator.SetTrigger("Run");

        

        if (Input.GetKey("a"))
        {
            //transform.position = new Vector3(transform.position.x-2,0,0);
            transform.Translate(Vector3.left*Time.deltaTime*Playerspeed);
            
        }
        if (Input.GetKey("d"))
        {
            //transform.position = transform.position + new Vector3(2,0,0);
            transform.Translate(Vector3.right*Time.deltaTime*Playerspeed);
            


            
        }

         if (Input.GetKeyDown("space")&& isgrounded)
        {
            //StartCoroutine(WaitJump());
            PlayerAnimator.SetTrigger("Jump");
            //print("space key was pressed");

            playerrigidbody.AddForce(jumpp * Jump_force, ForceMode.Impulse);
            
        }

        







    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            //Debug.Log("Do something here");
            turnonnragdoll();
        }
        if(collision.gameObject.tag == "Ground")
     {
         isgrounded = true;
     }





    }




   
}
