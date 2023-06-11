using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeComboVisual : MonoBehaviour
{

    [SerializeField] private Image _coctailImage;
    [SerializeField] private TextMeshProUGUI _coctailName, _bonusScore, _bonusTime;
    [SerializeField] private Transform _comboblock;
    [SerializeField] Image _imagePref;
    [SerializeField] GameObject _lock;
    
    public void Init(CoctailRecipeSO coctail)
    {
        _coctailImage.sprite = coctail.Image;
        _coctailName.text = coctail.Name;
        _bonusScore.text = coctail.BonusScore.ToString();
        _bonusTime.text = coctail.BonusTime.ToString();
        foreach(var drink in coctail.Recipe)
        {
            Instantiate(_imagePref,_comboblock).sprite = drink.Image;
            
        }
        _lock.SetActive(true);
        if (SaveProvider.Instace.SaveData.UnlockedRecipies.Contains(coctail.Name))
        {
            _lock.SetActive(false);
        }

    }
}
