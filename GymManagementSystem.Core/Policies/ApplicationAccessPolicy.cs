namespace GymManagementSystem.Core.Policies;
public static class ApplicationAccessPolicy
{
    private static readonly Dictionary<string, string[]> _rules =
        new()
        {
            ["Web"] = new[]
            {
                "Client",
                "Trainer",
                "GroupInstructor"
            },
            ["Backoffice"] = new[]
            {
                "Manager",
                "Receptionist",
                "Owner"
            }
        };

    public static bool CanAccess(string appType, IEnumerable<string> userRoles)
    {
        if (!_rules.TryGetValue(appType, out var allowedRoles))
            return false;

        return userRoles.Any(item => allowedRoles.Contains(item));
    }
}
