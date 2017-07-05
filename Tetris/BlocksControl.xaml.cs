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
    /// BlocksControl.xaml 的交互逻辑
    /// </summary>
    public partial class BlocksControl : UserControl
    {
        public static readonly int[,,,] blockArray = {
                                                    { { { 1, 1, 0, 0 },     ///OType
                                                        { 1, 1, 0, 0 },     ///OType
                                                        { 0, 0, 0, 0 },     ///OType
                                                        { 0, 0, 0, 0 } },   ///OType
                                                      { { 1, 1, 0, 0 },     ///OType
                                                        { 1, 1, 0, 0 },     ///OType
                                                        { 0, 0, 0, 0 },     ///OType
                                                        { 0, 0, 0, 0 } },   ///OType
                                                      { { 1, 1, 0, 0 },     ///OType
                                                        { 1, 1, 0, 0 },     ///OType
                                                        { 0, 0, 0, 0 },     ///OType
                                                        { 0, 0, 0, 0 } },   ///OType
                                                      { { 1, 1, 0, 0 },     ///OType
                                                        { 1, 1, 0, 0 },     ///OType
                                                        { 0, 0, 0, 0 },     ///OType
                                                        { 0, 0, 0, 0 } } }, ///OType
                                                    { { { 1, 1, 1, 1 },     ///IType
                                                        { 0, 0, 0, 0 },     ///IType
                                                        { 0, 0, 0, 0 },     ///IType
                                                        { 0, 0, 0, 0 } },   ///IType
                                                      { { 1, 0, 0, 0 },     ///IType
                                                        { 1, 0, 0, 0 },     ///IType
                                                        { 1, 0, 0, 0 },     ///IType
                                                        { 1, 0, 0, 0 } },   ///IType
                                                      { { 1, 1, 1, 1 },     ///IType
                                                        { 0, 0, 0, 0 },     ///IType
                                                        { 0, 0, 0, 0 },     ///IType
                                                        { 0, 0, 0, 0 } },   ///IType
                                                      { { 1, 0, 0, 0 },     ///IType
                                                        { 1, 0, 0, 0 },     ///IType
                                                        { 1, 0, 0, 0 },     ///IType
                                                        { 1, 0, 0, 0 } } }, ///IType
                                                    { { { 1, 0, 0, 0 },     ///LType
                                                        { 1, 0, 0, 0 },     ///LType
                                                        { 1, 1, 0, 0 },     ///LType
                                                        { 0, 0, 0, 0 } },   ///LType
                                                      { { 1, 1, 1, 0 },     ///LType
                                                        { 1, 0, 0, 0 },     ///LType
                                                        { 0, 0, 0, 0 },     ///LType
                                                        { 0, 0, 0, 0 } },   ///LType
                                                      { { 1, 1, 0, 0 },     ///LType
                                                        { 0, 1, 0, 0 },     ///LType
                                                        { 0, 1, 0, 0 },     ///LType
                                                        { 0, 0, 0, 0 } },   ///LType
                                                      { { 0, 0, 1, 0 },     ///LType
                                                        { 1, 1, 1, 0 },     ///LType
                                                        { 0, 0, 0, 0 },     ///LType
                                                        { 0, 0, 0, 0 } } }, ///LType
                                                    { { { 0, 1, 0, 0 },     ///JType
                                                        { 0, 1, 0, 0 },     ///JType
                                                        { 1, 1, 0, 0 },     ///JType
                                                        { 0, 0, 0, 0 } },   ///JType
                                                      { { 1, 0, 0, 0 },     ///JType
                                                        { 1, 1, 1, 0 },     ///JType
                                                        { 0, 0, 0, 0 },     ///JType
                                                        { 0, 0, 0, 0 } },   ///JType
                                                      { { 1, 1, 0, 0 },     ///JType
                                                        { 1, 0, 0, 0 },     ///JType
                                                        { 1, 0, 0, 0 },     ///JType
                                                        { 0, 0, 0, 0 } },   ///JType
                                                      { { 1, 1, 1, 0 },     ///JType
                                                        { 0, 0, 1, 0 },     ///JType
                                                        { 0, 0, 0, 0 },     ///JType
                                                        { 0, 0, 0, 0 } } }, ///JType
                                                    { { { 0, 1, 1, 0 },     ///SType
                                                        { 1, 1, 0, 0 },     ///SType
                                                        { 0, 0, 0, 0 },     ///SType
                                                        { 0, 0, 0, 0 } },   ///SType
                                                      { { 1, 0, 0, 0 },     ///SType
                                                        { 1, 1, 0, 0 },     ///SType
                                                        { 0, 1, 0, 0 },     ///SType
                                                        { 0, 0, 0, 0 } },   ///SType
                                                      { { 0, 1, 1, 0 },     ///SType
                                                        { 1, 1, 0, 0 },     ///SType
                                                        { 0, 0, 0, 0 },     ///SType
                                                        { 0, 0, 0, 0 } },   ///SType
                                                      { { 1, 0, 0, 0 },     ///SType
                                                        { 1, 1, 0, 0 },     ///SType
                                                        { 0, 1, 0, 0 },     ///SType
                                                        { 0, 0, 0, 0 } } }, ///SType
                                                    { { { 1, 1, 0, 0 },     ///ZType
                                                        { 0, 1, 1, 0 },     ///ZType
                                                        { 0, 0, 0, 0 },     ///ZType
                                                        { 0, 0, 0, 0 } },   ///ZType
                                                      { { 0, 1, 0, 0 },     ///ZType
                                                        { 1, 1, 0, 0 },     ///ZType
                                                        { 1, 0, 0, 0 },     ///ZType
                                                        { 0, 0, 0, 0 } },   ///ZType
                                                      { { 1, 1, 0, 0 },     ///ZType
                                                        { 0, 1, 1, 0 },     ///ZType
                                                        { 0, 0, 0, 0 },     ///ZType
                                                        { 0, 0, 0, 0 } },   ///ZType
                                                      { { 0, 1, 0, 0 },     ///ZType
                                                        { 1, 1, 0, 0 },     ///ZType
                                                        { 1, 0, 0, 0 },     ///ZType
                                                        { 0, 0, 0, 0 } } }, ///ZType
                                                    { { { 0, 1, 0, 0 },     ///TType
                                                        { 1, 1, 1, 0 },     ///TType
                                                        { 0, 0, 0, 0 },     ///TType
                                                        { 0, 0, 0, 0 } },   ///TType
                                                      { { 1, 0, 0, 0 },     ///TType
                                                        { 1, 1, 0, 0 },     ///TType
                                                        { 1, 0, 0, 0 },     ///TType
                                                        { 0, 0, 0, 0 } },   ///TType
                                                      { { 1, 1, 1, 0 },     ///TType
                                                        { 0, 1, 0, 0 },     ///TType
                                                        { 0, 0, 0, 0 },     ///TType
                                                        { 0, 0, 0, 0 } },   ///TType
                                                      { { 0, 1, 0, 0 },     ///TType
                                                        { 1, 1, 0, 0 },     ///TType
                                                        { 0, 1, 0, 0 },     ///TType
                                                        { 0, 0, 0, 0 } } }  ///TType
                                                 };
        private BlocksControl()
        {
            InitializeComponent();
            
        }

        public BlocksControl(BlocksShape blocksShape, BlocksStatus blockStatus) : this()
        {
            BlocksShape = blocksShape;
            BlocksStatus = blockStatus;
            DrawBlocks();
        }

        public BlocksStatus BlocksStatus
        {
            get { return (BlocksStatus)GetValue(BlocksStatusProperty); }
            set { SetValue(BlocksStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BlocksStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlocksStatusProperty =
            DependencyProperty.Register("BlocksStatus", typeof(BlocksStatus), typeof(BlocksControl), new PropertyMetadata(BlocksStatus.RotationZero, new PropertyChangedCallback(OnBlocksStatusChange)));



        public BlocksShape BlocksShape
        {
            get { return (BlocksShape)GetValue(BlocksShapeProperty); }
            set { SetValue(BlocksShapeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BlocksShape.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlocksShapeProperty =
            DependencyProperty.Register("BlocksShape", typeof(BlocksShape), typeof(BlocksControl), new PropertyMetadata(BlocksShape.OType));


        private static void OnBlocksStatusChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as BlocksControl;
            control.ReDrawBlocks();
        }

        private void ReDrawBlocks()
        {
            blockShowCanvas.Children.Clear();
            DrawBlocks();
        }

        private void DrawBlocks()
        {
            List<BlockControl> blockList = new List<BlockControl>();
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (blockArray[(int)BlocksShape, (int)BlocksStatus, y, x] == 1)
                    {
                        BlockControl blockControl = new BlockControl(BlocksShape);
                        blockControl.Margin = new Thickness(x * 16, y * 16, 0, 0);
                        blockList.Add(blockControl);
                    }
                }
            }
            blockList.ForEach(x => blockShowCanvas.Children.Add(x));
        }
    }
}
