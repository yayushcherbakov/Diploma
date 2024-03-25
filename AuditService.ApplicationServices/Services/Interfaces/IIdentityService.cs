﻿using AuditService.ApplicationServices.Models.Identities;

namespace AuditService.ApplicationServices.Services.Interfaces;

public interface IIdentityService
{
    public Task<AuthResponse> Authenticate
    (
        AuthRequest request,
        CancellationToken cancellationToken
    );

    public Task<UserInformationWithRoles> GetUserInformation
    (
        Guid userId,
        CancellationToken cancellationToken
    );

    public Task<AuthResponse> RegisterStudent
    (
        RegisterStudentPayload userPayload,
        CancellationToken cancellationToken
    );
    
    public Task<AuthResponse> RegisterAdministrator
    (
        RegisterAdministratorPayload userPayload,
        CancellationToken cancellationToken
    );
    
    public Task<AuthResponse> RegisterTeacher
    (
        RegisterTeacherPayload userPayload,
        CancellationToken cancellationToken
    );

    public Task<GetAllUsersResponse> GetAllUsers
    (
        GetAllUsersPayload payload,
        CancellationToken cancellationToken
    );

    public Task Delete
    (
        Guid userId,
        CancellationToken cancellationToken
    );

    public Task UpdateUser
    (
        Guid userId,
        UpdateUserRequest request,
        CancellationToken cancellationToken
    );

    public Task ChangePassword
    (
        Guid userId,
        ChangePasswordRequest request,
        CancellationToken cancellationToken
    );

    public Task<TokenModel> RefreshToken
    (
        TokenModel tokenModel,
        CancellationToken cancellationToken
    );

    public Task RevokeToken
    (
        Guid userId,
        CancellationToken cancellationToken
    );

    public Task RevokeAll
    (
        CancellationToken cancellationToken
    );

    public Task RequestResetPassword
    (
        RequestResetPasswordPayload payload,
        CancellationToken cancellationToken
    );
    
    public Task ConfirmResetPassword
    (
        ConfirmResetPasswordPayload payload,
        CancellationToken cancellationToken
    );
}
