﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace POP_SF_53_2016_GUI.UI
{
    public class ValidationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            var sb = new StringBuilder();
            var errors = value as ReadOnlyCollection<ValidationError>;
            if (errors != null)
            {
                foreach (var e in errors.Where(e => e.ErrorContent != null))
                { sb.AppendLine(e.ErrorContent.ToString()); }
            }

            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        { throw new NotImplementedException(); }
    }
}