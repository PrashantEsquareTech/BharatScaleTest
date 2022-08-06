using System;
using System.Collections;
using System.Collections.Generic;

namespace AIOInventorySystem.Desk
{
    class CurrencyToWord
    {
        private Hashtable htPunctuation;
        private List<DictionaryEntry> listStaticSuffix;
        private List<DictionaryEntry> listStaticPrefix;
        private List<DictionaryEntry> listHelpNotation;

        public CurrencyToWord()
        {
            htPunctuation = new Hashtable();
            listStaticPrefix = new List<DictionaryEntry>();
            listStaticSuffix = new List<DictionaryEntry>();
            listHelpNotation = new List<DictionaryEntry>();
            LoadStaticPrefix();
            LoadStaticSuffix();
        }

        public string ConvertToWord(string Value)
        {
            String ConvertedString = String.Empty;
            if (!(Value.ToString().Length > 40))
            {
                if (IsNumeric(Value.ToString()))
                {
                    try
                    {
                        string strValue = Reverse(Value);
                        switch (strValue.Length)
                        {
                            case 1:
                                if (int.Parse(strValue.ToString()) > 0)
                                    ConvertedString = GetWordConversion(Value);
                                else
                                    ConvertedString = "Zero ";
                                break;
                            case 2:
                                ConvertedString = GetWordConversion(Value);
                                ; break;
                            default:
                                InsertToPunctuationTable(strValue);
                                ReverseHashTable();
                                ConvertedString = ReturnHashtableValue();
                                break;
                        }
                    }
                    catch (Exception Ex)
                    {
                        ConvertedString = "Unexpected Error Occured <br/>";
                        ConvertedString += Ex.Message;
                    }
                }
                else
                    ConvertedString = "Please Enter Numbers Only, Decimal Values Are not supported";
            }
            else
                ConvertedString = "Please Enter Value in Less Then or Equal to 40 Digit";
            return ConvertedString;
        }

        internal bool IsNumeric(string ValueInNumeric)
        {
            bool IsFine = true;
            foreach (char ch in ValueInNumeric)
            {
                if (!(ch >= '0' && ch <= '9'))
                    IsFine = false;
            }
            return IsFine;
        }

        private string ReturnHashtableValue()
        {
            string strFinalString = String.Empty;
            for (int i = htPunctuation.Count; i > 0; i--)
            {
                if (GetWordConversion((htPunctuation[i]).ToString()) != "")
                    strFinalString = strFinalString + GetWordConversion((htPunctuation[i]).ToString()) + StaticPrefixFind((i).ToString());
            }
            return strFinalString;
        }

        private void LoadStaticSuffix()
        {
            listStaticSuffix.Add(new DictionaryEntry(1, "One "));
            listStaticSuffix.Add(new DictionaryEntry(2, "Two "));
            listStaticSuffix.Add(new DictionaryEntry(3, "Three "));
            listStaticSuffix.Add(new DictionaryEntry(4, "Four "));
            listStaticSuffix.Add(new DictionaryEntry(5, "Five "));
            listStaticSuffix.Add(new DictionaryEntry(6, "Six "));
            listStaticSuffix.Add(new DictionaryEntry(7, "Seven "));
            listStaticSuffix.Add(new DictionaryEntry(8, "Eight "));
            listStaticSuffix.Add(new DictionaryEntry(9, "Nine "));
            listStaticSuffix.Add(new DictionaryEntry(10, "Ten "));
            listStaticSuffix.Add(new DictionaryEntry(11, "Eleven "));
            listStaticSuffix.Add(new DictionaryEntry(12, "Twelve "));
            listStaticSuffix.Add(new DictionaryEntry(13, "Thirteen "));
            listStaticSuffix.Add(new DictionaryEntry(14, "Fourteen "));
            listStaticSuffix.Add(new DictionaryEntry(15, "Fifteen "));
            listStaticSuffix.Add(new DictionaryEntry(16, "Sixteen "));
            listStaticSuffix.Add(new DictionaryEntry(17, "Seventeen "));
            listStaticSuffix.Add(new DictionaryEntry(18, "Eighteen "));
            listStaticSuffix.Add(new DictionaryEntry(19, "Nineteen "));
            listStaticSuffix.Add(new DictionaryEntry(20, "Twenty "));
            listStaticSuffix.Add(new DictionaryEntry(30, "Thirty "));
            listStaticSuffix.Add(new DictionaryEntry(40, "Fourty "));
            listStaticSuffix.Add(new DictionaryEntry(50, "Fifty "));
            listStaticSuffix.Add(new DictionaryEntry(60, "Sixty "));
            listStaticSuffix.Add(new DictionaryEntry(70, "Seventy "));
            listStaticSuffix.Add(new DictionaryEntry(80, "Eighty "));
            listStaticSuffix.Add(new DictionaryEntry(90, "Ninty "));
        }

        private void LoadStaticPrefix()
        {
            listStaticPrefix.Add(new DictionaryEntry(2, "Thousand "));
            listStaticPrefix.Add(new DictionaryEntry(3, "Lakh "));
            listStaticPrefix.Add(new DictionaryEntry(4, "Crore "));
            listStaticPrefix.Add(new DictionaryEntry(5, "Arab "));
            listStaticPrefix.Add(new DictionaryEntry(6, "Kharab "));
            listStaticPrefix.Add(new DictionaryEntry(7, "Neel "));
            listStaticPrefix.Add(new DictionaryEntry(8, "Padma "));
            listStaticPrefix.Add(new DictionaryEntry(9, "Shankh "));
            listStaticPrefix.Add(new DictionaryEntry(10, "Maha-shankh "));
            listStaticPrefix.Add(new DictionaryEntry(11, "Ank "));
            listStaticPrefix.Add(new DictionaryEntry(12, "Jald "));
            listStaticPrefix.Add(new DictionaryEntry(13, "Madh "));
            listStaticPrefix.Add(new DictionaryEntry(14, "Paraardha "));
            listStaticPrefix.Add(new DictionaryEntry(15, "Ant "));
            listStaticPrefix.Add(new DictionaryEntry(16, "Maha-ant "));
            listStaticPrefix.Add(new DictionaryEntry(17, "Shisht "));
            listStaticPrefix.Add(new DictionaryEntry(18, "Singhar "));
            listStaticPrefix.Add(new DictionaryEntry(19, "Maha-singhar "));
            listStaticPrefix.Add(new DictionaryEntry(20, "Adant-singhar "));
        }
        
        private void ReverseHashTable()
        {
            Hashtable htTemp = new Hashtable();
            foreach (DictionaryEntry item in htPunctuation)
            {
                htTemp.Add(item.Key, Reverse(item.Value.ToString()));
            }
            htPunctuation.Clear();
            htPunctuation = htTemp;
        }

        private void InsertToPunctuationTable(string strValue)
        {
            htPunctuation.Add(1, strValue.Substring(0, 3).ToString());
            int j = 2;
            for (int i = 3; i < strValue.Length; i = i + 2)
            {
                if (strValue.Substring(i).Length > 0)
                {
                    if (strValue.Substring(i).Length >= 2)
                        htPunctuation.Add(j, strValue.Substring(i, 2).ToString());
                    else
                        htPunctuation.Add(j, strValue.Substring(i, 1).ToString());
                }
                else
                    break;
                j++;
            }
        }

        private string Reverse(string strValue)
        {
            string Reversed = String.Empty;
            foreach (char Ch in strValue)
            {
                Reversed = Ch + Reversed;
            }
            return Reversed;
        }

        private string GetWordConversion(string inputNumber)
        {
            string ToReturnWord = String.Empty;
            if (inputNumber.Length <= 3 && inputNumber.Length > 0)
            {
                if (inputNumber.Length == 3)
                {
                    if (int.Parse(inputNumber.Substring(0, 1)) > 0)
                        ToReturnWord = ToReturnWord + StaticSuffixFind(inputNumber.Substring(0, 1)) + "Hundread ";
                    string TempString = StaticSuffixFind(inputNumber.Substring(1, 2));
                    if (TempString == "")
                    {
                        ToReturnWord = ToReturnWord + StaticSuffixFind(inputNumber.Substring(1, 1) + "0");
                        ToReturnWord = ToReturnWord + StaticSuffixFind(inputNumber.Substring(2, 1));
                    }
                    ToReturnWord = ToReturnWord + TempString;
                }
                if (inputNumber.Length == 2)
                {
                    string TempString = StaticSuffixFind(inputNumber.Substring(0, 2));
                    if (TempString == "")
                    {
                        ToReturnWord = ToReturnWord + StaticSuffixFind(inputNumber.Substring(0, 1) + "0");
                        ToReturnWord = ToReturnWord + StaticSuffixFind(inputNumber.Substring(1, 1));
                    }
                    ToReturnWord = ToReturnWord + TempString;
                }
                if (inputNumber.Length == 1)
                    ToReturnWord = ToReturnWord + StaticSuffixFind(inputNumber.Substring(0, 1));
            }
            return ToReturnWord;
        }

        internal string StaticSuffixFind(string NumberKey)
        {
            string ValueFromNumber = String.Empty;
            foreach (DictionaryEntry Pair in listStaticSuffix)
            {
                if (Pair.Key.ToString().Trim() == NumberKey.Trim())
                    ValueFromNumber = Pair.Value.ToString();
            }
            return ValueFromNumber;
        }

        private string StaticPrefixFind(string NumberKey)
        {
            string ValueFromNumber = String.Empty;
            foreach (DictionaryEntry Pair in listStaticPrefix)
            {
                if (Pair.Key.ToString().Trim() == NumberKey.Trim())
                    ValueFromNumber = Pair.Value.ToString();
            }
            return ValueFromNumber;
        }

        private string StaticHelpNotationFind(string NumberKey)
        {
            string HelpText = String.Empty;
            foreach (DictionaryEntry Pair in listHelpNotation)
            {
                if (Pair.Key.ToString().Trim() == NumberKey.Trim())
                    HelpText = Pair.Value.ToString();
            }
            return HelpText;
        }
    }
}