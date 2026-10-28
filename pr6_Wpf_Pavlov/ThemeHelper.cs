using System;
using System.Linq;
using System.Windows;

namespace pr6_Wpf_Pavlov
{
    public static class ThemeHelper
    {
      
        private static readonly string[] _themePath =
        {
            "Styles/Colors/DefaultColors.xaml",
            "Styles/Colors/DarkTheme.xaml"
        };

        public static string Current
        {
            get => string.IsNullOrEmpty(Properties.Settings.Default.ThemePath)
                ? _themePath[0]
                : Properties.Settings.Default.ThemePath;
            set
            {
                Properties.Settings.Default.ThemePath = value;
                Properties.Settings.Default.Save();
            }
        }

     
        public static void Apply(string themePath)
        {
            var newTheme = new ResourceDictionary
            {
                Source = new Uri(themePath, UriKind.Relative)
            };

            var oldTheme = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null &&
                    _themePath.Any(p => d.Source.OriginalString.EndsWith(p, StringComparison.OrdinalIgnoreCase)));

            if (oldTheme != null)
            {
                int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldTheme);
                Application.Current.Resources.MergedDictionaries[index] = newTheme;
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(newTheme);
            }

            Current = themePath;
        }


        public static void ApplySaved()
        {
            var theme = Current;
            Apply(theme);
        }


        public static void Toggle()
        {
            var newTheme = Current == _themePath[0] ? _themePath[1] : _themePath[0];
            Apply(newTheme);
        }
    }
}