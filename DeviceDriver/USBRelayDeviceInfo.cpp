#include "stdafx.h"
#include "Model.h"
#include "USBRelayDeviceInfo.h"

namespace DeviceDriver {
    USBRelayDeviceInfo::USBRelayDeviceInfo(IDeviceDriver^ deviceDriver) { 
        m_deviceDriver = deviceDriver;
    }
    USBRelayDeviceInfo::~USBRelayDeviceInfo() {
        if (m_isDisposed)
            return;

        this->!USBRelayDeviceInfo();
        m_isDisposed = true;
    }  
    USBRelayDeviceInfo::!USBRelayDeviceInfo() {
        Close();
    }
}