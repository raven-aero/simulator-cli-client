namespace simulator_cli_client.Modules;

public static class DTOs
{
    //========dto for channel information=========================
    public class ChannelDTO
    {
        public int Id { get; set; }
        public int SourceFilesId { get; set; }
        public string StreamEndpoint { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int? FFmpegProcessId { get; set; }
        public bool IsActive { get; set; }
    }

    public record DeviceStreamInfo(string RtspStream, int SimId);

    //================================================================
    public record GetAllDevicesResponse(bool Msg, List<string> Devices);

    public record StartDeviceResponse(bool Success, string? Message, List<DeviceStreamInfo>? Streams);

    public record ApiResponse(bool Success, string? Message);
    
    public record GetActiveStreamsResponse(bool Success, IEnumerable<ChannelDTO> Streams);

    public record DeviceNameRequest(string DeviceName);
}