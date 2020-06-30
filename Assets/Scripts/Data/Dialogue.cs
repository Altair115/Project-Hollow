using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Dialogue Data Class
    /// </summary>
    [System.Serializable]
    public class Dialogue
    {
        public string first_name;
        public string last_name;
        [TextArea(3,10)]
        public List<string> sentences;
    }
}
