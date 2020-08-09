using System;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameEvents : Singleton<GameEvents>
    {
        private GameEvents() {}


        public event Action<int> OnDoorwayTriggerEnter;
        public void DoorwayTriggerEnter(int id)
        {
            OnDoorwayTriggerEnter?.Invoke(id);
        }

        public event Action<int> OnDoorwayTriggerExit;
        public void DoorwayTriggerExit(int id)
        {
            OnDoorwayTriggerExit?.Invoke(id);
        }
    }
}
