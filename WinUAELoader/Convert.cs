// Copyright (c) 2008, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class Convert
{
    private static Color[] ColorArray = new Color[]
    {
            Color.AliceBlue,
			Color.AntiqueWhite,
			Color.Aqua,
			Color.Aquamarine,
			Color.Azure,
			Color.Beige,
			Color.Bisque,
			Color.Black,
			Color.BlanchedAlmond,
			Color.Blue,
			Color.BlueViolet,
			Color.Brown,
			Color.BurlyWood,
			Color.CadetBlue,
			Color.Chartreuse,
			Color.Chocolate,
			Color.Coral,
			Color.CornflowerBlue,
			Color.Cornsilk,
			Color.Crimson,
			Color.Cyan,
			Color.DarkBlue,
			Color.DarkCyan,
			Color.DarkGoldenrod,
			Color.DarkGray,
			Color.DarkGreen,
			Color.DarkKhaki,
			Color.DarkMagenta,
			Color.DarkOliveGreen,
			Color.DarkOrange,
			Color.DarkOrchid,
			Color.DarkRed,
			Color.DarkSalmon,
			Color.DarkSeaGreen,
			Color.DarkSlateBlue,
			Color.DarkSlateGray,
			Color.DarkTurquoise,
			Color.DarkViolet,
			Color.DeepPink,
			Color.DeepSkyBlue,
			Color.DimGray,
			Color.DodgerBlue,
			Color.Firebrick,
			Color.FloralWhite,
			Color.ForestGreen,
			Color.Fuchsia,
			Color.Gainsboro,
			Color.GhostWhite,
			Color.Gold,
			Color.Goldenrod,
			Color.Gray,
			Color.Green,
			Color.GreenYellow,
			Color.Honeydew,
			Color.HotPink,
			Color.IndianRed,
			Color.Indigo,
			Color.Ivory,
			Color.Khaki,
			Color.Lavender,
			Color.LavenderBlush,
			Color.LawnGreen,
			Color.LemonChiffon,
			Color.LightBlue,
			Color.LightCoral,
			Color.LightCyan,
			Color.LightGoldenrodYellow,
			Color.LightGray,
			Color.LightGreen,
			Color.LightPink,
			Color.LightSalmon,
			Color.LightSeaGreen,
			Color.LightSkyBlue,
			Color.LightSlateGray,
			Color.LightSteelBlue,
			Color.LightYellow,
			Color.Lime,
			Color.LimeGreen,
			Color.Linen,
			Color.Magenta,
			Color.Maroon,
			Color.MediumAquamarine,
			Color.MediumBlue,
			Color.MediumOrchid,
			Color.MediumPurple,
			Color.MediumSeaGreen,
			Color.MediumSlateBlue,
			Color.MediumSpringGreen,
			Color.MediumTurquoise,
			Color.MediumVioletRed,
			Color.MidnightBlue,
			Color.MintCream,
			Color.MistyRose,
			Color.Moccasin,
			Color.NavajoWhite,
			Color.Navy,
			Color.OldLace,
			Color.Olive,
			Color.OliveDrab,
			Color.Orange,
			Color.OrangeRed,
			Color.Orchid,
			Color.PaleGoldenrod,
			Color.PaleGreen,
			Color.PaleTurquoise,
			Color.PaleVioletRed,
			Color.PapayaWhip,
			Color.PeachPuff,
			Color.Peru,
			Color.Pink,
			Color.Plum,
			Color.PowderBlue,
			Color.Purple,
			Color.Red,
			Color.RosyBrown,
			Color.RoyalBlue,
			Color.SaddleBrown,
			Color.Salmon,
			Color.SandyBrown,
			Color.SeaGreen,
			Color.SeaShell,
			Color.Sienna,
			Color.Silver,
			Color.SkyBlue,
			Color.SlateBlue,
			Color.SlateGray,
			Color.Snow,
			Color.SpringGreen,
			Color.SteelBlue,
			Color.Tan,
			Color.Teal,
			Color.Thistle,
			Color.Tomato,
			Color.Transparent,
			Color.Turquoise,
			Color.Violet,
			Color.Wheat,
			Color.White,
			Color.WhiteSmoke,
			Color.Yellow,
			Color.YellowGreen
			};

    public static string SubString(string str, int length)
    {
        if (str.Length < length)
            return str;
        else
            return str.Substring(0, length);
    }

	public static byte[] StrToByteArray(string str)
	{
		return System.Text.Encoding.Default.GetBytes(str);
	}

	public static string ByteArrayToStr(byte[] bytes)
	{
		return System.Text.Encoding.Default.GetString(bytes);
	}

	public static string ByteToStr(byte b)
	{
		return System.Text.Encoding.Default.GetString(new byte[] { b });
	}

	public static bool IsFloat(string val)
	{
		if (Regex.IsMatch( val.ToString(), @"^[+-]?([0-9]*\.?[0-9]+|[0-9]+\.?[0-9]*)([eE][+-]?[0-9]+)?$" ))
			return true;
		else return false;
	}

	public static int StrToInt(string val)
	{
		int result = 0;

		if (Int32.TryParse(val, out result))
			return result;

		return result;
	}

	public static Single StrToSingle(string val)
	{
        float result = 0;

        if (Single.TryParse(val, out result))
            return result;
		return 0;
	}

	public static bool StrToBool(string val)
	{
        if (String.IsNullOrEmpty(val))
            return false;

        if (val.Trim().ToLower() == "true" || val.Trim() == "1")
			return true;
		else
			return false;
	}

    public static string ColorToAlphaStr(Color color)
    {
        return color.A.ToString();
    }

    public static string ColorToARGBStr(Color color)
    {
        return color.A.ToString() + "," + color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString();
    }

    public static string ColorToRGBStr(Color color)
    {
        return color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString();
    }

    public static string ColorToStr(Color color)
    {
        if (color.IsKnownColor)
            return color.ToString();

        else return ColorToRGBStr(color);
    }

    public static Color StrToColor(string color, Color defaultColor)
    {
        if (color == null)
            return defaultColor;

        if (color.IndexOf(',') != -1)
        {
            string[] colorArray = color.Split(',');
            if (colorArray.Length == 3)
                return Color.FromArgb(StrToInt(colorArray[0]), StrToInt(colorArray[1]), StrToInt(colorArray[2]));
            if (colorArray.Length == 4)
                return Color.FromArgb(StrToInt(colorArray[0]), StrToInt(colorArray[1]), StrToInt(colorArray[2]), StrToInt(colorArray[3]));
        }
        for (int i = 0; i < ColorArray.Length; i++)
            if (color.ToLower() == ColorArray[i].Name.ToLower())
                return ColorArray[i];

        return defaultColor;
    }

    public static int[] StrArrayToIntArray(string val, char delimiter)
    {
        int[] valReturn = null;

        if (val == null)
            return valReturn;

        if (val.IndexOf(delimiter) != -1)
        {
            string[] valArray = val.Split(delimiter);
            valReturn = new int[valArray.Length];
            for (int i = 0; i < valArray.Length; i++)
                valReturn[i] = StrToInt(valArray[i]);

            return valReturn;
        }

        valReturn = new int[1];
        valReturn[0] = StrToInt(val);

        return valReturn;
    }

    public static RectangleF StrToRectangleF(string x, string y, string width, string height)
    {
        return new RectangleF(StrToInt(x), StrToInt(y), StrToInt(width), StrToInt(height));
    }

    public static Rectangle StrArrayToRectangle(string val, char delimiter)
    {
        Rectangle valReturn = Rectangle.Empty;

        if (val == null)
            return valReturn;

        int[] valArray = StrArrayToIntArray(val, delimiter);
        
        if(valArray.Length == 4)
            valReturn = new Rectangle(valArray[0], valArray[1], valArray[2], valArray[3]);

        return valReturn;
    }

    public static string RectangleToStrArray(Rectangle rect, char delimiter)
    {
        return rect.X.ToString() + delimiter + rect.Y.ToString() + delimiter + rect.Width.ToString() + delimiter + rect.Height.ToString();
    }

    public static string BoolToStr(bool val)
	{
		if(val)
			return "True";
		else
			return "False";
	}

    public static bool IntToBool(int val)
    {
        if (val == 0)
            return false;
        else
            return true;
    }

    public static bool IsAlpha(string str)
    {
        Regex regex = new Regex("[^a-zA-Z]");

        return !regex.IsMatch(str);
    }

    public static bool IsAlphaNumeric(string str)
    {
        Regex regex = new Regex("[^a-zA-Z0-9]");

        return !regex.IsMatch(str);
    }

    public static string RemoveBrackets(string str)
    {
        str = Regex.Replace(str, @"\[.+?\]", "");
        str = Regex.Replace(str, @"\(.+?\)", "");

        return str.Trim();
    }

    public static string ReplaceFirst(string str, string oldValue, string newValue)
    {
        string[] strSplit = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < strSplit.Length; i++)
        {
            if (strSplit[i] == oldValue)
            {
                strSplit[i] = newValue;
                return String.Join("", strSplit);
            }
        }

        return str;
    }

    public static string[] SplitString(string str, string[] splitStr)
    {
        int strPos = 0;
        List<string> strArray = new List<string>();

        string[] strSplit = str.Split(splitStr, StringSplitOptions.None);

        for (int i = 0; i < strSplit.Length; i++)
        {
            if (strSplit[i] != String.Empty)
                strArray.Add(strSplit[i]);

            strPos += strSplit[i].Length;

            for (int j = 0; j < splitStr.Length; j++)
            {
                if (str.IndexOf(splitStr[j], strPos) == strPos)
                {
                    if (splitStr[j] != String.Empty)
                        strArray.Add(splitStr[j]);

                    strPos += splitStr[j].Length;

                    break;
                }
            }
        }

        return strArray.ToArray();
    }

    public static string GetDateYear(string str)
    {
        if (String.IsNullOrEmpty(str))
            return "NULL";

        return "# " + DateTime.Parse("1/1/" + str).ToShortDateString() + " #";
    }

    public static string StrToDBStr(string str)
    {
        if (String.IsNullOrEmpty(str))
            return "NULL";

        str = str.Replace("'", "''");
        str = "'" + str + "'";

        return str;
    }

    public static string StrArrayToStr(string[] str)
    {
        string retVal = null;

        if (str == null)
            return retVal;

        for (int i = 0; i < str.Length; i++)
            retVal += str[i] + (i < str.Length - 1 ? ", " : null);

        return retVal;
    }

    public static string StrRemoveLastInstanceOf(string str, char c)
    {
        int charPos = str.LastIndexOf('-');

        return str.Substring(0, charPos) + str.Substring(charPos + 1, str.Length - charPos - 1);
    }
}

