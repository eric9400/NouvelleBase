using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Photon.MonoBehaviour
{
    public new PhotonView photonView;
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject PlayerCamera;
    public SpriteRenderer sr;
    public Text PlayerNameText;

    public bool IsGrounded;
    public float MoveSpeed;
    public float JumpForce;

    public Transform playerPos;
    public float positionRadius;
    public LayerMask ground;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    void Start()
    {
        isJumping = false;
    }

    private void Awake()
    {
        if (photonView.isMine)
        {
            PlayerCamera.SetActive(true);
            PlayerNameText.text = PhotonNetwork.playerName;
        }
        else
        {
            PlayerNameText.text = photonView.owner.NickName;
            PlayerCamera.SetActive(false);
        }
    }

    private void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);
        if(photonView.isMine)
        {
            CheckInput();
            Jump();

        }
    }

    void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);
        if(photonView.isMine)
        {
            Jump();
        }
    }

    private void Jump()
    {   
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001)
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            
        }
    }

    private void CheckInput()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        transform.position += move * MoveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);
        }

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    [PunRPC]
    private void FlipTrue()
    {
        sr.flipX = true;
    }

    [PunRPC]
    private void FlipFalse()
    {
        sr.flipX = false;
    }
}
