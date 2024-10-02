using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

namespace Udey.Scripts
{
    public class Rng : MonoBehaviour
    {
        #region Veriables

        [SerializeField] private GameObject cardDropDownPrefab;

        [Header("Headers")] [SerializeField] private TMP_Text[] headers = new TMP_Text[3];

        [Header("Descriptions")] [SerializeField]
        private TMP_Text[] descriptions = new TMP_Text[3];

        private List<int> _option;
        private const string ErrorMessage = "error";

        #endregion

        private void Start()
        {
            UseOptions(GetRandomOption());
        }

        #region RNG

        private List<int> GetRandomOption()
        {
            _option = Enumerable.Range(1, 10).OrderBy(x => Random.value).Take(3).Distinct().ToList();
            return _option;
        }

        #endregion

        #region Text

        private void UseOptions(List<int> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                headers[i].text = GetOptionText(options[i]);
            }
        }

        private string GetOptionText(int option)
        {
            return option switch
            {
                0 => "Increase Enemy Health",
                1 => "Increase Enemy Damage",
                2 => "Increase Enemy Health Regeneration",
                3 => "Increase Enemy Attack Speed",
                4 => "Increase Enemy Movement Speed",
                5 => "Increase Enemy Attack Range",
                6 => "Not Added Yet",
                7 => "Increase Enemy Defence",
                8 => "Increase Enemy Dodge Chance",
                9 => "Increase Enemy Critical Chance",
                10 => "Increase Enemy Critical Damage",
                _ => ErrorMessage
            };
        }

        #endregion
    }
}