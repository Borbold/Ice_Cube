using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gate : MonoBehaviour
{
    [SerializeField] [Min(1)] private float _value;
    [SerializeField] private Type _type;

   public enum Type
   {
        multiplication,
        Addition,
        Substraction
   }
    public float Value => _value;

   public string TypeName
    {
        get
        {
            if (_type == Type.multiplication)
                return "X";
            if (_type == Type.Addition)
                return "+";
            if (_type == Type.Substraction)
                return "-";
            else
                return "";
        }
    }

   public void Deactivate()
    {
        GetComponent<Collider>().enabled = false;
    }

   public int CalculateAdditionAmount(int value)
   {
        if (_type == Type.multiplication)
            return (int)(value * _value) - value;
        if (_type == Type.Addition)
            return (int)_value;
        if (_type == Type.Substraction)
            return -(int)_value;
        else
            return value;
   }
}
