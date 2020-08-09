using UnityEngine;

namespace Assets.Scripts
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private int _id = 0;

        // Start is called before the first frame update
        private void Start()
        {
            GameEvents.Instance.OnDoorwayTriggerEnter += OnDoorwayOpen;
            GameEvents.Instance.OnDoorwayTriggerExit += OnDoorwayClose;
            _id = gameObject.GetInstanceID();
            
        }

        private void OnDoorwayOpen(int id)
        {
            if (id == _id) LeanTween.moveLocalY(gameObject, 10f, 1f).setEaseOutQuad();
            Debug.Log("Child id:"+_id + " id passed through method:"+id);
        }
        private void OnDoorwayClose(int id)
        {
            if (id == _id) LeanTween.moveLocalY(gameObject, 1f, 1f).setEaseInQuad();
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnDoorwayTriggerEnter -= OnDoorwayOpen;
            GameEvents.Instance.OnDoorwayTriggerExit -= OnDoorwayClose;
        }
    }
}
