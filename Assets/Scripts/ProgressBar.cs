using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Assets.Scripts
{
    [ExecuteInEditMode]
    public class ProgressBar : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/UI/Linear Progress Bar")]
        public static void AddLinearProgressBar()
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
            go.transform.SetParent(Selection.activeGameObject.transform, false);
        }
        [MenuItem("GameObject/UI/Radial Progress Bar")]
        public static void AddRadialProgressBar()
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("UI/Radial Progress Bar"));
            go.transform.SetParent(Selection.activeGameObject.transform, false);
        }
#endif



        public int Minimum;
        public int Maximum;
        public int Current;
        public Image Mask;
        public Image Fill;
        [SerializeField] private Color _color = Color.white;

        // Update is called once per frame
        void Update()
        {
            GetCurrentFill();
        }

        void GetCurrentFill()
        {
            float currentOffset = Current - Minimum;
            float maximumOffset = Maximum - Minimum;
            float fillAmount = currentOffset / maximumOffset;
            Mask.fillAmount = fillAmount;
            Fill.color = _color;
        }
    }
}
