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
    //股价
    public Text Price = null;
    private float PriceValue = 0;

    //股数
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
    //该股票抵押比率(风控使用)
    const float STOCK_MORTGAGE_RATIO = 0.55f;
    //汇率，RMB兑港币
    const float ExchangeRate_HKD_To_RMB = 0.8f;

    [Header("过夜风控比率")]
    public Text OvernightRiskControlRatio = null;
    public float OvernightRiskControlRatioValue = 0;

    [Header("保证金要求")]
    public Text GuaranteeRequire = null;
    public float GuaranteeRequireValue = 0;

    [Header("持有该股票市值")]
    public Text StockMarketCapitalization = null;
    public float StockMarketCapitalizationValue = 0;

    [Header("总市值")]
    public Text TotalMarketCapitalization = null;
    public float TotalMarketCapitalizationValue = 0;

    [Header("RMB总市值")]
    public Text TotalRMBMarketCapitalization = null;
    public float TotalRMBMarketCapitalizationValue = 0;


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

    //--------------------------------------------------------
    /// 计算RMB总市值
    //--------------------------------------------------------
    float GetTotalRMBMarketCapitalization()
    {
        TotalRMBMarketCapitalizationValue = TotalMarketCapitalizationValue * ExchangeRate_HKD_To_RMB;

        TotalRMBMarketCapitalization.text = TotalRMBMarketCapitalizationValue.ToString();

        return TotalMarketCapitalizationValue;
    }

    //--------------------------------------------------------
    /// 计算持有该股票市值
    //--------------------------------------------------------
    float GetStockMarketCapitalization()
    {
        //持有该股票市值
        StockMarketCapitalizationValue = PriceValue * NumberValue;

        StockMarketCapitalization.text = StockMarketCapitalizationValue.ToString();

        return StockMarketCapitalizationValue;
    }



    //--------------------------------------------------------
    /// 计算保证金要求
    //--------------------------------------------------------
    float GetGuaranteeRequire()
    {
        //保证金要求=持有股票市值 * 该股票抵押比率
        GuaranteeRequireValue = StockMarketCapitalizationValue * STOCK_MORTGAGE_RATIO;

        GuaranteeRequire.text = GuaranteeRequireValue.ToString();

        return GuaranteeRequireValue;
    }

    //--------------------------------------------------------
    /// 计算过夜风控比率
    //--------------------------------------------------------
    float GetOvernightRiskControlRatio()
    {

        //过夜风控比率=1-保证金要求/资产总市值
        OvernightRiskControlRatioValue = (1 - GuaranteeRequireValue / TotalMarketCapitalizationValue) * 100;

        OvernightRiskControlRatio.text = OvernightRiskControlRatioValue.ToString() + "%";

        return OvernightRiskControlRatioValue;
    }



    void Update()
    {
        //计算持有该股票市值
        GetStockMarketCapitalization();

        //计算保证金要求
        GetGuaranteeRequire();

        //计算过夜风控比率
        GetOvernightRiskControlRatio();

        //计算保证金比率
        GetGuaranteeRatioValue();

        //计算总市值
        GetTotalMarketCapitalization();

        //计算RMB总市值
        GetTotalRMBMarketCapitalization();
    }

}
