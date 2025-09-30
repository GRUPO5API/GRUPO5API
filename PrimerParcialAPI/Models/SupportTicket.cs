namespace PrimerParcialAPI.Models;

public class SupportTicket
{
    public int Id { get; set; }                           // PK
    public string Subject { get; set; } = null!;          // Asunto obligatorio
    public string RequesterEmail { get; set; } = null!;   // Email obligatorio
    public string? Description { get; set; }              // Puede ser nulo
    public SeverityLevel Severity { get; set; }           // Enum para severidad
    public TicketStatus Status { get; set; }              // Enum para estado
    public DateTime OpenedAt { get; set; }                // Fecha apertura
    public DateTime? ClosedAt { get; set; }               // Fecha cierre (nullable)
    public string? AssignedTo { get; set; }               // Usuario asignado (nullable)
}

// Enumeración para severidad
public enum SeverityLevel
{
    Low,
    Medium,
    High,
    Critical
}

// Enumeración para estado del ticket
public enum TicketStatus
{
    Open,
    InProgress,
    OnHold,
    Closed
}