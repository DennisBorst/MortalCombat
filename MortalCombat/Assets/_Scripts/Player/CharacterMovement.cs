using System;
using System.Collections;
using System.Collections.Generic;
using ToolBox;
using ToolBox.Injection;
using ToolBox.Input;
using UnityEngine;
using UnityEngine.Events;

namespace MortalCombat
{
    public class CharacterMovement : DependencyBehavior
    {
        [Header("General")]
        [SerializeField] private float m_JumpForce = 15f;                          // Amount of force added when the player jumps.
        [SerializeField] private float m_DoubleJumpForce = 10f;                          // Amount of force added when the player jumps.
        [Range(0, 1), SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [Range(0, .3f), SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
        [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
        [SerializeField] private float m_ShootCooldown;
        [SerializeField] private float m_MeleeCooldown;
        /*[HideInInspector] */public bool invincible;

        [Header("NeedAssignment")]
        [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
        [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
        [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
        [HideInInspector] public GameObject m_playerObject;
        [SerializeField] private Animator m_anim;
        [SerializeField] private GameObject m_Projectile;
        [SerializeField] private GameObject m_Shield;
        [SerializeField] private Transform m_FirePoint;
        [SerializeField] private GameObject m_MeleeParticle;

        [Dependency] public InputService input;

        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
        private Rigidbody2D m_rb;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private Vector3 m_Velocity = Vector3.zero;

        [Header("Events")]
        [Space]
        public UnityEvent OnLandEvent;

        [System.Serializable]
        public class BoolEvent : UnityEvent<bool> { }

        [HideInInspector] public int m_ControllerID;
        private int jumpCounter;
        private float m_hInput;
        private float m_CurrentShootCooldown;
        private float m_CurrentMeleeCooldown;
        private bool m_CanShoot;
        private bool m_CanPunch;
        [SerializeField] private bool m_InputAvaible;
        [SerializeField] private bool m_FlipCharacterOnStart;
        private SpriteRenderer[] spriteRenderers;
        private Color colorHit = new Color32(150,0,0,255);
        private Color colorHit2 = new Color32(255,0,0,255);
        private Color colorNormal = new Color32(255, 255, 255, 255);

        private bool shieldUp;

        protected override void Awake()
        {
            base.Awake();
            m_rb = GetComponent<Rigidbody2D>();
            m_ControllerID = GetComponent<CharacterID>().m_PlayerID;

            if (OnLandEvent == null)
                OnLandEvent = new UnityEvent();

            if (m_FlipCharacterOnStart)
                Flip();

            spriteRenderers = m_playerObject.GetComponentsInChildren<SpriteRenderer>();

            GlobalEvents.AddListener<GameStartMessage>(OnGameStart);
            GlobalEvents.AddListener<PlayerWinMessage>(OnPlayerWin);
        }

        private void OnDestroy()
        {
            GlobalEvents.RemoveListener<GameStartMessage>(OnGameStart);
            GlobalEvents.RemoveListener<PlayerWinMessage>(OnPlayerWin);
        }

        private void OnPlayerWin(PlayerWinMessage obj)
        {
            m_InputAvaible = false;
            GlobalEvents.RemoveListener<PlayerWinMessage>(OnPlayerWin);
        }


        private void OnGameStart(GameStartMessage obj)
        {
            m_InputAvaible = true;
            //GlobalEvents.RemoveListener<GameStartMessage>(OnGameStart);
        }

        private void FixedUpdate()
        {
            bool wasGrounded = m_Grounded;
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    if(m_rb.velocity.y < 0.5f)
                    {
                        m_Grounded = true;
                        jumpCounter = 0;
                    }
                    if(m_rb.velocity.y > 10)
                        jumpCounter = 1;

                    if (!wasGrounded)
                        OnLandEvent.Invoke();
                }
            }

            if (m_InputAvaible)
            {
                Move(m_hInput, false);

                if (m_Grounded)
                {
                    //Animations
                    if (Mathf.Abs(m_hInput) <= 0f) { AnimState(0, false); }
                    else if (Mathf.Abs(m_hInput) > 0f) { AnimState(1, false); }
                }
                else
                {
                    AnimState(5, false);
                }
            }
            else
            {
                // stop moving
                Move(0, false);
                AnimState(0, false);
            }
        }

        private void Update()
        {
            m_hInput = input.Get(m_ControllerID, "Right") -
                input.Get(m_ControllerID, "Left");                

            if (m_InputAvaible)
            {
                CheckInput();
                CoolDown();
            }
        }

        private void CheckInput()
        {
            if (input.Down(m_ControllerID, "Jump") && jumpCounter <= 1)
            {
                Move(m_hInput, true);
                AnimState(5, false);
            }

            if (input.Down(m_ControllerID, "Shield"))
            {
                ShieldActive(true);
            }
            else if (input.GetUp(m_ControllerID, null, "Shield"))
            {
                ShieldActive(false);
            }

            if (input.Down(m_ControllerID, "Punch") && m_CanPunch)
            {
                m_CanPunch = false;
                m_MeleeParticle.SetActive(true);
                AnimState(3, true);

                ShieldActive(false);
            }

            if (input.Down(m_ControllerID, "Throw") && m_CanShoot)
            {
                m_CanShoot = false;
                GameObject projectile = Instantiate(m_Projectile, m_FirePoint.position, m_FirePoint.rotation);
                projectile.GetComponent<Projectile>().m_CharacterID = GetComponent<CharacterID>();
                GlobalEvents.SendMessage(new PlayerBulletMessage(m_ControllerID, m_CanShoot));

                ShieldActive(false);
            }
        }

        public void ShieldActive(bool value)
        {
            shieldUp = value;
            m_Shield.SetActive(value);
        }

        public void Move(float move, bool jump)
        {
            if (m_Grounded || m_AirControl)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_rb.velocity.y);
                // And then smoothing it out and applying it to the character
                m_rb.velocity = Vector3.SmoothDamp(m_rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (jumpCounter == 0 && jump)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                jumpCounter += 1;
                m_rb.velocity = Vector2.up * m_JumpForce;
                //m_rb.AddForce(new Vector2(0f, m_JumpForce));
            }
            else if (jumpCounter == 1 && jump)
            {
                jumpCounter += 1;
                m_rb.velocity = Vector2.up * (m_DoubleJumpForce + (m_rb.velocity.y/4f));
            }
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            transform.Rotate(0f, 180f, 0f);
        }

        private void AnimState(int animState, bool eventTrigger)
        {
            m_anim.SetInteger("AnimState", animState);
            if (eventTrigger) { m_anim.SetTrigger("Event"); }
        }

        public void InputsAviable(bool value)
        {
            m_InputAvaible = value;
        }

        public void Hit()
        {

            AnimState(4, true);
        }

        private void ResetCooldown(bool melee)
        {
            if (melee) { m_CurrentMeleeCooldown = m_MeleeCooldown; }
            else { m_CurrentShootCooldown = m_ShootCooldown; }
        }

        private void CoolDown()
        {
            if (!m_CanPunch)
            {
                if (m_CurrentMeleeCooldown <= 0)
                {
                    m_CanPunch = true;
                    ResetCooldown(true);
                }
                else
                {
                    m_CurrentMeleeCooldown = Timer(m_CurrentMeleeCooldown);
                }
            }

            if (!m_CanShoot)
            {
                if (m_CurrentShootCooldown <= 0)
                {
                    m_CanShoot = true;
                    ResetCooldown(false);
                    GlobalEvents.SendMessage(new PlayerBulletMessage(m_ControllerID, m_CanShoot));
                }
                else
                {
                    m_CurrentShootCooldown = Timer(m_CurrentShootCooldown);
                }

            }
        }

        private float Timer(float timer)
        {
            timer -= Time.deltaTime;
            return timer;
        }

        private void InvincibilityEffect(Color color, float alpha)
        {
            color.a = alpha;

            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].color = color;
            }
        }

        public void ActivateInvincibleFrames(float time)
        {
            StartCoroutine(InvincibilityFrames(time));
        }

        private IEnumerator InvincibilityFrames(float time)
        {
            invincible = true;
            //InvincibilityEffect(0.3f);
            yield return new WaitForSeconds(time);
            invincible = false;
            //InvincibilityEffect(1f);
        }
        public IEnumerator HitFrames(float duration, float fadeOutDuration = 0.1f)
        {
            float timeDevided = duration/4;
            invincible = true;
            InvincibilityEffect(colorHit, 255);
            yield return new WaitForSeconds(timeDevided);
            InvincibilityEffect(colorHit2, 255);
            yield return new WaitForSeconds(timeDevided);
            InvincibilityEffect(colorHit, 255);
            yield return new WaitForSeconds(timeDevided);
            InvincibilityEffect(colorNormal, 255);

            float startTime = Time.time;
            float endTime = startTime + fadeOutDuration;

            while (Time.time < endTime)
            {
                Color color = Color.Lerp(colorHit, colorNormal, (Time.time - startTime) / fadeOutDuration);
                InvincibilityEffect(color, 1);
                yield return null;
            }

            InvincibilityEffect(colorNormal, 1);

            invincible = false;
        }
    }
}