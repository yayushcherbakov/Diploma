﻿namespace AuditService.ApplicationServices.Models.Identities;

public record GetAllUsersPayload
(
    int Page,
    int ItemsPerPage
);