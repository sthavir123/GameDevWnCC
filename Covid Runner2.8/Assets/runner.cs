using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runner : MonoBehaviour
{   
     [SerializeField] private Transform groundCheckTransform = null;
     [SerializeField] private Transform leftCheckTransform = null;
     [SerializeField] private Transform rightCheckTransform = null;
     private bool jumpKeyWasPressed;
     private bool leftArrowWasPressed;
     private bool rightArrowWasPressed;
     private bool isGrounded;
     private int laneNumber=2;
     private int sanitiserDrops=0; 
     private bool bossFight = false; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
       
    
        // jump if space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
           jumpKeyWasPressed = true; 
           
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
           leftArrowWasPressed = true; 
           
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
           rightArrowWasPressed = true; 
           
        }
        
        
    }
    
    // FixedUpdate is called every physics update 
    private void FixedUpdate()
    { 
      if(!bossFight)
      {
      GetComponent<Rigidbody>().velocity = new Vector3((-1)*5, GetComponent<Rigidbody>().velocity.y, 0);
      }
      else
      {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
      }
      
      if(leftArrowWasPressed)
         { 
           if(laneNumber!=3)
           { 
             transform.Translate(new Vector3(0f, 0f, -200) * Time.deltaTime);
             laneNumber++;
             leftArrowWasPressed = false;
           } 
         }
      if(rightArrowWasPressed)
         { 
           if(laneNumber!=1)
           { 
           transform.Translate(new Vector3(0f, 0f, 200) * Time.deltaTime);
           laneNumber--;
           rightArrowWasPressed = false;
           } 
         }
            
      if(!isGrounded)
       {
         return;
       }
       
      if (jumpKeyWasPressed)
        {
           Debug.Log("Space key was pressed down");
           GetComponent<Rigidbody>().AddForce(Vector3.up*10f, ForceMode.VelocityChange);
           jumpKeyWasPressed = false; 
           
        }
        
       
        
        
    }     
    private void OnCollisionEnter(Collision collisionInfo)
    {
       if(collisionInfo.collider.tag == "block")
       {
          Debug.Log(" game over");
       }
       isGrounded = true;
       
    }
    
   // private void OntriggerEnter
    private void OnCollisionExit(Collision collision)
    {
      isGrounded = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.layer == 6)
      {
         Debug.Log("collected");
         Destroy(other.gameObject);
         sanitiserDrops=sanitiserDrops+1;   
      }
      
      if(other.gameObject.layer == 7)
      {
         Debug.Log("start fight");
         Destroy(other.gameObject);
         bossFight = true;   
      }
    }  
}
