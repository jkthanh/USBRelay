#include "stdafx.h"
#include "DeviceDriver.h"

namespace DeviceDriver {
    int DriverWrapper::UsbRelayInit() {
        return usb_relay_init();
    }
    int DriverWrapper::UsbRelayExit() {
        return usb_relay_exit();
    }
    usb_relay_device_info * DriverWrapper::UsbRelayDeviceEnumerate() {
        return usb_relay_device_enumerate();
    }
    void DriverWrapper::UsbRelayDeviceFreeEnumerate(usb_relay_device_info * info) {
        return usb_relay_device_free_enumerate(info);
    }
    void DriverWrapper::UsbRelayDeviceClose(int hHandle) {
        return usb_relay_device_close(hHandle);
    }
    int DriverWrapper::UsbRelayDeviceOpen(struct usb_relay_device_info* device_info) {
        return usb_relay_device_open(device_info);
    }
    int DriverWrapper::UsbRelayDeviceOpenWithSerialNumber(const char *serial_number, unsigned len) {
        return usb_relay_device_open_with_serial_number(serial_number, len);
    }
    int DriverWrapper::UsbRelayDeviceOpenOneRelayChannel(int hHandle, int index) {
        return usb_relay_device_open_one_relay_channel(hHandle, index);
    }
    int DriverWrapper::UsbRelayDeviceOpenAllRelayChannel(int hHandle) {
        return usb_relay_device_open_all_relay_channel(hHandle);
    }
    int DriverWrapper::UsbRelayDeviceCloseOneRelayChannel(int hHandle, int index) {
        return usb_relay_device_close_one_relay_channel(hHandle, index);
    }
    int DriverWrapper::UsbRelayDeviceCloseAllRelayChannel(int hHandle) {
        return usb_relay_device_close_all_relay_channel(hHandle);
    }
    int DriverWrapper::UsbRelayDeviceGetStatus(int hHandle, unsigned int *status) {
        return usb_relay_device_get_status(hHandle, status);
    }
}