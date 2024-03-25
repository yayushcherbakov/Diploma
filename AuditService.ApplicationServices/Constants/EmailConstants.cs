namespace AuditService.ApplicationServices.Constants;

public static class EmailConstants
{
    public static class ResetPassword
    {
        public const string Subject = "Reset Your Password";

        public const string Body = """
                                   <div>Dear {0},</div>
                                   <div>We received a request to reset your password for {1} account.</div>
                                   <div>Password reset token: {2}</div>
                                   <div>If you did not request a password reset, please disregard this email. Your account will remain secure.</div> 
                                   <div>Best Regards.</div>
                                   """;
    }
}
