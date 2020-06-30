using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// This class is made for test purposes later to be implemented in other class
    /// </summary>
    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue Dialogue;

        public void TriggerDialogue()
        {
            DialogueManager.Instance.StartDialogue(Dialogue);
        }
    }
}