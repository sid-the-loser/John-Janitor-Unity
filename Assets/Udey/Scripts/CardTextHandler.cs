using TMPro;
using UnityEngine;

namespace Udey.Scripts
{
    public class CardTextHandler : MonoBehaviour
    {
        [SerializeField] private GameObject cardSelection;

        [Header("Left")] [SerializeField] private TMP_Text leftHeader;
        [SerializeField] private TMP_Text leftDescription;

        [Header("Middle")] [SerializeField] private TMP_Text middleHeader;
        [SerializeField] private TMP_Text middleDescription;

        [Header("Right")] [SerializeField] private TMP_Text rightHeader;
        [SerializeField] private TMP_Text rightDescription;


        private void OnEnable()
        {
            CheckCardSelectionActivation();
        }

        private void CheckCardSelectionActivation()
        {
            if (cardSelection != null && cardSelection.activeSelf)
            {
                void SetHeaderText(params TMP_Text[] headers)
                {
                    foreach (var header in headers) header.text = "header";
                }

                void SetDescriptionText(params TMP_Text[] descriptions)
                {
                    foreach (var description in descriptions) description.text = "description";
                }

                SetHeaderText(leftHeader, rightHeader, middleHeader);
                SetDescriptionText(leftDescription, rightDescription, middleDescription);
            }
        }
    }
}