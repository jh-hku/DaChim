using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class used to control a player in a Runner
    /// game. Includes logic for player movement as well as 
    /// other gameplay logic.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        private int playerLife = 10;
        /// <summary> Returns the PlayerController. </summary>
        public static PlayerController Instance => s_Instance;
        static PlayerController s_Instance;

        [SerializeField]
        Animator m_Animator;

        [SerializeField]
        SkinnedMeshRenderer m_SkinnedMeshRenderer;

        [SerializeField]
        PlayerSpeedPreset m_PlayerSpeed = PlayerSpeedPreset.Medium;

        [SerializeField]
        float m_CustomPlayerSpeed = 10.0f;

        [SerializeField]
        float m_AccelerationSpeed = 3.0f;

        [SerializeField]
        float m_DecelerationSpeed = 10.0f;

        [SerializeField]
        float m_HorizontalSpeedFactor = 0.5f;

        [SerializeField]
        float m_ScaleVelocity = 2.0f;

        [SerializeField]
        bool m_AutoMoveForward = true;

        Vector3 m_LastPosition;
        float m_StartHeight;

        const float k_MinimumScale = 0.1f;
        static readonly string s_Speed = "Speed";

        enum PlayerSpeedPreset
        {
            Slow,
            Medium,
            Fast,
            Custom
        }

        Transform m_Transform;
        Vector3 m_StartPosition;
        bool m_HasInput;
        float m_MaxXPosition;
        float m_XPos;
        float m_ZPos;
        float m_TargetPosition;
        float m_Speed;
        float m_TargetSpeed;
        Vector3 m_Scale;
        Vector3 m_TargetScale;
        Vector3 m_DefaultScale;

        private int desiredLane = 1; //0Left, 1Mid, 2Right
        public float laneDistance = 5.5f; //dis btwn 2 lanes
        public float jumpForce = 2000;
        public float gravity = -9.81f;
        float velocity;


        const float k_HalfWidth = 0.5f;

        /// <summary> The player's root Transform component. </summary>
        public Transform Transform => m_Transform;

        /// <summary> The player's current speed. </summary>
        public float Speed => m_Speed;

        /// <summary> The player's target speed. </summary>
        public float TargetSpeed => m_TargetSpeed;

        /// <summary> The player's minimum possible local scale. </summary>
        public float MinimumScale => k_MinimumScale;

        /// <summary> The player's current local scale. </summary>
        public Vector3 Scale => m_Scale;

        /// <summary> The player's target local scale. </summary>
        public Vector3 TargetScale => m_TargetScale;

        /// <summary> The player's default local scale. </summary>
        public Vector3 DefaultScale => m_DefaultScale;

        /// <summary> The player's default local height. </summary>
        public float StartHeight => m_StartHeight;

        /// <summary> The player's default local height. </summary>
        public float TargetPosition => m_TargetPosition;

        /// <summary> The player's maximum X position. </summary>
        public float MaxXPosition => m_MaxXPosition;



        void Awake()
        {
            
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;

            Initialize();
        }

        /// <summary>
        /// Set up all necessary values for the PlayerController.
        /// </summary>
        /// 
        

        public void Initialize()
        {
            m_Transform = transform;
            m_StartPosition = m_Transform.position;
            m_DefaultScale = m_Transform.localScale;
            m_Scale = m_DefaultScale;
            m_TargetScale = m_Scale;


            if (m_SkinnedMeshRenderer != null)
            {
                m_StartHeight = m_SkinnedMeshRenderer.bounds.size.y;
            }
            else 
            {
                m_StartHeight = 1.0f;
            }
           
            m_Animator.SetInteger("arms", 2);
            m_Animator.SetInteger("legs", 2);
            ResetSpeed();
        }

        /// <summary>
        /// Returns the current default speed based on the currently
        /// selected PlayerSpeed preset.
        /// </summary>
        public float GetDefaultSpeed()
        {
            switch (m_PlayerSpeed)
            {
                case PlayerSpeedPreset.Slow:
                    return 5.0f;

                case PlayerSpeedPreset.Medium:
                    return 10.0f;

                case PlayerSpeedPreset.Fast:
                    return 20.0f;
            }

            return m_CustomPlayerSpeed;
        }

        /// <summary>
        /// Adjust the player's current speed
        /// </summary>
        public void AdjustSpeed(float speed)
        {
            m_TargetSpeed += speed;
            m_TargetSpeed = Mathf.Max(0.0f, m_TargetSpeed);
        }

        /// <summary>
        /// Reset the player's current speed to their default speed
        /// </summary>
        public void ResetSpeed()
        {
            m_Speed = 0.0f;
            m_TargetSpeed = GetDefaultSpeed();
        }

        /// <summary>
        /// Adjust the player's current scale
        /// </summary>
        public void AdjustScale(float scale)
        {
            m_TargetScale += Vector3.one * scale;
            m_TargetScale = Vector3.Max(m_TargetScale, Vector3.one * k_MinimumScale);
        }

        /// <summary>
        /// Reset the player's current speed to their default speed
        /// </summary>
        public void ResetScale()
        {
            m_Scale = m_DefaultScale;
            m_TargetScale = m_DefaultScale;
        }

        /// <summary>
        /// Returns the player's transform component
        /// </summary>
        public Vector3 GetPlayerTop()
        {
            return m_Transform.position + Vector3.up * (m_StartHeight * m_Scale.y - m_StartHeight);
        }

        /// <summary>
        /// Sets the target X position of the player
        /// </summary>
        public void SetDeltaPosition(float normalizedDeltaPosition)
        {
            // if (m_MaxXPosition == 0.0f)
            // {
            //     Debug.LogError("Player cannot move because SetMaxXPosition has never been called or Level Width is 0. If you are in the LevelEditor scene, ensure a level has been loaded in the LevelEditor Window!");
            // }
            m_MaxXPosition  = 7.1f;
            float fullWidth = 10 * 2.0f;
            m_TargetPosition = m_TargetPosition + fullWidth * normalizedDeltaPosition;
            m_TargetPosition = Mathf.Clamp(m_TargetPosition, -m_MaxXPosition, m_MaxXPosition);
            m_HasInput = true;
        }

        /// <summary>
        /// Stops player movement
        /// </summary>
        public void CancelMovement()
        {
            m_HasInput = false;
        }

        /// <summary>
        /// Set the level width to keep the player constrained
        /// </summary>
        public void SetMaxXPosition(float levelWidth)
        {
            // Level is centered at X = 0, so the maximum player
            // X position is half of the level width
            m_MaxXPosition = levelWidth * k_HalfWidth;
        }

        /// <summary>
        /// Returns player to their starting position
        /// </summary>
        public void ResetPlayer()
        {
            m_Transform.position = m_StartPosition;
            m_XPos = 0.0f;
            m_ZPos = m_StartPosition.z;
            m_TargetPosition = 0.0f;

            m_LastPosition = m_Transform.position;

            m_HasInput = false;

            ResetSpeed();
            ResetScale();
        }

        void Update()
        {
            if (IsDied()) 
            {
                return;
            }
            


            float deltaTime = Time.deltaTime;

            // Update Scale

            //if (!Approximately(m_Transform.localScale, m_TargetScale))
            //{
            //    m_Scale = Vector3.Lerp(m_Scale, m_TargetScale, deltaTime * m_ScaleVelocity);
            //    m_Transform.localScale = m_Scale;
            //}

            // Update Speed

            if (!m_AutoMoveForward && !m_HasInput)
            {
                Decelerate(deltaTime, 0.0f);
            }
            else if (m_TargetSpeed < m_Speed)
            {
                Decelerate(deltaTime, m_TargetSpeed);
            }
            else if (m_TargetSpeed > m_Speed)
            {
                Accelerate(deltaTime, m_TargetSpeed);
            }

            float speed = m_Speed * deltaTime;

            // Update position

            m_ZPos += speed;

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                desiredLane ++;
                if(desiredLane == 3)
                {
                    desiredLane = 2;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                desiredLane--;
                if(desiredLane == -1)
                {
                    desiredLane = 0;
                }
            }
            //velocity += gravity * Time.deltaTime;
            velocity = 0;
            //if (Input.GetKeyDown(KeyCode.UpArrow))
            //{
            //    Debug.Log("Up");
                
            //    velocity = jumpForce;
            //}
            m_Transform.Translate(new Vector3(0,velocity,0) * Time.deltaTime);

            m_Transform.position = new Vector3(m_XPos, m_Transform.position.y, m_ZPos);

            if (desiredLane == 0)
            {
                m_Transform.position += Vector3.left * laneDistance;

            } else if (desiredLane == 2)
            {
                m_Transform.position += Vector3.right * laneDistance;
            }

            

            if (m_HasInput)
            {
                // float horizontalSpeed = speed * m_HorizontalSpeedFactor;

                // float newPositionTarget = Mathf.Lerp(m_XPos, m_TargetPosition, horizontalSpeed);
                // float newPositionDifference = newPositionTarget - m_XPos;

                // newPositionDifference = Mathf.Clamp(newPositionDifference, -horizontalSpeed, horizontalSpeed);
                
                // m_XPos += newPositionDifference;
            }

            

            //if (m_Animator != null && deltaTime > 0.0f)
            //{
            //    float distanceTravelledSinceLastFrame = (m_Transform.position - m_LastPosition).magnitude;
            //    float distancePerSecond = distanceTravelledSinceLastFrame / deltaTime;

            //    //m_Animator.SetFloat(s_Speed, distancePerSecond);
            //}

            //if (m_Transform.position != m_LastPosition)
            //{
            //    m_Transform.forward = Vector3.Lerp(m_Transform.forward, (m_Transform.position - m_LastPosition).normalized, speed);
            //}

            m_LastPosition = m_Transform.position;
            m_TargetSpeed += deltaTime * m_AccelerationSpeed;
        }

        void Accelerate(float deltaTime, float targetSpeed)
        {
            m_Speed += deltaTime * m_AccelerationSpeed;
            m_Speed = Mathf.Min(m_Speed, targetSpeed);
        }

        void Decelerate(float deltaTime, float targetSpeed)
        {
            m_Speed -= deltaTime * m_DecelerationSpeed;
            m_Speed = Mathf.Max(m_Speed, targetSpeed);
        }

        bool Approximately(Vector3 a, Vector3 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z);
        }

        public void GotHitted()
        {
            DecreasePlayerLife(1);
        }
        public void BeingCured()
        {
            IncreasePlayerLife(1);
        }
        

        public int GetPlayerLife()
        {
            return playerLife;
        }
        private void DecreasePlayerLife(int lifeDecreased)
        {
            playerLife -= lifeDecreased;
        }
        private void IncreasePlayerLife(int lifeIncreased)
        {
            playerLife += lifeIncreased;
        }
        public bool IsDied()
        {
            return playerLife < 0;
        }

        
    }
}