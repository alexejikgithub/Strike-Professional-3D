using UnityEngine;

namespace Scripts.Input
{
    public abstract class GameplayInput : MonoBehaviour
    {
        public abstract void GameplayInputOn();

        public abstract void GameplayInputOff();
    }
}