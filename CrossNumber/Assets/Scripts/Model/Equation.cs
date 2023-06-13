using UnityEngine;

public class Equation {

    string Input;

    public float Value { get; private set; }
    public bool CanCalc { get; private set; }

    public Equation()
    {
        CanCalc = false;
    }

    public Equation(string s)
    {
        CanCalc = CanCalcCheck(s);

        if (CanCalc)
            Value = Calculate(s);
    }

    public bool CanCalcCheck(string Input)
    {
        // 수식이 존재하지 않으면 false
        if (Input.Length < 1) return false;

        string str = Input.Substring(0, 1);
        bool isNum = int.TryParse(str, out int i);

        // 첫 문자가 +, -가 아닌 문자일 경우 false
        if (!isNum && str != "+" && str != "-")
            return false;

        //마지막 문자가 숫자면 true, 아니면 false
        str = Input.Substring(Input.Length - 1);
        isNum = int.TryParse(str, out i);

        return isNum;
    }

    float Calculate(string _value)
    {
        string[] _words = _value.Split(' ');

        float[] nums = new float[3];
        string[] ops = new string[2];

        int numsNum = 0;
        int opsNum = 0;
        int wordIdx = 0;
        int length = _words.Length;

        if (_words[wordIdx] == "-")
        {
            wordIdx++;
            nums[numsNum++] = -1;
            ops[opsNum++] = "*";
        }
        else if (_words[wordIdx] == "+")
        {
            wordIdx++;
        }

        while (true)
        {
            if (_words[wordIdx] == "(")
            {
                // 재귀로 실행하면 괄호가 구현될듯 함
            }
            nums[numsNum++] = int.Parse(_words[wordIdx++]);

            if (numsNum == 3)
            {
                if (ops[0] == "^")
                {
                    nums[0] = Calc(nums[0], ops[0], nums[1]);

                    ops[0] = ops[1];
                    nums[1] = nums[2];
                }
                else if (ops[1] != "+" && ops[1] != "-")
                {
                    nums[1] = Calc(nums[1], ops[1], nums[2]);
                }
                else
                {
                    nums[0] = Calc(nums[0], ops[0], nums[1]);

                    ops[0] = ops[1];
                    nums[1] = nums[2];
                }

                numsNum--;
                opsNum--;

            }

            if (length == wordIdx || _words[wordIdx] == ")")
            {
                if (numsNum == 2)
                {
                    nums[0] = Calc(nums[0], ops[0], nums[1]);
                }

                return nums[0];

            }

            ops[opsNum++] = _words[wordIdx++];

        }

    }

    // 숫자와 기호를 넣으면 결과를 출력한다.
    static float Calc(float Num1, string Char, float Num2)
    {
        if (Char == "+")
            return Num1 + Num2;
        else if (Char == "-")
            return Num1 - Num2;
        else if (Char == "*")
            return Num1 * Num2;
        else if (Char == "^")
            return Mathf.Pow(Num1, Num2);
        else
            return Num1 / Num2;

    }

}