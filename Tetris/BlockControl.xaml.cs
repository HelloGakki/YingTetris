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
    /// BlockControl.xaml 的交互逻辑
    /// </summary>
    public partial class BlockControl : UserControl
    {
        public BlockControl()
        {
            InitializeComponent();
        }

        public BlockControl(BlocksShape blocksShape) : this()
        {
            BlocksShape = blocksShape;
        }

        public BlocksShape BlocksShape
        {
            get { return (BlocksShape)GetValue(BlocksShapeProperty); }
            set { SetValue(BlocksShapeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BlocksShape.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlocksShapeProperty =
            DependencyProperty.Register("BlocksShape", typeof(BlocksShape), typeof(BlockControl), new PropertyMetadata(BlocksShape.OType));


    }
}
