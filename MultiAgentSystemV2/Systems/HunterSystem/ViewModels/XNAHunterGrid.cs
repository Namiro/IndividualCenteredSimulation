using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MultiAgentSystem.Cores.Constants;
using MultiAgentSystem.Cores.Helpers;
using MultiAgentSystem.Cores.ViewModels;
using MultiAgentSystem.HunterSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MultiAgentSystem.HunterSystem.ViewModels
{
    internal class XNAHunterGrid : XNASurface, INotifyPropertyChanged
    {

        #region GUI

        private string _LabelGameTimeSeconds = "0";
        public string LabelGameTimeSeconds
        {
            get
            {
                return _LabelGameTimeSeconds;
            }
            set
            {
                _LabelGameTimeSeconds = value;
                RaisePropertyChanged(nameof(XNAHunterGrid.LabelGameTimeSeconds));
            }
        }

        private string _LabelNumberHunters = "0";
        public string LabelNumberHunters
        {
            get
            {
                return _LabelNumberHunters;
            }
            set
            {
                _LabelNumberHunters = value;
                RaisePropertyChanged(nameof(XNAHunterGrid.LabelNumberHunters));
            }
        }

        private string _LabelNumberDefendersEaten = "0";
        public string LabelNumberDefendersEaten
        {
            get
            {
                return _LabelNumberDefendersEaten;
            }
            set
            {
                _LabelNumberDefendersEaten = value;
                RaisePropertyChanged(nameof(XNAHunterGrid.LabelNumberDefendersEaten));
            }
        }

        private string _LabelWallPercent = "0";
        public string LabelWallPercent
        {
            get
            {
                return _LabelWallPercent;
            }
            set
            {
                _LabelWallPercent = value;
                RaisePropertyChanged(nameof(XNAHunterGrid.LabelWallPercent));
            }
        }

        private string _LabelHunterSpeedPercent = "0";
        public string LabelHunterSpeedPercent
        {
            get
            {
                return _LabelHunterSpeedPercent;
            }
            set
            {
                _LabelHunterSpeedPercent = value;
                RaisePropertyChanged(nameof(XNAHunterGrid.LabelHunterSpeedPercent));
            }
        }

        private string _LabelAvatarSpeedPercent = "0";
        public string LabelAvatarSpeedPercent
        {
            get
            {
                return _LabelAvatarSpeedPercent;
            }
            set
            {
                _LabelAvatarSpeedPercent = value;
                RaisePropertyChanged(nameof(XNAHunterGrid.LabelAvatarSpeedPercent));
            }
        }

        private string _PlayPauseString = "Play";
        public string PlayPauseString
        {
            get
            {
                return _PlayPauseString;
            }
            set
            {
                _PlayPauseString = value;
                RaisePropertyChanged(nameof(XNAHunterGrid.PlayPauseString));
            }
        }

        private int _SliderAvatarSpeedPercent = App.SpeedPercentAvatar;
        public int SliderAvatarSpeedPercent
        {
            get
            {
                return _SliderAvatarSpeedPercent;
            }
            set
            {
                _SliderAvatarSpeedPercent = value;
                App.SpeedPercentAvatar = value;
                RaisePropertyChanged(nameof(XNAHunterGrid.SliderAvatarSpeedPercent));
            }
        }

        private int _SliderHunterSpeedPercent = App.SpeedPercentHunter;
        public int SliderHunterSpeedPercent
        {
            get
            {
                return _SliderHunterSpeedPercent;
            }
            set
            {
                _SliderHunterSpeedPercent = value;
                App.SpeedPercentHunter = value;
                RaisePropertyChanged(nameof(XNAHunterGrid.SliderHunterSpeedPercent));
            }
        }





        public CommandXNAHunterGrid CommandXNAHunterGrid { get; private set; }

        #endregion


        public volatile static DirectionEnum UserDirectionChoose = DirectionEnum.NoOne;

        public event PropertyChangedEventHandler PropertyChanged;

        public static bool IsGameOver { get; set; } = false;
        public static bool IsWinner { get; set; } = false;
        public static bool IsPlaying { get; set; } = false;
        public static bool IsSuperAvatar { get; set; } = false;

        /// <summary>
        /// Allow to stop generate Defender when Diamond is display
        /// </summary>
        private static bool IsDisplayWinnerDiamond { get; set; } = false;

        /// <summary>
        /// This property allow to avoid to generate more than one Defender by second
        /// </summary>
        private int CurrentGameSeconds { get; set; } = 0;

        /// <summary>
        /// list of defender generate
        /// </summary>
        private List<Defender> Defenders { get; set; } = new List<Defender>();

        /// <summary>
        /// Allow to know if the avatar has eaten a defender
        /// </summary>
        private int PreviousAvatarDefenderEaten { get; set; } = 0;

        /// <summary>
        /// When the last defender has been eaten
        /// </summary>
        private TimeSpan WhenDefenderEaten { get; set; }

        /// <summary>
        /// Second as super avatar when the avatar eat a defender
        /// </summary>
        private double SecondsAsSuperAvatar { get; set; } = 5;

        /// <summary>
        /// Second wait before a new defender appear
        /// </summary>
        private int SecondsForNewDefender { get; set; } = 5;

        private GameTime PausedGameTime { get; set; }

        private Texture2D GameOverTexture { get; set; }
        private Texture2D WinnerTexture { get; set; }


        public XNAHunterGrid() : base()
        {
            LabelNumberHunters = App.HuntersNumber + "";
            LabelAvatarSpeedPercent = App.SpeedPercentAvatar + "";
            LabelHunterSpeedPercent = App.SpeedPercentHunter + "";
            LabelWallPercent = App.WallsPercent + "";

            CommandXNAHunterGrid = new CommandXNAHunterGrid();
        }

        protected override sealed void LoadContent()
        {

            Environment = new HunterEnvironment();
            base.LoadContent();

            BackgroundColor = Color.Black;

            GameOverTexture = ContentManager.Load<Texture2D>("gameover");
            WinnerTexture = ContentManager.Load<Texture2D>("winner");

            CommandXNAHunterGrid.Initialize(this);
        }

        protected override sealed void Update(GameTime gameTime)
        {
            Avatar avatar = ((HunterEnvironment)Environment).Avatar;
            if (!IsGameOver && !IsWinner && IsPlaying)
            {
                LabelGameTimeSeconds = ((int)gameTime.TotalGameTime.TotalSeconds) + "";
                LabelNumberDefendersEaten = avatar.DefenderEaten + "";

                // Every X second we create a new Defender
                if ((gameTime.TotalGameTime.Seconds % SecondsForNewDefender) == 0 && gameTime.TotalGameTime.Seconds != CurrentGameSeconds && !IsDisplayWinnerDiamond)
                {
                    Defender defender = new Defender();
                    Defenders.Add(defender);
                    defender.CreationTime = gameTime.TotalGameTime;
                    defender.Coordinate = Environment.Grid.GetRandomFreeCoordinate();
                    Environment.Grid.Occupy(defender);
                    CurrentGameSeconds = gameTime.TotalGameTime.Seconds;
                }

                // Create a new WinnerDiamond if avatar has eaten more then 3 Defender
                if (avatar.DefenderEaten > 3 && !IsDisplayWinnerDiamond)
                {
                    WinnerDiamond winnerDiamond = new WinnerDiamond();
                    winnerDiamond.Coordinate = Environment.Grid.GetRandomFreeCoordinate();
                    Environment.Grid.Occupy(winnerDiamond);
                    IsDisplayWinnerDiamond = true;
                }

                // Allow to activate/desactivate Super avatar
                if (avatar.DefenderEaten > PreviousAvatarDefenderEaten || (IsSuperAvatar && gameTime.TotalGameTime.TotalSeconds < WhenDefenderEaten.TotalSeconds + SecondsAsSuperAvatar))
                {
                    if (!IsSuperAvatar || avatar.DefenderEaten > PreviousAvatarDefenderEaten)
                        WhenDefenderEaten = gameTime.TotalGameTime;

                    IsSuperAvatar = true;
                    PreviousAvatarDefenderEaten = avatar.DefenderEaten;
                }
                else
                {
                    IsSuperAvatar = false;
                }

                // Allow to remove defenders with expired timelife
                for (int i = 0; i < Defenders.Count; i++)
                {
                    if (Defenders[i].IsLifeTimeOut(gameTime.TotalGameTime))
                    {
                        Environment.Grid.Free(Defenders[i].Coordinate);
                        Defenders.Remove(Defenders[i]);
                    }
                }

                KeyboardState keyboardState = Keyboard.GetState();

                // The others and simple move
                if (keyboardState.IsKeyDown(Keys.Right))
                    UserDirectionChoose = DirectionEnum.Right;
                else if (keyboardState.IsKeyDown(Keys.Left))
                    UserDirectionChoose = DirectionEnum.Left;
                else if (keyboardState.IsKeyDown(Keys.Up))
                    UserDirectionChoose = DirectionEnum.Up;
                else if (keyboardState.IsKeyDown(Keys.Down))
                    UserDirectionChoose = DirectionEnum.Down;

                // If Moore, it's possible to move in diagonale
                if (App.IsMoore && keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Up))
                    UserDirectionChoose = DirectionEnum.UpRight;
                else if (App.IsMoore && keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Up))
                    UserDirectionChoose = DirectionEnum.UpLeft;
                else if (App.IsMoore && keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Down))
                    UserDirectionChoose = DirectionEnum.DownRight;
                else if (App.IsMoore && keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Down))
                    UserDirectionChoose = DirectionEnum.DownLeft;

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (IsGameOver)
            {
                base.Draw(gameTime);
                SpriteBatch.Begin();

                SpriteBatch.Draw(GameOverTexture, new Vector2((App.CanvasSizeX / 2) - (GameOverTexture.Width / 2), (App.CanvasSizeY / 2) - (GameOverTexture.Height / 2)));

                SpriteBatch.End();
            }
            else if (IsWinner)
            {
                base.Draw(gameTime);
                SpriteBatch.Begin();

                SpriteBatch.Draw(WinnerTexture, new Vector2((App.CanvasSizeX / 2) - (GameOverTexture.Width / 2), (App.CanvasSizeY / 2) - (GameOverTexture.Height / 2)));

                SpriteBatch.End();
            }
            else
                base.Draw(gameTime);
        }

        public void Restart()
        {
            this.LoadContent();
            UserDirectionChoose = DirectionEnum.NoOne;
            IsGameOver = false;
            IsWinner = false;
            IsPlaying = false;
            IsSuperAvatar = false;
            TickCount = 0;
            PreviousAvatarDefenderEaten = 0;

            IsDisplayWinnerDiamond = false;
            CurrentGameSeconds = 0;
            Defenders.Clear();
        }

        public void PlayPauseControl()
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                PlayPauseString = "Play !";
            }
            else
            {
                IsPlaying = true;
                PlayPauseString = "Pause !";
            }
        }


        /// <summary>
        /// Allow to raise property changement
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
