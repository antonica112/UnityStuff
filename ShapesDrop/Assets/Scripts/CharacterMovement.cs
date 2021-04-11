using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float _thrust = 150f;

    private Rigidbody2D rb;

    //private Light2D characterLight;
    private float defaultRadius = 2f;

    Vector3 worldPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if(animator == null)
            animator = GetComponent<Animator>();

       /*characterLight = transform.GetChild(0).GetComponent<Light2D>();
        if (characterLight == null)
            Debug.LogWarning("Light2D component not found on character's first children.");
        else defaultRadius = characterLight.pointLightOuterRadius;*/

        GameController.SetGameStarted(false);

        SoundManager.Instance.PlayGameMusic();
    }

    void CalculateLight()
    {
        /*if (characterLight != null)
        {
            float currRadius = Mathf.Clamp((defaultRadius + GameController.distanceTraveled / 100f), defaultRadius, 8f);

            characterLight.pointLightOuterRadius = currRadius;
        }*/
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (GameController.GameEnded)
            return;

        if (col.gameObject.tag == "Platform")
        {
            SoundManager.Instance.PlayImpactEffect();

            GameController.IncreaseScore(1);
            CalculateLight();

            if (animator != null)
                animator.SetTrigger("Hit");

            // Trigger Touched effect on platform
            Platform plScript = col.gameObject.GetComponent<Platform>();

            if (plScript != null)
            {
                plScript.LightPlatform();

                /*if(plScript.moveHorizontal)
                    transform.parent = col.gameObject.transform;*/
            }

            col.gameObject.tag = "Untagged";
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        //transform.parent = null;
        StartCoroutine(LeaveParent());
    }

    IEnumerator LeaveParent()
    {
        yield return new WaitForSeconds(0.5f);

        transform.parent = null;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (GameController.GameEnded)
            return;

        if (col.tag == "Enemy")
        {
            if(animator != null)
                animator.SetBool("Dead", true);

            GameController.SetEndGame(true);

            SoundManager.Instance.PlayEndGameSound();
        }
    }

    void StartGame()
    {
        rb.isKinematic = false;
        GameController.SetGameStarted(true);
    }

    void Update()
    {
        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !GameController.GameEnded)
        {
            if (!GameController.GameStarted)
                StartGame();

            MoveTo();
        }
    }

    void MoveTo()
    {
        if (GameController.GameStarted)
        {
            bool movingRight = false;

            if (worldPoint.x > transform.position.x)
                movingRight = true;

            Vector3 force = Vector3.zero;

            if (movingRight)
                force = new Vector3(1, 0, 0) * _thrust * Time.fixedDeltaTime;
            else force = new Vector3(-1, 0, 0) * _thrust * Time.fixedDeltaTime;

            rb.AddForce(force, ForceMode2D.Force);
        }
    }
}