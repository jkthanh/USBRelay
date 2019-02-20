#include "../usb_relay_device/usb_relay_device.h"
#include "Model.h"
#include "DeviceDriver.h"

#pragma once

using namespace System;
using namespace System::Runtime::InteropServices;

namespace DeviceDriver {
    private ref class USBRelayChannelInfo : public IRelayChannelInfo {
    public:
        virtual property ChannelIndex Index;
        virtual property ChannelState State;
    };

    private ref class USBRelayDeviceInfo : public IRelayDeviceInfo {
    public:
        virtual property String^ SerialNumber;
        virtual property String^ DevicePath;
        virtual property DeviceType Type;
    internal:
        int m_handle;
    };

    public ref class RelayController : public IRelayController {
    public:
        RelayController() : RelayController(gcnew DriverWrapper()) {}
        ~RelayController();

        virtual System::Collections::Generic::List<IRelayDeviceInfo^> ^ ListDevices();
        virtual System::Collections::Generic::List<IRelayChannelInfo^> ^ ListChannels(IRelayDeviceInfo^ device);

        virtual IRelayChannelInfo^ GetChannelInfo(IRelayDeviceInfo^ device, ChannelIndex channel);

        virtual bool ConnectDevice(IRelayDeviceInfo^ device);

        virtual bool OpenChannel(IRelayDeviceInfo^ device, ChannelIndex channel);
        virtual bool CloseChannel(IRelayDeviceInfo^ device, ChannelIndex channel);
        virtual IRelayChannelInfo^ ToggleChannel(IRelayDeviceInfo^ device, ChannelIndex channel);

        virtual bool OpenAllChannels(IRelayDeviceInfo^ device);
        virtual bool CloseAllChannels(IRelayDeviceInfo^ device);

    protected:
        !RelayController();
        RelayController(IDeviceDriver^ deviceDriver);
    private:
        bool m_isDisposed;
        IDeviceDriver^ m_deviceDriver;
        System::Collections::Generic::Dictionary<String^, USBRelayDeviceInfo^>^ m_openDevices;

        USBRelayChannelInfo^ CreateChannelInfo(ChannelIndex channel, unsigned int status);
        ChannelState GetState(ChannelIndex channel, unsigned int status);
        USBRelayDeviceInfo^ GetRealDevice(IRelayDeviceInfo^ device);
        USBRelayChannelInfo^ GetChannel(USBRelayDeviceInfo^ device, ChannelIndex channel);
        void AssertDeviceConnected(USBRelayDeviceInfo^ device);
    };
}