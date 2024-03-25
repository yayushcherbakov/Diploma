using AuditService.DataAccess.Enums;

namespace AuditService.ApplicationServices.Models.AuditLogs;

public record AuditLogModel(
    Guid UserId,
    string UserName,
    DateTime Timestamp,
    AuditLogType AuditLogType
);
