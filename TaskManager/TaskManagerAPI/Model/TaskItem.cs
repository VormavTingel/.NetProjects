namespace TaskManagerAPI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }           // Chave primária
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
