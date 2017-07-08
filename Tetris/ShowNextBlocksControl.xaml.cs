using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetris
{
    /// <summary>
    /// ShowNextBlocksControl.xaml 的交互逻辑
    /// </summary>
    public partial class ShowNextBlocksControl : UserControl
    {
        public ShowNextBlocksControl()
        {
            InitializeComponent();
        }



        public BlocksControl NextBlocks
        {
            get { return (BlocksControl)GetValue(NextBlocksProperty); }
            set { SetValue(NextBlocksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NextBlocks.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextBlocksProperty =
            DependencyProperty.Register("NextBlocks", typeof(BlocksControl), typeof(ShowNextBlocksControl), new PropertyMetadata(null, new PropertyChangedCallback(OnNextBlocksChange)));

        private static void OnNextBlocksChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
            var control = d as ShowNextBlocksControl;
            control.showBlocksGrid.Children.Clear();
            if (e.NewValue == null)
                return;
            control.showBlocksGrid.Children.Add((BlocksControl)e.NewValue);
        }
    }
}
