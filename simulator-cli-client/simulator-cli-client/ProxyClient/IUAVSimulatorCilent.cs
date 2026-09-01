using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static simulator_cli_client.Modules.DTOs;

namespace simulator_cli_client.Interfaces
{
    public interface IUAVSimulatorCilent
    {
        Task<List<string>> GetAllDevicesAsync(CancellationToken ct = default);
        Task<bool> AddDeviceAsync(string deviceName, string telemetryFilePath, string multimediaFilePath, CancellationToken ct = default);
        Task<bool> RemoveDeviceAsync(string deviceName, CancellationToken ct = default);
        Task<StartDeviceResponse> StartDeviceChannelsAsync(string deviceName, CancellationToken ct = default);
        Task<bool> StopDeviceChannelsAsync(string deviceName, CancellationToken ct = default);
        Task<StartDeviceResponse> StartAllDevicesChannelsAsync(CancellationToken ct = default);
        Task<ApiResponse> StopAllDevicesChannelsAsync(CancellationToken ct = default);
    }
}