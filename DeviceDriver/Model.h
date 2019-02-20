#pragma once

using namespace System;

namespace DeviceDriver {
    public enum class DeviceType : Int32
    {
        SingleChannel = 1,
        DoubleChannel = 2,
        QuadChannel = 4,
        EightChannel = 8
    };

    public enum class ChannelIndex : Int32 {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8
    };

    public enum class ChannelState : Int32 {
        Closed = 0,
        Opened = 1
    };

    public interface class IRelayChannelInfo {
        property ChannelIndex Index {
            ChannelIndex get();
        }
        property ChannelState State {
            ChannelState get();
        }
    };

    public interface class IRelayDeviceInfo {
        property String^ SerialNumber {
            String^ get();
        };
        property String^ DevicePath {
            String^ get();
        };
        property DeviceType Type {
            DeviceType get();
        };
    };

    public interface class IRelayController : public IDisposable {
        System::Collections::Generic::List<IRelayDeviceInfo^> ^ ListDevices();
        System::Collections::Generic::List<IRelayChannelInfo^> ^ ListChannels(IRelayDeviceInfo^ device);

        IRelayChannelInfo^ GetChannelInfo(IRelayDeviceInfo^ device, ChannelIndex channel);

        bool ConnectDevice(IRelayDeviceInfo^ device);

        bool OpenChannel(IRelayDeviceInfo^ device, ChannelIndex channel);
        bool CloseChannel(IRelayDeviceInfo^ device, ChannelIndex channel);
        IRelayChannelInfo^ ToggleChannel(IRelayDeviceInfo^ device, ChannelIndex channel);

        bool OpenAllChannels(IRelayDeviceInfo^ device);
        bool CloseAllChannels(IRelayDeviceInfo^ device);
    };
}