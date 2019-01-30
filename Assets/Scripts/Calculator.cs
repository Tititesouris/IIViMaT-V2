using UnityEngine;

public class Calculator : MonoBehaviour
{
    public float value;

    public bool Add(float value)
    {
        this.value += value;
        return true;
    }

    public bool Subtract(float value)
    {
        this.value -= value;
        return true;
    }

    public bool Multiply(float value)
    {
        this.value *= value;
        return true;
    }

    public bool Divide(float value)
    {
        if (Equals(this.value, value))
            return false;
        this.value /= value;
        return true;
    }
}