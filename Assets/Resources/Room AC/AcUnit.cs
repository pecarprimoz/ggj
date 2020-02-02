using System;
using UnityEngine;

namespace Assets.Resources.Room_AC
{
    public enum AcUnitStatus
    {
        Opening,
        Operating,
        Closing,
        Off
    }

    public class AcUnit : MonoBehaviour
    {
        public AcUnitStatus Status;

        public TextMesh TemperatureTextMesh;

        public float Temperature = 69;
        public float TemperatureWarming = 69;
        public Color32 TemperatureWarmingColor;

        public float TemperatureFixed = -10;
        public Color32 TemperatureFixedColor;
        public float TemperatureChange = 1f;

        public string TemperatureText => (int)Temperature + "°c";

        public GameObject FlapOne;
        public Vector3 FlapOneClosedRotation;
        public Vector3 FlapOneOperatingInitialRotation;
        public Vector3 FlapOneOperatingFinalRotation;

        public GameObject FlapTwo;
        public Vector3 FlapTwoClosedRotation;
        public Vector3 FlapTwoOperatingInitialRotation;
        public Vector3 FlapTwoOperatingFinalRotation;

        public bool FlapsGoingDown;
        public float FlapsOperatingMovementSpeed;
        public float FlapsOperatingOpenPercentage;

        public float FlapsClosingSpeed;



        private void Update()
        {
            switch (Status)
            {
                case AcUnitStatus.Opening:
                    Opening();
                    break;
                case AcUnitStatus.Operating:
                    Operating();
                    break;
                case AcUnitStatus.Closing:
                    Closing();
                    break;
                case AcUnitStatus.Off:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Status == AcUnitStatus.Operating)
            {
                if (Temperature > TemperatureFixed)
                {
                    Temperature -= TemperatureChange * Time.deltaTime;
                }
            }
            else
            {
                if (Temperature < TemperatureWarming)
                {
                    Temperature += TemperatureChange * Time.deltaTime;
                }
            }

            TemperatureTextMesh.text = TemperatureText;


            if (Temperature < 0)
            {
                TemperatureTextMesh.color = TemperatureFixedColor;
            }
            else
            {
                TemperatureTextMesh.color = TemperatureWarmingColor;
            }
        }

        private void Opening()
        {
            if (FlapOne.transform.eulerAngles.x < FlapOneOperatingInitialRotation.x)
            {
                FlapOne.transform.eulerAngles += transform.right * FlapsClosingSpeed * Time.deltaTime;
            }

            if (FlapTwo.transform.eulerAngles.x < FlapTwoOperatingInitialRotation.x)
            {
                FlapTwo.transform.eulerAngles += transform.right * FlapsClosingSpeed * Time.deltaTime;
            }

            if (FlapOne.transform.eulerAngles.x >= FlapOneOperatingInitialRotation.x && FlapTwo.transform.eulerAngles.x >= FlapTwoOperatingInitialRotation.x)
            {
                Status = AcUnitStatus.Operating;
            }
        }

        private void Operating()
        {
            FlapsOperatingOpenPercentage += (FlapsGoingDown ? FlapsOperatingMovementSpeed : -FlapsOperatingMovementSpeed) * Time.deltaTime;

            if (FlapsOperatingOpenPercentage <= 0.0f)
            {
                FlapsGoingDown = true;
            }
            else if (FlapsOperatingOpenPercentage >= 1.0f)
            {
                FlapsGoingDown = false;
            }

            FlapOne.transform.rotation = Quaternion.Lerp(Quaternion.Euler(FlapOneOperatingInitialRotation), Quaternion.Euler(FlapOneOperatingFinalRotation), FlapsOperatingOpenPercentage);
            FlapTwo.transform.rotation = Quaternion.Lerp(Quaternion.Euler(FlapTwoOperatingInitialRotation), Quaternion.Euler(FlapTwoOperatingFinalRotation), FlapsOperatingOpenPercentage);


            if (Temperature > TemperatureFixed)
            {
                Temperature -= TemperatureChange * Time.deltaTime;
                TemperatureTextMesh.text = TemperatureText;
            }
        }

        private void Closing()
        {
            if (FlapOne.transform.eulerAngles.x > 1.0f)
            {
                FlapOne.transform.eulerAngles -= transform.right * FlapsClosingSpeed * Time.deltaTime;
            }

            Debug.Log($"Flap one: {FlapOne.transform.eulerAngles.x}");

            if (FlapTwo.transform.eulerAngles.x > 1.0f)
            {
                FlapTwo.transform.eulerAngles -= transform.right * FlapsClosingSpeed * Time.deltaTime;
            }

            Debug.Log($"Flap two: {FlapTwo.transform.eulerAngles.x}");

            if (FlapOne.transform.eulerAngles.x <= 1.0f && FlapTwo.transform.eulerAngles.x <= 1.0f)
            {
                Status = AcUnitStatus.Off;
            }
        }
    }
}
