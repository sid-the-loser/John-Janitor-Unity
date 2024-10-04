using System.Collections.Generic;
using System.Linq;
using Sid.Scripts.Common;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

namespace Udey.Scripts
{
    public class GameManager : MonoBehaviour
    {
        #region Veriables
        
        #region TextVer
        
        [SerializeField] private GameObject cardDropDownPrefab;
        [Header("Headers")] [SerializeField] private TMP_Text[] headers = new TMP_Text[3];
        [Header("Descriptions")] [SerializeField] private TMP_Text[] descriptions = new TMP_Text[3];
        private List<int> _option;
        private const string ErrorMessage = "error";
        
        #endregion
        #region StatVer
        
        private float _maxHealth = GlobalVariables.MaxHealth;
        private float _baseDamage = GlobalVariables.BaseDamage;
        private float _baseHpRegen = GlobalVariables.BaseHpRegen;
        private float _attSpeed = GlobalVariables.AttSpeed;
        private float _moveSpeed = GlobalVariables.MoveSpeed;
        private float _attRange = GlobalVariables.AttRange;
        private float _baseDefense = GlobalVariables.BaseDefense;
        private float _dodgeChance = GlobalVariables.DodgeChance;
        private float _critChance = GlobalVariables.CritChance;
        private float _critDamage = GlobalVariables.CritDamage;
        
        #endregion
        
        #endregion

        private void Start()
        {
            foreach (var header in headers)
            {
                header.fontSize = 30;
            }
            foreach (var description in descriptions)
            {
                description.fontSize = 30;
            }

            UseOptions(GetRandomOption());
        }
        private void DeactivateCardDropDown()
        {
            cardDropDownPrefab.SetActive(false);
        }
        private void Update()
        {
            if (cardDropDownPrefab.activeSelf)
            {
                GlobalVariables.Paused = true;
            }
        }

        #region RNG

        private List<int> GetRandomOption()
        {
            _option = Enumerable.Range(0, 10).OrderBy(x => Random.value).Take(3).Distinct().ToList();
            return _option;
        }

        #endregion
        #region Text

        private void UseOptions(List<int> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                headers[i].text = GetHeaderText(options[i]);
                descriptions[i].text = GetDescText(options[i]);
            }
        }
        private string GetHeaderText(int option)
        {
            return option switch
            {
                0 => "The Enemies Feast",
                1 => "The Enemies Sharpen Their Claws",
                2 => "The Enemies Blood Boils",
                3 => "The Enemies Muscle Relax",
                4 => "The Enemies Become Nimble",
                5 => "The Enemies Arms Grow longer",
                6 => "Not Added Yet",
                7 => "The Enemies Grow Harder",
                8 => "The Enemies Become Agile",
                9 => "The Enemies Feel Lucky",
                10 => "The Enemies Aims Better",
                _ => ErrorMessage
            };
        }
        private string GetDescText(int option)
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
        #region CardSelection

        public void SelectedCardLeft()
        {
            ChangeStats(descriptions[0].text);
            DeactivateCardDropDown();
        }
        public void SelectedCardMiddle()
        {
            ChangeStats(descriptions[1].text);
            DeactivateCardDropDown();
        }
        public void SelectedCardRight()
        {
            ChangeStats(descriptions[2].text);
            DeactivateCardDropDown();
        }
        private void ChangeStats(string pickedOption)
        {
            switch (pickedOption)
            {
                case "Increase Enemy Health":
                    _maxHealth = _maxHealth * 1.10f;
                    break;
                case "Increase Enemy Damage":
                    _baseDamage = _baseDamage * 1.10f;
                    break;
                case "Increase Enemy Health Regeneration":
                    _baseHpRegen = _baseHpRegen * 1.05f;
                    break;
                case "Increase Enemy Attack Speed":
                    _attSpeed = _attSpeed * 1.05f;
                    break;
                case "Increase Enemy Movement Speed":
                    _moveSpeed = _moveSpeed * 1.10f;
                    break;
                case "Increase Enemy Attack Range":
                    _attRange = _attRange * 1.15f;
                    break;
                case "Not Added Yet":
                    Debug.Log("Not Added Yet");
                    break;
                case "Increase Enemy Defence":
                    _baseDefense = _baseDefense * 1.10f;
                    break;
                case "Increase Enemy Dodge Chance":
                    _dodgeChance = _dodgeChance + (_dodgeChance*0.05f);
                    break;
                case "Increase Enemy Critical Chance":
                    _critChance = _critChance + (_critChance*0.05f);
                    break;
                case "Increase Enemy Critical Damage":
                    _critDamage = _critDamage + (_critDamage*0.02f);
                    break;
            }
        }
        #endregion
    }
}