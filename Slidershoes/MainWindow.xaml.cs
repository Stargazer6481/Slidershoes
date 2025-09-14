using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System.Windows.Controls;
using System.Windows.Shapes;
using Nefarius.ViGEm.Client.Targets;

namespace Slidershoes
{
    public partial class MainWindow : Window
    {
        #region WinAPI
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT { public int X; public int Y; }
        #endregion

        private ViGEmClient _client;
        private IXbox360Controller _controller;
        private bool _running;
        private int _screenCenterX;
        private int _screenCenterY;
        private Thread _controllerThread;

        public MainWindow()
        {
            InitializeComponent();

            _screenCenterX = (int)SystemParameters.PrimaryScreenWidth / 2;
            _screenCenterY = (int)SystemParameters.PrimaryScreenHeight / 2;

            StartButton.Click += StartButton_Click;
            StopButton.Click += StopButton_Click;

            this.Loaded += (s, e) => this.Focus();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            StatusText.Text = "Status: Running";

            try
            {
                _client = new ViGEmClient();
                _controller = _client.CreateXbox360Controller();
                _controller.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing controller: " + ex.Message);
                StartButton.IsEnabled = true;
                StopButton.IsEnabled = false;
                StatusText.Text = "Status: Stopped";
                return;
            }

            _running = true;

            _controllerThread = new Thread(ControllerLoop)
            {
                IsBackground = true
            };
            _controllerThread.SetApartmentState(ApartmentState.STA);
            _controllerThread.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _running = false;

            try
            {
                _controller?.Disconnect();
                _client?.Dispose();
            }
            catch { }

            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            StatusText.Text = "Status: Stopped";
        }

        private void ControllerLoop()
        {
            GetCursorPos(out POINT lastPos);

            while (_running)
            {
                GetCursorPos(out POINT currentPos);
                int deltaX = currentPos.X - lastPos.X;
                int deltaY = currentPos.Y - lastPos.Y;
                lastPos = currentPos;

                short sensitivity = (short)SensitivitySlider.Dispatcher.Invoke(() => SensitivitySlider.Value);

                short joyX = (short)Math.Max(Math.Min(deltaX * sensitivity, 32767), -32768);
                short joyY = (short)Math.Max(Math.Min(-deltaY * sensitivity, 32767), -32768);

                _controller.SetAxisValue(Xbox360Axis.LeftThumbX, joyX);
                _controller.SetAxisValue(Xbox360Axis.LeftThumbY, joyY);

                // Animate joystick indicator
                // Inside ControllerLoop -> Dispatcher.Invoke
                Dispatcher.Invoke(() =>
                {
                    double canvasCenter = JoystickCanvas.Width / 2 - ThumbIndicator.Width / 2;
                    double maxMove = JoystickCanvas.Width / 2 - ThumbIndicator.Width / 2;

                    // Direct mapping (works correctly)
                    double targetX = canvasCenter + (joyX / 32768.0) * maxMove;
                    double targetY = canvasCenter + (joyY / 32768.0) * maxMove;

                    ThumbIndicator.SetValue(Canvas.LeftProperty, targetX);
                    ThumbIndicator.SetValue(Canvas.TopProperty, targetY);
                });


                bool lockMouse = LockMouseCheckBox.Dispatcher.Invoke(() => LockMouseCheckBox.IsChecked == true);
                bool isKeyPressed = Keyboard.IsKeyDown(Key.Space);

                if (lockMouse && !isKeyPressed)
                {
                    SetCursorPos(_screenCenterX, _screenCenterY);
                    lastPos = new POINT { X = _screenCenterX, Y = _screenCenterY };
                }

                // Stop with F11
                if (Keyboard.IsKeyDown(Key.F11))
                {
                    Dispatcher.Invoke(() => StopButton_Click(null, null));
                    break;
                }

                Thread.Sleep(10);
            }
        }
    }
}
