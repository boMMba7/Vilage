using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    private void Start()
    {
        SetMaxPower(100);
    }

    public void SetMaxPower(int power)
    {
        _slider.maxValue = power;        
    }

    public void AddPower(int power)
    {
        if(power + _slider.value > _slider.maxValue)
        {
            _slider.value = _slider.maxValue;
        }
        else
        {
            _slider.value += power;
        }
    }

    //retorna falso caso nao tenha energia suficiente
    public bool UsePower(int amountPower)
    {
        if(_slider.value < amountPower)
        {
            return false;
        }
        _slider.value -= amountPower; 
        return true;
    }
}
