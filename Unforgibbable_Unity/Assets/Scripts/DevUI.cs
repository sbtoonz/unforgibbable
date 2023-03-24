using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

    public TMP_Dropdown Dropdown;
    
    internal static Gibber current_gibber;

    [SerializeField]internal List<Gibber> _gibbers = new List<Gibber>();
    private List<string> gibnames = new List<string>();

    private void Awake()
    {
        _gibbers.Add(ZNetScene.instance.GetPrefab("deer_gibs").GetComponent<Gibber>());
        _gibbers.Add(ZNetScene.instance.GetPrefab("boar_gibs").GetComponent<Gibber>());
        current_gibber = _gibbers[0];
        m_minVel.onValueChanged.AddListener(delegate(float num) { OnMinVelChang(num); });
        m_maxVel.onValueChanged.AddListener(delegate(float num) { OnMaxVelChang(num); });
        m_maxRot.onValueChanged.AddListener(delegate(float num) { OnMaxRotChang(num); });
        m_ImpactDir.onValueChanged.AddListener(delegate(float num) {  OnImpactDirChang(num);});
        minvel.SetText(current_gibber.m_minVel.ToString());
        maxvel.SetText(current_gibber.m_maxVel.ToString());
        maxrot.SetText(current_gibber.m_maxRotVel.ToString());
        impact.SetText(current_gibber.m_impactDirectionMix.ToString());
        foreach (var gibber in _gibbers)
        {
            gibnames.Add(gibber.name);
        }
        Dropdown.AddOptions(gibnames);
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

    public void OnDropDownChanged(int i)
    {
        current_gibber = _gibbers[i];
        Dropdown.captionText.SetText(current_gibber.name);
    }

    public void OnMinVelChang(float num)
    {
        minvel.SetText(num.ToString());
        current_gibber.m_minVel = num;
    }
    
    public void OnMinVelChang(string num)
    {
        minvel.SetText(num.ToString());
        int.TryParse(num, out var test);
        current_gibber.m_minVel = test;
    }

    public void OnMaxVelChang(float num)
    {
        maxvel.SetText(num.ToString());
        current_gibber.m_maxVel = num;
    }
    public void OnMaxVelChang(string num)
    {
        maxvel.SetText(num.ToString());
        int.TryParse(num, out int test);
        current_gibber.m_maxVel = test;
    }

    public void OnMaxRotChang(float num)
    {
        maxrot.SetText(num.ToString());
        current_gibber.m_maxRotVel = num;
    }
    public void OnMaxRotChang(string num)
    {
        maxrot.SetText(num.ToString());
        int.TryParse(num, out int test);
        current_gibber.m_maxRotVel = test;
    }

    public void OnImpactDirChang(float num)
    {
        impact.SetText(num.ToString());
        current_gibber.m_impactDirectionMix = num;
    }
    public void OnImpactDirChang(string num)
    {
        impact.SetText(num.ToString());
        int.TryParse(num, out int test);
        current_gibber.m_impactDirectionMix = test;
    }

}
