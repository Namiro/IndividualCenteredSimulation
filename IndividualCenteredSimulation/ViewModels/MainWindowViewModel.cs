using IndividualCenteredSimulation.Helpers;
using IndividualCenteredSimulation.MAS;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IndividualCenteredSimulation.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties

        #region Constants

        #endregion

        #region Commands

        #endregion

        #region Events

        #endregion

        #region GUI

        private WriteableBitmap _WriteableBitmap;
        public WriteableBitmap WriteableBitmap
        {
            get
            {
                return _WriteableBitmap;
            }
            set
            {
                _WriteableBitmap = value;
                RaisePropertyChanged(nameof(MainWindowViewModel.WriteableBitmap));
            }
        }

        private int GridStartX { get; set; }
        private int GridStartY { get; set; }

        #endregion

        public MultiAgentSystem MultiAgentSystem { get; set; }

        #endregion

        #region Construtors

        public MainWindowViewModel()
        {
            MultiAgentSystem = new MultiAgentSystem();

            WriteableBitmap = BitmapFactory.New(MultiAgentSystem.Grid.XSize * App.BoxSize + 1, MultiAgentSystem.Grid.YSize * App.BoxSize + 1);

            //This allow to refresh the data to display when the value from the system is modified
            MultiAgentSystem.PropertyChanged += (obj, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(MultiAgentSystem.Grid):
                        RefereshView();
                        break;
                    default:
                        break;
                }
            };

            RefereshView();
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method allow to refresh the view.
        /// It means to draw the view in function of the value of MultiAgentSystem.Agents
        /// </summary>
        public void RefereshView()
        {
            App.StartExec = DateTime.Now;

            Application.Current.Dispatcher.Invoke(() =>
            {
                //WriteableBitmap.Lock();

                using (WriteableBitmap.GetBitmapContext())
                {
                    // Clear the WriteableBitmap with white color
                    WriteableBitmap.Clear(Colors.White);

                    DrawGrid();

                    for (int i = 0; i < MultiAgentSystem.Grid.XSize; i++)
                    {
                        for (int j = 0; j < MultiAgentSystem.Grid.YSize; j++)
                        {
                            DrawGridCell(new Coordinate(i, j), ((IDrawable)MultiAgentSystem.Grid.Get(i, j)).Color);
                        }
                    }
                    RaisePropertyChanged(nameof(MainWindowViewModel.WriteableBitmap));

                    Logger.WriteLog("Draw time : " + DateTime.Now.Subtract(App.StartExec).Milliseconds);
                }

                //WriteableBitmap.Unlock();
            });
        }

        private void DrawGrid()
        {
            DrawNumCell();

            int x = GridStartX;
            int y = GridStartY;
            for (int i = 0; i < MultiAgentSystem.Grid.XSize; i++)
            {
                for (int j = 0; j < MultiAgentSystem.Grid.YSize; j++)
                {
                    WriteableBitmap.DrawRectangle(x, y, x + App.BoxSize, y + App.BoxSize, GraphicHelper.CastColor(System.Drawing.Color.Black));
                    y += App.BoxSize;
                }
                x += App.BoxSize;
                y = GridStartY;
            }
        }

        private void DrawNumCell()
        {
            GridStartX = App.BoxSize + 5;
            GridStartY = App.BoxSize + 5;
            WriteableBitmap = BitmapFactory.New(MultiAgentSystem.Grid.XSize * App.BoxSize + 1 + GridStartX, MultiAgentSystem.Grid.YSize * App.BoxSize + 1 + GridStartY);

            int x = GridStartX;
            int y = GridStartY;
            for (int i = 0; i < MultiAgentSystem.Grid.XSize; i++)
            {
                LetterGlyphTool.DrawString(WriteableBitmap, x, 0 + App.BoxSize / 4, Colors.Black, new PortableFontDesc(), i + "");
                x += App.BoxSize;
            }

            for (int i = 0; i < MultiAgentSystem.Grid.YSize; i++)
            {
                LetterGlyphTool.DrawString(WriteableBitmap, 0, y + App.BoxSize / 4, Colors.Black, new PortableFontDesc(), i + "");
                y += App.BoxSize;
            }
        }

        private void DrawGridCell(Coordinate coordinate, Color color)
        {
            int x = GridStartX + coordinate.X * App.BoxSize;
            int y = GridStartY + coordinate.Y * App.BoxSize;

            WriteableBitmap.FillEllipse(x + 2, y + 2, x + App.BoxSize - 2, y + App.BoxSize - 2, color);
        }

        private void DrawGridCell(Coordinate coordinate, WriteableBitmap BitmapImage)
        {
            int x = GridStartX + coordinate.X * App.BoxSize;
            int y = GridStartY + coordinate.Y * App.BoxSize;

            // TODO Afficher une image dans une celulle de la grille
        }

        private void DrawGridCell(Coordinate coordinate, string text)
        {
            int x = GridStartX + coordinate.X * App.BoxSize;
            int y = GridStartY + coordinate.Y * App.BoxSize;

            LetterGlyphTool.DrawString(WriteableBitmap, x, y + App.BoxSize / 4, Colors.Black, new PortableFontDesc(), text);
        }
        #endregion
    }
}
