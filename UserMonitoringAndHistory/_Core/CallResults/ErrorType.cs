namespace UserMonitoringAndHistory
{
    public enum ErrorType
    {
        NotError = 0,
        ValidationError400,
        UnauthorizedError401,
        AccessDenied403,
        NotFoundError404,
        WrongHttpMethodError405,
        ModelWasNotInitialized400,
        UnexpectedError500,
        ThirdPartyApiError417,
        CanNotFindParameters415,
        ApiIsNotAvailable,
        CacheIsNotAvailable,
    }
}
