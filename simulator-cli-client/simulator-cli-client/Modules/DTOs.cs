namespace simulator_cli_client.Modules
{
    public static class DTOs
    {
        // Response Models
        public record GetAllDevicesResponse(bool Msg, List<string> Devices);
        public record ApiResponse(bool Success, string? Message);

        // Request Models
        public record DeviceNameRequest(string DeviceName);
    }
}
