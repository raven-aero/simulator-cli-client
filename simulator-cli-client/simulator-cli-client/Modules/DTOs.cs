namespace simulator_cli_client.Modules;

public static class DTOs
{
    public record GetAllDevicesResponse(bool Msg, List<string> Devices);

    public record DeviceStreamInfo(string RtspStream, int SimId);

    public record StartDeviceResponse(bool Success, string? Message, List<DeviceStreamInfo>? Streams);

    public record ApiResponse(bool Success, string? Message);

    public record DeviceNameRequest(string DeviceName);
}