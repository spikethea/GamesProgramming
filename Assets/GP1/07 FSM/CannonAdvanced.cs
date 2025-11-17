using UnityEngine;

namespace AlanZucconi.GP1.Lecture07
{
    // An advanced version of the Cannon script
    public class CannonAdvanced : MonoBehaviour
    {
        public enum CannonState
        {
            Aiming,
            Charging,
            Waiting
        }
        public CannonState State = CannonState.Aiming;


        // Update is called once per frame
        void Update()
        {
            switch (State)
            {
                case CannonState.Aiming:
                    Aim();

                    if (SpacePressed())
                    {
                        StartCharging();
                        State = CannonState.Charging;
                    }
                    break;

                case CannonState.Charging:
                    Charge();

                    if (SpaceReleased())
                    {
                        FireProjectile();
                        CurrentCharge = 0;
                        State = CannonState.Waiting;
                    }
                    break;

                case CannonState.Waiting:
                    if (ProjectileStopped())
                    {
                        State = CannonState.Aiming;
                    }
                    break;

            }
        }


        

        [Header("Aiming")]
        [Range(0, 1)]
        public float RotationSpeed = 1; // 1 = full rotation between T0,T1 in 1 sec

        [Header("Horizontal Rotation")]
        [Range(0, 1)]
        public float H = 0.5f;
        public Transform BarrelH;
        public Transform H0;
        public Transform H1;

        [Header("Vertical Rotation")]
        [Range(0, 1)]
        public float V = 0;
        public Transform BarrelV;
        public Transform V0;
        public Transform V1;


        void Awake()
        {
            RecoilObjectLocalPosition = RecoilObject.localPosition;
        }


        // Moves the barrel
        void Aim ()
        {
            H += Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime;
            H = Mathf.Clamp01(H);

            BarrelH.localRotation = Quaternion.Slerp(H0.localRotation, H1.localRotation, H);


            V += Input.GetAxis("Vertical") * RotationSpeed * Time.deltaTime;
            V = Mathf.Clamp01(V);

            BarrelV.localRotation = Quaternion.Slerp(V0.localRotation, V1.localRotation, V);
        }
        


        // Return true when the spacebar is pressed
        bool SpacePressed()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }


        [Header("Charging")]
        public float CurrentCharge = 0;
        [Range(0,10f)]
        public float ChargingSpeed = 1f;

        void StartCharging ()
        {

        }

        // Charges the power of the projectile
        //  counting the time (in seconds) the spacebar was being pressed
        void Charge()
        {
            CurrentCharge += ChargingSpeed * Time.deltaTime;
        }

        // Returns true when the spacebar is released
        bool SpaceReleased()
        {
            return Input.GetKeyUp(KeyCode.Space);
        }

        [Header("Shooting")]
        public Transform FirePoint;
        public Bullet ProjectilePrefab;
        public Bullet Projectile;

        // Instantiate a new projectile where the cannon is,
        //  and launches the projectile based on the current charge
        void FireProjectile()
        {
            Projectile = Instantiate(ProjectilePrefab,
                                      FirePoint.position, FirePoint.rotation);
            Projectile.Rigidbody.linearVelocity = Projectile.transform.forward * CurrentCharge;

            
            // Sound
            Audio?.PlayOneShot(FireClip);

            // TODO: Cannon animation?

            // Particles
            FireParticles?.Play();
            SmokeParticles?.Play();

            // Recoil
            FireTime = Time.time;
        }

        // Returns true when the velocity of the Projectile is small enough
        bool ProjectileStopped()
        {
            return Projectile.Rigidbody.linearVelocity.magnitude < 1f;
        }

        [Header("Sound")]
        public AudioSource Audio;
        public AudioClip FireClip;

        [Header("Recoil")]
        public Transform RecoilObject;
        public AnimationCurve RecoilCurve;
        public float RecoilAmount;
        // The original local position of the object to move during the recoil
        private Vector3 RecoilObjectLocalPosition;
        private float FireTime = float.NegativeInfinity; // Never

        [Header("Particles")]
        public ParticleSystem FireParticles;
        public ParticleSystem SmokeParticles;

        void LateUpdate()
        {
            float t = Mathf.Clamp01(Time.time - FireTime);
            float recoil = RecoilCurve.Evaluate(t);
            RecoilObject.localPosition =
                RecoilObjectLocalPosition -
                Vector3.forward * RecoilAmount * recoil;
        }
    }
}