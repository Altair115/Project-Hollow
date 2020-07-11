using UnityEngine;

namespace Assets.Scripts
{
    [AddComponentMenu("Mythirial Test/Camera Test")]
    [RequireComponent(typeof(Camera))]
    public class CameraTest : MonoBehaviour
    {
        [SerializeField]
        private const float Y_ANGLE_MIN = 0.0f;
        [SerializeField]
        private const float Y_ANGLE_MAX = 50.0f;

        Camera cam;

        public bool lockCursor;
        public Transform lookAt;
        public float dstFromTarget = 5f;
        public float dstFactor = .5f;

        public float rotationSmoothTime = .12f;
		public float depthSmoothTime = 1;
        Vector3 rotationSmoothVelocity;
        Vector3 currentRotation;

        public float dstMax = 5f;
        Vector3 dollyDir;

        private float currentX = 0.0f;
        private float currentY = 45.0f;
        private float sensitivityX = 4.0f;
        private float sensitivityY = 1.0f;

        private void Awake()
        {
            cam = GetComponent<Camera>();
            dollyDir = transform.localPosition.normalized;
            dstFromTarget = transform.localPosition.magnitude;

            if (lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void LateUpdate()
        {
            CamPan();
        }

        /// <summary>
        /// Panning the camera
        /// </summary>
        void CamPan()
        {
            CollisionDetect();
            currentX += Input.GetAxis("Look Horizontal") * sensitivityX ;
            currentY += Input.GetAxis("Look Vertical") * sensitivityY;
            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
       
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(currentY, currentX), ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;

            transform.position = lookAt.position - transform.forward * Mathf.Clamp(dstFromTarget, 0, dstMax);
            //transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * dstFromTarget, Time.deltaTime * rotationSmoothTime);
        }

        /// <summary>
        /// Detecting collision for the camera to protect it from clipping through walls
        /// </summary>
        Vector3 cam_Direction;
        void CollisionDetect()
        {
            cam_Direction = -(lookAt.position - transform.position);
            RaycastHit cam_colission;
            
            if (Physics.SphereCast(lookAt.position, dstFactor, cam_Direction, out cam_colission, dstFromTarget)) {
                dstFromTarget = cam_colission.distance;
            }
            else
                dstFromTarget = Mathf.Lerp(dstFromTarget, dstMax, Time.deltaTime * depthSmoothTime);
        }
    }
}