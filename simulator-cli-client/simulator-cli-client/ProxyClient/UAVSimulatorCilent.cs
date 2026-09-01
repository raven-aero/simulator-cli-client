using simulator_cli_client.Constants;
using simulator_cli_client.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

using static simulator_cli_client.Constants.CLIConstants;
using static simulator_cli_client.Modules.DTOs;

namespace simulator_cli_client.Services
{
    public class UAVSimulatorCilent: IUAVSimulatorCilent
    {
        private readonly HttpClient _httpClient;

        public UAVSimulatorCilent(HttpClient client)
        { 
            this._httpClient = client;
        }

        public async Task<List<string>> GetAllDevicesAsync(CancellationToken ct = default)
        {
            var response = await this._httpClient.GetFromJsonAsync<GetAllDevicesResponse>(ProxyAPIs.BASE_ROUTE, ct);
            return response?.Devices ?? new List<string>();
        }

        public async Task<bool> AddDeviceAsync(string deviceName, string telemetryFilePath, string multimediaFilePath, CancellationToken ct = default)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(deviceName), CLIConstants.DeviceNameField);

            using var telemetryStream = File.OpenRead(telemetryFilePath);
            using var telemetryContent = new StreamContent(telemetryStream);
            content.Add(telemetryContent, TelemetryFileName, Path.GetFileName(telemetryFilePath));

            using var multimediaStream = File.OpenRead(multimediaFilePath); 
            using var multimediaContent = new StreamContent(multimediaStream);
            content.Add(multimediaContent, MultimediaFileName, Path.GetFileName(multimediaFilePath));

            var response = await _httpClient.PostAsync(ProxyAPIs.BASE_ROUTE, content, ct);
            var re = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<ApiResponse>(cancellationToken: ct);
            return result?.Success ?? false;
        }

        public async Task<bool> RemoveDeviceAsync(string deviceName, CancellationToken ct = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, ProxyAPIs.BASE_ROUTE)
            {
                Content = JsonContent.Create(new DeviceNameRequest(deviceName))
            };

            var response = await _httpClient.SendAsync(request, ct);
            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<ApiResponse>(cancellationToken: ct);
            return result?.Success ?? false;
        }

        public async Task<StartDeviceResponse> StartDeviceChannelsAsync(string deviceName, CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsJsonAsync(ProxyAPIs.START_DEVICE_ROUTE, new DeviceNameRequest(deviceName), ct);
            return await response.Content.ReadFromJsonAsync<StartDeviceResponse>(cancellationToken: ct)
                   ?? new StartDeviceResponse(false, UNKNOWN_ERROR, null);
        }

        public async Task<bool> StopDeviceChannelsAsync(string deviceName, CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsJsonAsync(ProxyAPIs.STOP_DEVICE_ROUTE, new DeviceNameRequest(deviceName), ct);
            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<ApiResponse>(cancellationToken: ct);
            return result?.Success ?? false;
        }

        public async Task<StartDeviceResponse> StartAllDevicesChannelsAsync(CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsync(ProxyAPIs.STARTALL_DEVICE_ROUTE, null, ct);
            return await response.Content.ReadFromJsonAsync<StartDeviceResponse>(cancellationToken: ct)
                   ?? new StartDeviceResponse(false, UNKNOWN_ERROR, null);
        }

        public async Task<ApiResponse> StopAllDevicesChannelsAsync(CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsync(ProxyAPIs.STOPALL_DEVICE_ROUTE, null, ct);
            return await response.Content.ReadFromJsonAsync<ApiResponse>(cancellationToken: ct)
                   ?? new ApiResponse(false, UNKNOWN_ERROR);
        }

        public async Task<GetActiveStreamsResponse> GetActiveStreamsAsync(CancellationToken ct = default)
        {
            var response = await _httpClient.GetFromJsonAsync<GetActiveStreamsResponse>(ProxyAPIs.GET_ACTIVE_STREAMS_ROUTE, ct);
            return response ?? new GetActiveStreamsResponse(false, Enumerable.Empty<ChannelDTO>());
        }
    }
}