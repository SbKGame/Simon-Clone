using GalaSoft.MvvmLight.Messaging;
using SimonClone.Message;
using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace SimonClone.View
{
    /// <summary>
    /// Logique d'interaction pour GameSceneView.xaml
    /// </summary>
    public partial class GameSceneView : UserControl
    {
        public GameSceneView()
        {
            InitializeComponent();
            Initiliaze();
        }

        private void Initiliaze()
        {
            Messenger.Default.Register<ComputerPlayMessage>(this, ComputerPlay);
        }

        private void ComputerPlay(ComputerPlayMessage msg)
        {
            Button btn = null;

            switch (msg.Color)
            {
                case Enum.SimonButton.Green:
                    btn = btn_Green;
                    break;
                case Enum.SimonButton.Red:
                    btn = btn_Red;
                    break;
                case Enum.SimonButton.Yellow:
                    btn = btn_Yellow;
                    break;
                case Enum.SimonButton.Blue:
                    btn = btn_Blue;
                    break;
                default:
                    break;
            }

            Dispatcher.Invoke(() => {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn, new object[] { true });

                var timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(150);
                timer.Tick += (obj, e) =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        btn.RaiseEvent(new System.Windows.RoutedEventArgs(ButtonBase.ClickEvent));
                        typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(btn, new object[] { false });
                        timer.Stop();
                    });
                };
                timer.Start();
            });
        }
    }
}
