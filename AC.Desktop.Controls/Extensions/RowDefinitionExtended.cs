using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AC.Desktop.Controls.Extensions
{
    public class RowDefinitionExtended : RowDefinition
    {
        public static DependencyProperty VisibleProperty;

        public Boolean Visible
        {
            get { return (Boolean)GetValue(VisibleProperty); }
            set { SetValue(VisibleProperty, value); }
        }

        static RowDefinitionExtended()
        {
            VisibleProperty = DependencyProperty.Register("Visible",
                typeof(Boolean),
                typeof(RowDefinitionExtended),
                new PropertyMetadata(true, new PropertyChangedCallback(OnVisibleChanged)));

            HeightProperty.OverrideMetadata(typeof(RowDefinitionExtended),
                new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), null,
                    new CoerceValueCallback(CoerceHeight)));

            MinHeightProperty.OverrideMetadata(typeof(RowDefinitionExtended),
                new FrameworkPropertyMetadata((Double)0, null,
                    new CoerceValueCallback(CoerceMinHeigth)));
        }

        public static void SetVisible(DependencyObject obj, Boolean nVisible)
        {
            obj.SetValue(VisibleProperty, nVisible);
        }
        public static Boolean GetVisible(DependencyObject obj)
        {
            return (Boolean)obj.GetValue(VisibleProperty);
        }

        static void OnVisibleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            obj.CoerceValue(HeightProperty);
            obj.CoerceValue(MinHeightProperty);
        }
        static Object CoerceHeight(DependencyObject obj, Object nValue)
        {
            return (((RowDefinitionExtended)obj).Visible) ? nValue : new GridLength(0);
        }
        static Object CoerceMinHeigth(DependencyObject obj, Object nValue)
        {
            return (((RowDefinitionExtended)obj).Visible) ? nValue : (Double)0;
        }
    }
}
