// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Client.Validators.LuhnFormulaValidator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Text;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Client.Validators
{
  public class LuhnFormulaValidator : CustomValidator
  {
    protected override bool OnServerValidate(string value)
    {
      if (string.IsNullOrEmpty(value))
        return false;
      StringBuilder stringBuilder = new StringBuilder();
      for (int startIndex = 0; startIndex < value.Length; ++startIndex)
      {
        if ("0123456789".IndexOf(value.Substring(startIndex, 1)) >= 0)
          stringBuilder.Append(value.Substring(startIndex, 1));
      }
      if (stringBuilder.Length < 13 || stringBuilder.Length > 16)
        return false;
      for (int index = stringBuilder.Length + 1; index <= 16; ++index)
        stringBuilder.Insert(0, "0");
      int num1 = 0;
      string str = stringBuilder.ToString();
      for (int index = 1; index <= 16; ++index)
      {
        int num2 = 1 + index % 2;
        int num3 = int.Parse(str.Substring(index - 1, 1)) * num2;
        if (num3 > 9)
          num3 -= 9;
        num1 += num3;
      }
      return num1 % 10 == 0;
    }
  }
}
