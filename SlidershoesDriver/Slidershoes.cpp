// SlidershoesDriver.cpp
#include <openvr_driver.h>
#include <windows.h>
#include <cstdint>
#include <cstring>

using namespace vr;

// ------------------ Slidershoes Device -------------------
class SlidershoesDevice : public ITrackedDeviceServerDriver {
public:
    SlidershoesDevice() {
        screenCenterX = GetSystemMetrics(SM_CXSCREEN) / 2;
        screenCenterY = GetSystemMetrics(SM_CYSCREEN) / 2;
        SetCursorPos(screenCenterX, screenCenterY);
    }
    virtual ~SlidershoesDevice() {}

    EVRInitError Activate(uint32_t unObjectId) override {
        m_unObjectId = unObjectId;
        return VRInitError_None;
    }

    void Deactivate() override {}
    void EnterStandby() override {}

    void* GetComponent(const char* pchComponentNameAndVersion) override {
        return nullptr;
    }

    void DebugRequest(const char* pchRequest, char* pchResponseBuffer, uint32_t unResponseBufferSize) override {
        if (pchResponseBuffer && unResponseBufferSize > 0)
            pchResponseBuffer[0] = '\0';
    }

    DriverPose_t GetPose() override {
        DriverPose_t pose = {};
        pose.poseIsValid = true;
        pose.deviceIsConnected = true;

        // Mouse movement to fake position
        POINT p;
        if (GetCursorPos(&p)) {
            int dx = p.x - screenCenterX;
            int dz = p.y - screenCenterY;

            footX += dx * 0.01f;
            footZ += dz * 0.01f;

            SetCursorPos(screenCenterX, screenCenterY);
        }

        pose.vecPosition[0] = footX;
        pose.vecPosition[1] = 0.0f;
        pose.vecPosition[2] = footZ;
        pose.qRotation.w = 1.0f;

        return pose;
    }

private:
    uint32_t m_unObjectId = k_unTrackedDeviceIndexInvalid;
    float footX = 0.0f;
    float footZ = 0.0f;
    int screenCenterX, screenCenterY;
};

// ------------------ Provider -------------------
class SlidershoesProvider : public IServerTrackedDeviceProvider {
public:
    SlidershoesProvider() {}
    virtual ~SlidershoesProvider() {}

    EVRInitError Init(vr::IVRDriverContext* pDriverContext) override {
        VR_INIT_SERVER_DRIVER_CONTEXT(pDriverContext);
        m_pDevice = new SlidershoesDevice();
        VRServerDriverHost()->TrackedDeviceAdded("slidershoes", TrackedDeviceClass_GenericTracker, m_pDevice);
        return VRInitError_None;
    }

    void Cleanup() override {
        delete m_pDevice;
        m_pDevice = nullptr;
    }

    const char* const* GetInterfaceVersions() override {
        return k_InterfaceVersions;
    }

    void RunFrame() override {
        if (m_pDevice)
            VRServerDriverHost()->TrackedDevicePoseUpdated(0, m_pDevice->GetPose(), sizeof(DriverPose_t));
    }

    bool ShouldBlockStandbyMode() override { return false; }
    void EnterStandby() override {}
    void LeaveStandby() override {}

private:
    SlidershoesDevice* m_pDevice = nullptr;
};

// ------------------ Factory -------------------
extern "C" __declspec(dllexport) void* HmdDriverFactory(const char* pInterfaceName, int* pReturnCode) {
    if (0 == strcmp(pInterfaceName, IServerTrackedDeviceProvider_Version)) {
        if (pReturnCode) *pReturnCode = VRInitError_None;
        static SlidershoesProvider g_provider;
        return &g_provider;
    }
    if (pReturnCode) *pReturnCode = VRInitError_Init_InterfaceNotFound;
    return nullptr;
}
