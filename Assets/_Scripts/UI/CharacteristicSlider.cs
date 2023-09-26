using UnityEngine;
using UnityEngine.UI;

public class CharacteristicSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void Initialize(float characteristicValue)
    {
        _slider.value = characteristicValue;
    }

    private void OnValidate()
    {
        if (_slider == null)
        {
            _slider = GetComponent<Slider>();
        }
    }
}
