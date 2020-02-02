using UnityEngine;

namespace Assets.Resources.Room_AC
{
    public class AcRemote : MonoBehaviour
    {
        public AcUnit Ac;

        public float DebounceTime = 1.0f;
        private float _lastUseTime;

        public void OnQuitButtonPressed()
        {
            var currentTime = Time.time;

            if (currentTime - _lastUseTime > DebounceTime)
            {
                Debug.Log("Quit button pressed");
                _lastUseTime = currentTime;
                Ac.Status = Ac.Status == AcUnitStatus.Off ? AcUnitStatus.Opening : AcUnitStatus.Closing;
            }
        }
    }
}
