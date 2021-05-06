using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;
    public GameObject bullet;
    public float bulletSpeed = 100f;

    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private CapsuleCollider _col;

    private GameBehavior _gameManager;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
    }

   
    void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);


        _rb.MoveRotation(_rb.rotation * angleRot);

        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;
        
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);

        if (JoystickMove.Instance.JoyVec.x != 0 || JoystickMove.Instance.JoyVec.y != 0 )
        {
            _rb.velocity = new Vector3 (JoystickMove.Instance.JoyVec.x, 0, JoystickMove.Instance.JoyVec.y ) * moveSpeed;
            _rb.rotation = Quaternion.LookRotation ( new Vector3 (JoystickMove.Instance.JoyVec.x, 0, JoystickMove.Instance.JoyVec.y ) );
        }

        /*
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position,
                this.transform.rotation) as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward * bulletSpeed;
        }
        */



    }
    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

       
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            }

        
    }
               
    

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, 
            _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom,
            distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.Lives -= 1;
        }
    }
}
