using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using DeviceDriver;

namespace parking
{
    /// <summary>
    /// Interaction logic for ChannelInfoControl.xaml
    /// </summary>
    public partial class ChannelInfoControl : INotifyPropertyChanged
    {
        //public event EventHandler<ChannelToggledEventArgs> ToggleStateClicked;

        public IRelayChannelInfo ChannelInfo
        {
            get { return _channelInfo; }
            set
            {
                _channelInfo = value;
                OnPropertyChanged();
            }
        }

        private readonly IRelayDeviceInfo _deviceInfo;
        private IRelayChannelInfo _channelInfo;

        public ChannelInfoControl(IRelayDeviceInfo deviceInfo, IRelayChannelInfo channelInfo)
        {
            ChannelInfo = channelInfo;
            _deviceInfo = deviceInfo;

            //InitializeComponent();
        }

        
        //private void btnToggle_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ToggleStateClicked != null)
        //    {
        //        ToggleStateClicked(this, new ChannelToggledEventArgs
        //        {
        //            ChannelInfo = ChannelInfo,
        //            DeviceInfo = _deviceInfo
        //        });
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
