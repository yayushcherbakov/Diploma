namespace AuditService.ApplicationServices.Models.AuditLogs;

public record GetAuditLogsResponse
(
    List<AuditLogModel> Logs, 
    int TotalCount
);