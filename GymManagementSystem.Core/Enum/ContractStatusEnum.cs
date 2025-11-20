namespace GymManagementSystem.Core.Enum;

public enum ContractStatus
{
    Draft,         // Utworzona automatycznie, jeszcze niewygenerowana PDF
    Generated,     // PDF wygenerowany, czeka na podpis
    Signed,        // Podpisana
    Terminated     // Wypowiedziana / zakończona
}
