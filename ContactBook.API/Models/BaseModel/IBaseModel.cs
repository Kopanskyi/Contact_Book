using System;

namespace ContactBook.API.Models.BaseModel
{
    public interface IBaseModel
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
