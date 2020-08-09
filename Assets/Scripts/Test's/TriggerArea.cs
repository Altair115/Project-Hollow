using UnityEngine;

namespace Assets.Scripts
{
    public class TriggerArea : MonoBehaviour
    {
        [SerializeField] private int _id = 0;

        void Start()
        {
            _id = transform.parent.gameObject.GetInstanceID();
        }

        private void OnTriggerEnter(Collider other)
        {
            GameEvents.Instance.DoorwayTriggerEnter(_id);
        }

        private void OnTriggerExit(Collider other)
        {
            GameEvents.Instance.DoorwayTriggerExit(_id);
        }
    }
}
