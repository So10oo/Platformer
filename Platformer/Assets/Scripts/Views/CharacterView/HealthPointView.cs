using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class HealthPointView : View<float>
{
    [SerializeField] Image _hpBar;

    public override void ViewData(float data)
    {
        _hpBar.fillAmount = data;
    }
}

