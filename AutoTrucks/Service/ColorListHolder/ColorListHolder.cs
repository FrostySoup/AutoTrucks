using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace Service.ColorListHolder
{
    public class ColorListHolder : IColorListHolder
    {
        private ObservableCollection<ColorItem> ColorList;
        public ColorListHolder()
        {
            ColorList = new ObservableCollection<ColorItem>();
            ColorList.Add(new ColorItem(Colors.AliceBlue, "AliceBlue"));
            ColorList.Add(new ColorItem(Colors.AntiqueWhite, "AntiqueWhite"));
            ColorList.Add(new ColorItem(Colors.Beige, "Beige"));
            ColorList.Add(new ColorItem(Colors.BlanchedAlmond, "BlanchedAlmond"));
            ColorList.Add(new ColorItem(Colors.Cyan, "Cyan"));
            ColorList.Add(new ColorItem(Colors.FloralWhite, "FloralWhite"));
            ColorList.Add(new ColorItem(Colors.Gold, "Gold"));
            ColorList.Add(new ColorItem(Colors.GreenYellow, "GreenYellow"));
            ColorList.Add(new ColorItem(Colors.Honeydew, "Honeydew"));
            ColorList.Add(new ColorItem(Colors.Lavender, "Lavender"));
            ColorList.Add(new ColorItem(Colors.LavenderBlush, "LavenderBlush"));
            ColorList.Add(new ColorItem(Colors.LightGreen, "LightGreen"));
            ColorList.Add(new ColorItem(Colors.LightPink, "LightPink"));
            ColorList.Add(new ColorItem(Colors.Yellow, "Yellow"));
            ColorList.Add(new ColorItem(Colors.MediumSpringGreen, "MediumSpringGreen"));
            ColorList.Add(new ColorItem(Colors.LightSteelBlue, "LightSteelBlue"));
            ColorList.Add(new ColorItem(Colors.Violet, "Violet"));
            ColorList.Add(new ColorItem(Colors.PowderBlue, "PowderBlue"));
            ColorList.Add(new ColorItem(Colors.DeepSkyBlue, "DeepSkyBlue"));
            ColorList.Add(new ColorItem(Colors.Salmon, "Salmon"));
        }

        public ColorItem GetColorByReferenceId(string referenceId)
        {          
            if (referenceId != null && referenceId.Length >= 7)
            {
                string id = "" + referenceId[5] + referenceId[6];
                int results;
                if (Int32.TryParse(id, out results))
                {
                    if (results < ColorList.Count)
                        return ColorList.ElementAt(results);
                }
            }
            return new ColorItem(Colors.White, "White");
        }

        public string SetReferenceByColor(Color color)
        {
            var colorItem = ColorList.FirstOrDefault(x => x.Color == color);
            int index = ColorList.IndexOf(colorItem);
            if (index < 10)
                return GenerateCode() + "0" + index;
            return GenerateCode() + index.ToString();
        }

        private string GenerateCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[5];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        public ObservableCollection<ColorItem> GetColors()
        {
            return ColorList;
        }
    }
}
