//-------------------------------------------------------------------------------------
//	main.cs
//
//	Created by 浅墨
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class main : MonoBehaviour
{
    public Text Price = null;
    private float PriceValue = 0;

    public Text Number = null;
    private float NumberValue = 0;

    [Header("欠款金额")]
    public Text ArrearsAmount = null;
    private float ArrearsAmountValue = 0;

    [Header("保证金比例")]
    public Text GuaranteeRatio = null;
    public float GuaranteeRatioValue = 0;

    [Header("抵押额")]
    public float MortgageValue = 0;

    //抵押比例
    public float MortgageRatio = 0.5f;

    [Header("总市值")]
    public Text TotalMarketCapitalization = null;
    public float TotalMarketCapitalizationValue = 0;


    void Start()
    {

    }

    //--------------------------------------------------------
    /// 计算保证金比例
    //--------------------------------------------------------
    float GetGuaranteeRatioValue()
    {
        PriceValue = Convert.ToSingle(Price.text);
        NumberValue = Convert.ToSingle(Number.text);
        ArrearsAmountValue = Convert.ToSingle(ArrearsAmount.text);

        //市值
        float MarketValue = PriceValue * NumberValue;

        //抵押额=市值 x 抵押比例
        MortgageValue = MortgageRatio * MarketValue;
        //保证金比例=1-欠款金额/抵押额
        GuaranteeRatioValue = (1 - ArrearsAmountValue / MortgageValue) * 100f;

        GuaranteeRatio.text = GuaranteeRatioValue.ToString() + "%";

        return GuaranteeRatioValue;
    }

    //--------------------------------------------------------
    /// 计算总市值
    //--------------------------------------------------------
    float GetTotalMarketCapitalization()
    {
        //市值
        float MarketValue = PriceValue * NumberValue;

        TotalMarketCapitalizationValue = MarketValue - ArrearsAmountValue;

        TotalMarketCapitalization.text = TotalMarketCapitalizationValue.ToString();

        return TotalMarketCapitalizationValue;
    }

    void Update()
    {

        GetGuaranteeRatioValue();

        GetTotalMarketCapitalization();

    }

}
