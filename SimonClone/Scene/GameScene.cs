using GalaSoft.MvvmLight.Command;
using SbKGame.Core.Scene;
using SimonClone.Enum;
using SimonClone.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SimonClone.Scene
{
    public class GameScene : SceneBase
    {
        #region -- Fields --
        private int _score;
        private bool _isComputerMode;
        private int _indexPlayerSequence;
        private bool _isPlayerLose;

        private Random _random = new Random();
        private List<SimonButton> _sequence;
        #endregion

        #region -- Public Properties --
        public int Score
        {
            get { return _score; }
            set { Set(ref _score, value); }
        }

        public bool IsComputerMode
        {
            get { return _isComputerMode; }
            set { Set(ref _isComputerMode, value); }
        }

        public bool IsPlayerLose
        {
            get { return _isPlayerLose; }
            set { Set(ref _isPlayerLose, value); }
        }
        #endregion

        #region -- Commands --
        public RelayCommand GreenButtonCommand { get; private set; }
        public RelayCommand RedButtonCommand { get; private set; }
        public RelayCommand YellowButtonCommand { get; private set; }
        public RelayCommand BlueButtonCommand { get; private set; }
        public RelayCommand RestartGameCommand { get; private set; }
        public RelayCommand ExitGameCommand { get; private set; }
        #endregion    

        #region -- Override Methods --
        public override void Initiliaze()
        {
            Score = 0;
            IsComputerMode = true;
            IsPlayerLose = false;
            _indexPlayerSequence = 0;
            _sequence = new List<SimonButton>();

            GreenButtonCommand = new RelayCommand(() => TrySequence(SimonButton.Green), () => !IsComputerMode);
            RedButtonCommand = new RelayCommand(() => TrySequence(SimonButton.Red), () => !IsComputerMode);
            YellowButtonCommand = new RelayCommand(() => TrySequence(SimonButton.Yellow), () => !IsComputerMode);
            BlueButtonCommand = new RelayCommand(() => TrySequence(SimonButton.Blue), () => !IsComputerMode);

            RestartGameCommand = new RelayCommand(Initiliaze);
            ExitGameCommand = new RelayCommand(Dispatcher.CurrentDispatcher.InvokeShutdown);
        }

        public async void SwitchModeAnimationEnd()
        {
            RefreshCommands();

            if (IsComputerMode)
            {
                UpdateSequence();
                await PlaySequence();
                ToggleComputerMode();
            }
        }
        #endregion

        #region -- Private Methods --
        private void ToggleComputerMode()
        {
            IsComputerMode = !IsComputerMode;
        }

        private void TrySequence(SimonButton colorButton)
        {
            if(_sequence[_indexPlayerSequence] == colorButton)
            {
                Score++;
                _indexPlayerSequence++;
            }
            else
            {
                IsPlayerLose = true;
            }

            if(_sequence.Count == _indexPlayerSequence)
            {
                _indexPlayerSequence = 0;
                ToggleComputerMode();
            }
        }

        private async Task PlaySequence()
        {
            if (!_sequence.Any()) return;

            await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                foreach (var item in _sequence)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    MessengerInstance.Send(new ComputerPlayMessage(item));
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            });        
        }

        private void UpdateSequence()
        {        
            var newItem = (SimonButton)_random.Next(0, 4);
            _sequence.Add(newItem);
        }

        private void RefreshCommands()
        {
            GreenButtonCommand.RaiseCanExecuteChanged();
            RedButtonCommand.RaiseCanExecuteChanged();
            YellowButtonCommand.RaiseCanExecuteChanged();
            BlueButtonCommand.RaiseCanExecuteChanged();
        }
        #endregion
    }
}
