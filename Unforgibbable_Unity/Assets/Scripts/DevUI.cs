using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DevUI : MonoBehaviour
{
    public GameObject UIPanel;
    
    public Slider m_minVel;
    public TextMeshProUGUI minvel;
    
    public Slider m_maxVel;
    public TextMeshProUGUI maxvel;
    
    public Slider m_maxRot;
    public TextMeshProUGUI maxrot;
    
    public Slider m_ImpactDir;
    public TextMeshProUGUI impact;


    internal static Gibber _gibber;

    private void Awake()
    {
        _gibber = ZNetScene.instance.GetPrefab("deer_gibs").GetComponent<Gibber>();
        m_minVel.onValueChanged.AddListener(delegate(float num) { OnMinVelChang(num); });
        m_maxVel.onValueChanged.AddListener(delegate(float num) { OnMaxVelChang(num); });
        m_maxRot.onValueChanged.AddListener(delegate(float num) { OnMaxRotChang(num); });
        m_ImpactDir.onValueChanged.AddListener(delegate(float num) {  OnImpactDirChang(num);});
        minvel.SetText(_gibber.m_minVel.ToString());
        maxvel.SetText(_gibber.m_maxVel.ToString());
        maxrot.SetText(_gibber.m_maxRotVel.ToString());
        impact.SetText(_gibber.m_impactDirectionMix.ToString());
        UIPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            UIPanel.SetActive(!UIPanel.activeSelf);
        }
        if (UIPanel.activeSelf)
        {
            StoreGui.instance.m_hiddenFrames = 0;
        }
    }

    private void OnMinVelChang(float num)
    {
        minvel.SetText(num.ToString());
        _gibber.m_minVel = num;
    }

    private void OnMaxVelChang(float num)
    {
        maxvel.SetText(num.ToString());
        _gibber.m_maxVel = num;
    }

    private void OnMaxRotChang(float num)
    {
        maxrot.SetText(num.ToString());
        _gibber.m_maxRotVel = num;
    }

    private void OnImpactDirChang(float num)
    {
        impact.SetText(num.ToString());
        _gibber.m_impactDirectionMix = num;
    }
}
