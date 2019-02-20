// DeviceDriver.h
#pragma once
#pragma comment(lib, "../usb_relay_device/usb_relay_device.lib")
#include "../usb_relay_device/usb_relay_device.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace DeviceDriver {

    public interface class IDeviceDriver {
        int UsbRelayInit();
        int UsbRelayExit();        
        usb_relay_device_info * UsbRelayDeviceEnumerate();
        void UsbRelayDeviceFreeEnumerate(usb_relay_device_info * info);
        void UsbRelayDeviceClose(int hHandle);
        int UsbRelayDeviceOpen(struct usb_relay_device_info* device_info);
        int UsbRelayDeviceOpenWithSerialNumber(const char *serial_number, unsigned len);
        int UsbRelayDeviceOpenOneRelayChannel(int hHandle, int index);
        int UsbRelayDeviceOpenAllRelayChannel(int hHandle);
        int UsbRelayDeviceCloseOneRelayChannel(int hHandle, int index);
        int UsbRelayDeviceCloseAllRelayChannel(int hHandle);
        int UsbRelayDeviceGetStatus(int hHandle, unsigned int *status);
    };

    private ref class DriverWrapper : public IDeviceDriver {
    public:
        virtual int UsbRelayInit();
        virtual int UsbRelayExit();
        virtual usb_relay_device_info * UsbRelayDeviceEnumerate();
        virtual void UsbRelayDeviceFreeEnumerate(usb_relay_device_info * info);
        virtual void UsbRelayDeviceClose(int hHandle);
        virtual int UsbRelayDeviceOpen(struct usb_relay_device_info* device_info);
        virtual int UsbRelayDeviceOpenWithSerialNumber(const char *serial_number, unsigned len);
        virtual int UsbRelayDeviceOpenOneRelayChannel(int hHandle, int index);
        virtual int UsbRelayDeviceOpenAllRelayChannel(int hHandle);
        virtual int UsbRelayDeviceCloseOneRelayChannel(int hHandle, int index);
        virtual int UsbRelayDeviceCloseAllRelayChannel(int hHandle);
        virtual int UsbRelayDeviceGetStatus(int hHandle, unsigned int *status);
	};
}
