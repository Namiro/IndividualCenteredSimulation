using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace MultiAgentSystem.HunterSystem.ViewModels
{
    class CommandXNAHunterGrid : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private static XNAHunterGrid XNAHunterGrid { get; set; }

        public CommandXNAHunterGrid()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Initialize(XNAHunterGrid xNAHunterGrid)
        {
            XNAHunterGrid = xNAHunterGrid;
        }

        public void Execute(object parameter)
        {
            switch (((Control)parameter).Name)
            {
                case "ButtonPlayPause":
                    XNAHunterGrid.PlayPauseControl();
                    break;
                case "ButtonRestart":
                    XNAHunterGrid.Restart();
                    break;
                default:
                    break;
            }
        }
    }
}
