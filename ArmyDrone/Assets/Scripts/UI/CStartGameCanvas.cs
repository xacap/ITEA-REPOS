using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace UI
{
    public class CStartGameCanvas : MonoBehaviour
    {
        private void Awake()
        {
            var panelNode = gameObject.transform.Find("Panel").gameObject;
           
            var buttonObj = panelNode.transform.Find("ButtonStart").gameObject;
            var button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => StartButton());

            var toggleObj = panelNode.transform.Find("Toggle").gameObject;
            var toggle = toggleObj.GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(delegate
            {
                ToggleValueChangedOccured(toggle);
            }
                );

            var sliderObj = panelNode.transform.Find("Slider").gameObject;
            var slider = sliderObj.GetComponent<Slider>();
            slider.onValueChanged.AddListener(delegate
            {
                ChangeValueSlider(slider);
            }
                );

            var dropdownObj = panelNode.transform.Find("Dropdown").gameObject;
            var dropdown = dropdownObj.GetComponent<Dropdown>();
            dropdown.onValueChanged.AddListener(delegate
            {
                ChangeValueDropdown(dropdown);
            }
                );

            var textObj0 = panelNode.transform.Find("Text1").GetComponent<Text>();
            textObj0.text = "✪ УНИЧТОЖИТЬ 70% ВРАЖЕСКИХ СИЛ";

            var textObj1 = panelNode.transform.Find("Text2").GetComponent<Text>();
            textObj1.text = "✪ УНИЧТОЖИТЬ 100% ВРАЖЕСКИХ СИЛ";

            var textObj2 = panelNode.transform.Find("Text3").GetComponent<Text>();
            textObj2.text = "✪ СОБРАТЬ ВСЕХ ПЛЕННЫХ";

            var textObj3 = panelNode.transform.Find("Text4").GetComponent<Text>();
            textObj3.text = "✪ ОСТАТЬСЯ НЕТРОНУТЫМ";
        }

        public void Show()
        {
            
        }
        private void StartButton()
        {
            Debug.Log("Нажата кнопка START");
           // Destroy(this.gameObject);
        }

        private void ToggleValueChangedOccured(Toggle tglValue)
        {
            Debug.Log("Текущее состояние преключателя:  " + tglValue.isOn);
        }

        private void ChangeValueSlider(Slider slider)
        {
            Debug.Log("Текущее значение слайдера:  " + slider.value);
        }

        private void ChangeValueDropdown(Dropdown dropdown)
        {
            Debug.Log("Текущее значение слайдера:  " + dropdown.value);
        }
    }
}


