using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [AddComponentMenu("Mythirial/Dialogue Manager")]
    public class DialogueManager : Singleton<DialogueManager>
    {
        public Text NameText;
        public Text DialogueText;

        public Animator Animator;

        private Queue<string> _sentences;
        
        private DialogueManager(){}

        // Start is called before the first frame update
        void Start()
        {
            _sentences = new Queue<string>();
        }

        /// <summary>
        /// Trigger for the start of a conversation
        /// show's the dialogue box with the name and first sentence
        /// </summary>
        /// <param name="dialogue"></param>
        public void StartDialogue(Dialogue dialogue)
        {
            Animator.SetBool("IsOpen", true);
            NameText.text = dialogue.first_name;

            _sentences.Clear();

            foreach (var sentence in dialogue.sentences)
            {
                _sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }

        /// <summary>
        /// Trigger for the end of a conversation
        /// </summary>
        public void EndDialogue()
        {
            Animator.SetBool("IsOpen", false);
        }

        /// <summary>
        /// Display Next sentence in the conversation
        /// </summary>
        public void DisplayNextSentence()
        {
            if (_sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            var sentence = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        /// <summary>
        /// A Co-routine Animates sentences by letter
        /// </summary>
        /// <param name="sentence">sentence to animate</param>
        /// <returns></returns>
        private IEnumerator TypeSentence(string sentence)
        {
            DialogueText.text = String.Empty;
            foreach (var letter in sentence.ToCharArray())
            {
                DialogueText.text += letter;
                yield return null;
            }
        }
    }
}