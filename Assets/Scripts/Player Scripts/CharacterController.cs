using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterController : MonoBehaviour
{

    [SerializeField] float walkSpeed, jumpHeight;
    Rigidbody rbody;
    Animator animator;
    [SerializeField] Transform camHold;
    [SerializeField] AnimationClip lightAnim;
    [SerializeField] bool sleeping;
    float animTime = 0;
    [SerializeField] ParticleSystem fire,water;
    AudioSource sound;
    [SerializeField] private float RotationPower = 10;
    [SerializeField] private float HorizontalDamping = 1;

    private Transform FollowTargetTransform;
    private Vector2 PreviousMouseDelta = Vector2.zero;
    Camera cam;
    Vector2 move;
    bool onFire, isWet;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cam = Camera.main;
        sound = GetComponent<AudioSource>();
        
    }

    
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(lightAnim.length);
        //cam.transform = camHold;
        #region
        if (sleeping == false)
        rbody.AddRelativeForce(new Vector3(move.x, 0, move.y));
        if (animTime <= lightAnim.length)
        {
            if (sleeping == true)
            {
                animTime += Time.deltaTime;

                Debug.Log(animTime);
                //walkSpeed = 0;

            }


        }
        else
        {
            sleeping = false;
            onFire = false;
            isWet = false;
            // walkSpeed = 50;
            animTime = 0;
        }


        #endregion // stopping animation logic
        water.gameObject.SetActive(isWet);
        fire.gameObject.SetActive(onFire);
        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*10, Color.red);
    }
    void OnPause()
    {
        PauseMenu.isPaused = true;   
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
        //animator.Play("RunForward")
        animator.SetFloat("Forward", move.y);
        animator.SetFloat("Horizontal", move.x);
        move *= walkSpeed;



    }

   
    void OnInteract()
    {
        int layerMask = 1 << 3;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 5, out hit, layerMask))
        {
            Debug.Log("hello");
           if (hit.collider.gameObject.tag == "Objective")
            {
                if(onFire == true)
                {
                    animator.Play("Interact");
                    sleeping = true;
                    hit.collider.gameObject.GetComponent<ObjectiveScript>().SetBool();
                    
                }
                if(isWet == true)
                {
                    animator.Play("Interact2");
                    sleeping = true;
                    hit.collider.gameObject.GetComponent<ObjectiveScript>().SetWet();
                }
               // hit.collider.gameObject
            }

        }


    }
    void OnLook(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        Quaternion addedRotation = Quaternion.AngleAxis(
            Mathf.Lerp(PreviousMouseDelta.x, input.x, 1f / HorizontalDamping) * RotationPower,
            transform.up);

        camHold.rotation *= addedRotation;

        PreviousMouseDelta = input;

        transform.rotation = Quaternion.Euler(0, camHold.rotation.eulerAngles.y, 0);

        camHold.localEulerAngles = Vector3.zero;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fire")
        {
            sound.Play();
            onFire = true;
            isWet = false;
        }
        if (other.tag == "Water")
        {
            onFire = false;
            isWet = true;
        }
    }
}
