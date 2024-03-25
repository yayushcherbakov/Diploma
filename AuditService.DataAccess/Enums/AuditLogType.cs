namespace AuditService.DataAccess.Enums;

public enum AuditLogType
{
    Registration,
    Login,
    TokenRefresh,
    PasswordChanged,
    UserUpdated,
    UserRolesUpdated,
    TokenRefreshRevoked,
    ResetPasswordRequest,
    ResetPasswordConfirm
}
